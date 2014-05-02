using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        private readonly string _template;
        private readonly string _templateArguments;
        private string _indexFile;

        private string _autoOutput;
        private string _autoTitle;
        private string _autoArguments;
        private bool _argumentError;

        public MainForm()
        {
            FFMSSharp.FFMS2.Initialize(Path.Combine(Environment.CurrentDirectory, "Binaries"));

            InitializeComponent();

            AllowDrop = true;
            DragEnter += HandleDragEnter;
            DragDrop += HandleDragDrop;

            _templateArguments = "{0} -c:v libvpx -crf 32 -b:v {1}K -threads {2} {3} {4} {5}";
            //{0} is '-an' if no audio, otherwise blank
            //{1} is bitrate in kb/s
            //{2} is amount of threads to use
            //{3} is '-fs XM' if X MB limit enabled otherwise blank
            //{4} is '-metadata title="TITLE"' when specifying a title, otherwise blank
            //{5} is '-quality best -lag-in-frames 16 -auto-alt-ref 1' when using HQ mode, otherwise blank

            //TODO: add an option for subtitles. It's either '-vf "ass=subtitle.ass"' or '-vf subtitles=subtitle.srt'

            _template = "-f avisynth -i \"{0}\" {2} {3} -f webm -y \"{1}\"";
            //{0} is input file
            //{1} is output file
            //{2} is extra arguments
            //{3} is '-pass X' if HQ mode enabled, otherwise blank

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Keeping this disabled for now because threads are crashy

            //int threads = Environment.ProcessorCount;  //Set thread slider to default of 4
            //trackThreads.Value = Math.Min(trackThreads.Maximum, Math.Max(trackThreads.Minimum, threads));

            trackThreads_Scroll(sender, e); //Update label
        }

        private void buttonBrowseIn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    SetFile(dialog.FileName);
            }
        }

        private void SetFile(string path)
        {
            buttonGo.Enabled = false;
            buttonPreview.Enabled = false;
            buttonGo.Text = "Indexing...";

            textBoxIn.Text = path;
            string fullPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            if (boxMetadataTitle.Text == _autoTitle || boxMetadataTitle.Text == "")
                boxMetadataTitle.Text = _autoTitle = name;
            if (textBoxOut.Text == _autoOutput || textBoxOut.Text == "")
                textBoxOut.Text = _autoOutput = Path.Combine(fullPath, name + ".webm");
            Program.InputFile = path;
            Program.FileMd5 = null;

            // Reset filters
            Filters.ResetFilters();
            listViewProcessingScript.Clear();
            toolStripButtonAdvancedScripting.Checked = false; // STUB: this part is weak
            toolStripButtonAdvancedScripting.Enabled = true;
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross;
            textBoxProcessingScript.Hide();
            listViewProcessingScript.Show();
            GenerateAvisynthScript();

            // Index the file and generate our VideoSource object
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate
            {
                using (MD5 md5 = MD5.Create())
                {
                    using (FileStream stream = File.OpenRead(path))
                    {
                        Program.FileMd5 = BitConverter.ToString(md5.ComputeHash(stream));
                        _indexFile = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".ffindex");
                    }
                }

                FFMSSharp.Index index;

                if (File.Exists(_indexFile))
                {
                    index = new FFMSSharp.Index(_indexFile);
                    if (index.BelongsToFile(path))
                    {
                        IndexSubtitleTracks(path, index);
                        
                        return;
                    }
                }

                FFMSSharp.Indexer indexer = new FFMSSharp.Indexer(path);
                index = indexer.Index(new List<int>()); // don't index any audio tracks

                index.WriteIndex(_indexFile);

                IndexSubtitleTracks(path, index);
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate{
                buttonGo.Enabled = true;
                buttonPreview.Enabled = true;
                buttonGo.Text = "Convert";
                toolStripButtonCrop.Enabled = true;
                toolStripButtonResize.Enabled = true;
                toolStripButtonReverse.Enabled = true;
                toolStripButtonSubtitle.Enabled = true;
                toolStripButtonTrim.Enabled = true;

                // Extract attachments
                Program.AttachmentDirectory = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".attachments");
                Directory.CreateDirectory(Program.AttachmentDirectory);

                var ffmpeg = new FFmpeg(string.Format("-dump_attachment:t \"\" -y -i \"{0}\"", Program.InputFile));
                ffmpeg.StartInfo.WorkingDirectory = Program.AttachmentDirectory;
                ffmpeg.Start();
            });
            bw.RunWorkerAsync();
        }

        void IndexSubtitleTracks(string path, FFMSSharp.Index index)
        {
            Program.VideoSource = index.VideoSource(path, index.GetFirstTrackOfType(FFMSSharp.TrackType.Video));
            Program.SubtitleTracks = new List<int>();
            for (int i = 0; i <= index.NumberOfTracks; i++)
            {
                if (index.GetTrack(i).TrackType == FFMSSharp.TrackType.Subtitle)
                    Program.SubtitleTracks.Add(i);
            }
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            // show copy cursor for files
            bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = dataPresent ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) SetFile(file);
        }

        private void buttonBrowseOut_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.OverwritePrompt = true;
                dialog.ValidateNames = true;
                dialog.Filter = "WebM files|*.webm";

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    textBoxOut.Text = dialog.FileName;
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string result = Convert();
            if (!string.IsNullOrWhiteSpace(result))
                MessageBox.Show(result, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            string result = Preview();
            if (!string.IsNullOrWhiteSpace(result))
                MessageBox.Show(result, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        char[] invalidChars = Path.GetInvalidPathChars();

        private string Preview()
        {
            string input = textBoxIn.Text;

            if (string.IsNullOrWhiteSpace(input))
                return "No input file!";

            if (invalidChars.Any(input.Contains))
                return "Input path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars);

            if (!File.Exists(input))
                return "Input file doesn't exist!";

            string options = textBoxArguments.Text;
            try
            {
                if (options.Trim() == "" || _argumentError)
                    options = GenerateArguments();
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }

            // Generate the script if we're in simple mode
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            if (!toolStripButtonAdvancedScripting.Checked)
                GenerateAvisynthScript();

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine(string.Format("PluginPath = \"{0}\\\"", Path.Combine(Environment.CurrentDirectory, "Binaries")));
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadCPlugin(PluginPath+\"assrender.dll\")");
                avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\")", input, _indexFile));
                avscript.Write(textBoxProcessingScript.Text);
            }

            new FFplay(string.Format("-window_title Preview -f avisynth \"{0}\"", avsFileName)).Start();

            return null;
        }

        private string Convert()
        {
            string input = textBoxIn.Text;
            string output = textBoxOut.Text;

            if (string.IsNullOrWhiteSpace(input))
                return "No input file!";
            if (string.IsNullOrWhiteSpace(output))
                return "No output file!";

            if (invalidChars.Any(input.Contains))
                return "Input path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars);
            if (invalidChars.Any(output.Contains))
                return "Output path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars);

            if (!File.Exists(input))
                return "Input file doesn't exist!";

            if (input == output)
                return "Input and output files are the same!";

            string options = textBoxArguments.Text;
            try
            {
                if (options.Trim() == "" || _argumentError)
                    options = GenerateArguments();
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }

            // Generate the script if we're in simple mode
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            if (!toolStripButtonAdvancedScripting.Checked)
                GenerateAvisynthScript();

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine(string.Format("PluginPath = \"{0}\\\"", Path.Combine(Environment.CurrentDirectory, "Binaries")));
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadCPlugin(PluginPath+\"assrender.dll\")");
                avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\")", input, _indexFile));
                avscript.Write(textBoxProcessingScript.Text);
            }

            string[] arguments;
            if (!boxHQ.Checked)
                arguments = new[] { string.Format(_template, avsFileName, output, options, "", "") };
            else
            {
                arguments = new string[2];
                arguments[0] = string.Format(_template, avsFileName, output, options, "-pass 1");
                arguments[1] = string.Format(_template, avsFileName, output, options, "-pass 2");
            }

            var form = new ConverterForm(this, arguments);
            form.ShowDialog();

            return null;
        }

        private void UpdateArguments(object sender, EventArgs e)
        {
            try
            {
                string arguments = GenerateArguments();
                if (arguments != _autoArguments || _argumentError)
                {
                    textBoxArguments.Text = _autoArguments = arguments;
                    _argumentError = false;
                }
            }
            catch (ArgumentException argExc)
            {
                textBoxArguments.Text = "ERROR: " + argExc.Message;
                _argumentError = true;
            }
        }

        private string GenerateArguments()
        {
            float limit = 0;
            string limitTo = "";
            if (!string.IsNullOrWhiteSpace(boxLimit.Text))
            {
                if (!float.TryParse(boxLimit.Text, out limit))
                    throw new ArgumentException("Invalid size limit!");
                limitTo = string.Format("-fs {0}M", limit.ToString(CultureInfo.InvariantCulture)); //Should turn comma into dot
            }

            int bitrate = 900;
            if (!string.IsNullOrWhiteSpace(boxBitrate.Text))
            {
                if (!int.TryParse(boxBitrate.Text, out bitrate))
                    throw new ArgumentException("Invalid bitrate!");
            }
            else if (limit != 0)
            {
                double duration;

                if (!toolStripButtonAdvancedScripting.Checked)
                {
                    if (Filters.Trim == null)
                    {
                        duration = Program.VideoSource.LastTime - Program.VideoSource.FirstTime;
                    }
                    else
                    {
                        double firsttime, lasttime;
                        var track = Program.VideoSource.Track;

                        firsttime = Program.FrameToTime(Filters.Trim.TrimStart);
                        lasttime = Program.FrameToTime(Filters.Trim.TrimEnd);

                        duration = lasttime - firsttime;
                    }
                }
                else
                {
                    try
                    {
                        List<string> processingScriptCommands = new List<string>(textBoxProcessingScript.Lines);
                        string trimcmd = processingScriptCommands.Find(x => x.StartsWith("Trim("));

                        Match match = Regex.Match(trimcmd, @".*?(\d+).*?(\d+)");
                        double firsttime, lasttime;
                        var track = Program.VideoSource.Track;

                        firsttime = Program.FrameToTime(Filters.Trim.TrimStart);
                        lasttime = Program.FrameToTime(Filters.Trim.TrimEnd);

                        duration = lasttime - firsttime;
                    }
                    catch (ArgumentNullException) // Probably means there's no trimming in the script
                    {
                        duration = Program.VideoSource.LastTime - Program.VideoSource.FirstTime;
                    }
                }

                bitrate = (int)(8192 * limit / duration);
            }

            int threads = trackThreads.Value;

            string metadataTitle = "";
            if (!string.IsNullOrWhiteSpace(boxMetadataTitle.Text))
                metadataTitle = string.Format("-metadata title=\"{0}\"", boxMetadataTitle.Text.Replace("\"", "\\\""));

            string HQ = "";
            if (boxHQ.Checked)
                HQ = "-quality best -lag-in-frames 16 -auto-alt-ref 1";

            string audioEnabled = boxAudio.Checked ? "" : "-an"; //-an if no audio
            return string.Format(_templateArguments, audioEnabled, bitrate, threads, limitTo, metadataTitle, HQ);
        }

        private void GenerateAvisynthScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("# This is an AviSynth script. You may write advanced commands below, or just press the buttons above for smooth sailing.");
            if (Filters.Subtitle != null)
                script.AppendLine(Filters.Subtitle.GetAvisynthCommand());
            if (Filters.Trim != null)
                script.AppendLine(Filters.Trim.GetAvisynthCommand());
            if (Filters.Crop != null)
                script.AppendLine(Filters.Crop.GetAvisynthCommand());
            if (Filters.Resize != null)
                script.AppendLine(Filters.Resize.GetAvisynthCommand());
            if (Filters.Reverse != null)
                script.AppendLine(Filters.Reverse.GetAvisynthCommand());

            textBoxProcessingScript.Text = script.ToString();
        }

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
            UpdateArguments(sender, e);
        }

        private void toolStripButtonCrop_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new CropForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Filters.Crop = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Crop");
                        toolStripButtonCrop.Enabled = false;
                    }
                }
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "Crop(left, top, -right, -bottom)");
            }
        }

        private void toolStripButtonResize_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new ResizeForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Filters.Resize = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Resize");
                        toolStripButtonResize.Enabled = false;
                    }
                }
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "LanczosResize(width, height)");
            }
        }

        private void toolStripButtonReverse_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                Filters.Reverse = new ReverseFilter();
                listViewProcessingScript.Items.Add("Reverse");
                toolStripButtonReverse.Enabled = false;
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "Reverse()");
            }
        }

        private void toolStripButtonSubtitle_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new SubtitleForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Filters.Subtitle = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Subtitle");
                        toolStripButtonSubtitle.Enabled = false;
                    }
                }
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "assrender(filename)");
            }
        }

        private void toolStripButtonTrim_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new TrimForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Filters.Trim = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Trim");
                        GenerateArguments();
                        toolStripButtonTrim.Enabled = false;
                    }
                }
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "Trim(start_frame, end_frame)");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButtonAdvancedScripting.Checked = !toolStripButtonAdvancedScripting.Checked;
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross; // STUB: get better icons
            //if (toolStripButton1.Checked)
            //{
                listViewProcessingScript.Hide();
                if (Filters.Subtitle != null)
                    Filters.Subtitle.BeforeEncode();
                GenerateAvisynthScript();
                textBoxProcessingScript.Show();
                toolStripButtonCrop.Enabled = true;
                toolStripButtonResize.Enabled = true;
                toolStripButtonReverse.Enabled = true;
                toolStripButtonSubtitle.Enabled = true;
                toolStripButtonTrim.Enabled = true;
            //}
            //else
            //{
            //    textBoxProcessingScript.Hide();
            //    listViewProcessingScript.Show();
            //}
            // This could be used to toggle between advanced and list view, but that's hard to code. Maybe later?
            // For now, just stay in advanced mode forever.
            toolStripButtonAdvancedScripting.Enabled = false;
        }

        private void listViewProcessingScript_KeyUp(object sender, KeyEventArgs e) // STUB
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem item in listViewProcessingScript.SelectedItems)
                {
                    switch (item.Text)
                    {
                        case "Crop":
                            Filters.Crop = null;
                            toolStripButtonCrop.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            break;
                        case "Resize":
                            Filters.Resize = null;
                            toolStripButtonResize.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            break;
                        case "Reverse":
                            Filters.Reverse = null;
                            toolStripButtonReverse.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            break;
                        case "Subtitle":
                            Filters.Subtitle = null;
                            toolStripButtonSubtitle.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            break;
                        case "Trim":
                            Filters.Trim = null;
                            toolStripButtonTrim.Enabled = true;
                            GenerateArguments();
                            listViewProcessingScript.Items.Remove(item);
                            break;
                    }
                }
            }
        }

        private void listViewProcessingScript_ItemActivate(object sender, EventArgs e)
        {
            switch (listViewProcessingScript.FocusedItem.Text)
            {
                case "Crop":
                    using (var form = new CropForm(Filters.Crop))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Filters.Crop = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Resize":
                    using (var form = new ResizeForm(Filters.Resize))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Filters.Resize = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Subtitle":
                    using (var form = new SubtitleForm(Filters.Subtitle))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Filters.Subtitle = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Trim":
                    using (var form = new TrimForm(Filters.Trim))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Filters.Trim = form.GeneratedFilter;
                            GenerateArguments();
                        }
                    }
                    break;
            }
        }
    }
}
