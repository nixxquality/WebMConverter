using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        private string _template;
        private string _templateArguments;
        private string _indexFile;

        private string _autoOutput;
        private string _autoTitle;
        private string _autoArguments;
        private bool _argumentError;

        public MainForm()
        {
            FFMSsharp.FFMS2.Initialize(Path.Combine(Environment.CurrentDirectory, "Binaries"));

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
            buttonGo.Text = "Indexing...";

            textBoxIn.Text = path;
            string fullPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            if (boxMetadataTitle.Text == _autoTitle || boxMetadataTitle.Text == "")
                boxMetadataTitle.Text = _autoTitle = name;
            if (textBoxOut.Text == _autoOutput || textBoxOut.Text == "")
                textBoxOut.Text = _autoOutput = Path.Combine(fullPath, name + ".webm");

            trackBar1.Enabled = true;

            // Index the file and generate our VideoSource object
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate {
                using (MD5 md5 = MD5.Create())
                {
                    using (FileStream stream = File.OpenRead(path))
                    {
                        _indexFile = Path.Combine(Path.GetTempPath(), BitConverter.ToString(md5.ComputeHash(stream)) + ".ffindex");
                    }
                }

                FFMSsharp.Index index;

                if (File.Exists(_indexFile))
                {
                    index = new FFMSsharp.Index(_indexFile);
                    if (index.BelongsToFile(path))
                    {
                        FFMS2.VideoSource = index.VideoSource(path, index.GetFirstTrackOfType(FFMSsharp.TrackType.Video));
                        return;
                    }
                }

                FFMSsharp.Indexer indexer = new FFMSsharp.Indexer(path);
                index = indexer.Index();

                index.WriteIndex(_indexFile);

                FFMS2.VideoSource = index.VideoSource(path, index.GetFirstTrackOfType(FFMSsharp.TrackType.Video));
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate{
                buttonGo.Enabled = true;
                buttonGo.Text = "Convert";
                toolStripButtonTrim.Enabled = true;
                toolStripButtonCrop.Enabled = true;
                toolStripButtonResize.Enabled = true;

                previewFrame1.GeneratePreview();
                trackBar1.Maximum = FFMS2.VideoSource.NumFrames - 1;
                trackBar1.TickFrequency = trackBar1.Maximum / 60;
            });
            bw.RunWorkerAsync();
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

        char[] invalidChars = Path.GetInvalidPathChars();

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

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine("PluginPath = \"" + Environment.CurrentDirectory + "/Binaries/\"");
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadPlugin(PluginPath+\"vsfilter.dll\")");
                avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\")", input, _indexFile));
                avscript.Write(textBoxProcessingScript.Text);
            }

            string[] arguments;
            if (!boxHQ.Checked)
                arguments = new[] { string.Format(_template, avsFileName, output, options, "", "") };
            else
            {
                int passes = 2; //Can you even use more than 2 passes?

                arguments = new string[passes];
                for (int i = 0; i < passes; i++)
                    arguments[i] = string.Format(_template, avsFileName, output, options, "-pass " + (i + 1));
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

            int bitrate = 900; // STUB
            if (!string.IsNullOrWhiteSpace(boxBitrate.Text))
                if (!int.TryParse(boxBitrate.Text, out bitrate))
                    throw new ArgumentException("Invalid bitrate!");

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

        private static string MakeParseFriendly(string text)
        {
            //This method adds "00:" in front of text, if the text format is in either 00:00 or 00:00.00 format.
            //This pattern should work.

            string pattern = @"^[0-5][0-9]:[0-5][0-9](\.[0-9]+)?$";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            if (regex.IsMatch(text))
                return "00:" + text;
            return text;
        }

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
            UpdateArguments(sender, e);
        }

        private void toolStripButtonTrim_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new TrimForm())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + "Trim(" + form.TrimStart + ", " + form.TrimEnd + ")");
                        listViewProcessingScript.Items.Add("Trim");
                        toolStripButtonTrim.Enabled = false;
                    }
                }
            }
            else
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + "Trim(start_frame, end_frame)");
            }
        }

        private void toolStripButtonCrop_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                using (var form = new CropForm())
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.CropPixels.GetAvisynthCommand());
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

        private void toolStripButtonResize_Click(object sender, EventArgs e) // STUB
        {
            if (!toolStripButtonAdvancedScripting.Checked)
            {
                listViewProcessingScript.Items.Add("LanczosResize");
                toolStripButtonResize.Enabled = false;
            }
            textBoxProcessingScript.AppendText(Environment.NewLine + "LanczosResize(width, height)");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButtonAdvancedScripting.Checked = !toolStripButtonAdvancedScripting.Checked;
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross; // STUB: get better icons
            //if (toolStripButton1.Checked)
            //{
                listViewProcessingScript.Hide();
                textBoxProcessingScript.Show();
                toolStripButtonTrim.Enabled = true;
                toolStripButtonCrop.Enabled = true;
                toolStripButtonResize.Enabled = true;
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
                        case "Trim":
                            toolStripButtonTrim.Enabled = true;
                            break;
                        case "Crop":
                            toolStripButtonCrop.Enabled = true;
                            break;
                        case "LanczosResize":
                            toolStripButtonResize.Enabled = true;
                            break;
                    }

                    List<string> processingScriptCommands = new List<string>(textBoxProcessingScript.Lines);
                    processingScriptCommands.Remove(processingScriptCommands.Find( x => x.StartsWith(item.Text) ));
                    textBoxProcessingScript.Lines = processingScriptCommands.ToArray();

                    listViewProcessingScript.Items.Remove(item);
                }
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            previewFrame1.Frame = trackBar1.Value;
        }

        private void listViewProcessingScript_ItemActivate(object sender, EventArgs e)
        {
            List<string> processingScriptCommands = new List<string>(textBoxProcessingScript.Lines);
            string filter = listViewProcessingScript.FocusedItem.Text;
            string item = processingScriptCommands.Find(x => x.StartsWith(filter));

            Match match;
            switch (filter)
            {
                case "Trim":
                    match = Regex.Match(item, @".*?(\d+).*?(\d+)");
                    if (!match.Success)
                        throw new Exception("The trim is fucked");

                    using (var form = new TrimForm(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            processingScriptCommands.Remove(item);
                            processingScriptCommands.Add("Trim(" + form.TrimStart + ", " + form.TrimEnd + ")");
                        }
                    }
                    break;
                case "Crop":
                    match = Regex.Match(item, @".*?(\d+).*?(\d+).*?([-+]\d+).*?([-+]\d+)");
                    if (!match.Success)
                        throw new Exception("The crop is fucked");

                    using (var form = new CropForm(new CropRectangle(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            processingScriptCommands.Remove(item);
                            processingScriptCommands.Add(form.CropPixels.GetAvisynthCommand());
                        }
                    }
                    break;
            }

            textBoxProcessingScript.Lines = processingScriptCommands.ToArray();
        }
    }
}
