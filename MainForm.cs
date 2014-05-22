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
using StopWatch = System.Timers.Timer;
using System.Xml;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        private const string _template = "-f avisynth -i \"{0}\" {2} {3} -f webm -y \"{1}\"";
        //{0} is input file
        //{1} is output file
        //{2} is extra arguments
        //{3} is '-pass X' if HQ mode enabled, otherwise blank

        private const string _templateArguments = "{0} -c:v libvpx -crf 32 -b:v {1}K -threads {2} -slices {3}{4}{5}{6}";
        //{0} is '-an' if no audio, otherwise blank
        //{1} is bitrate in kb/s
        //{2} is amount of threads to use
        //{3} is amount of slices to split the frame into
        //{4} is ' -fs XM' if X MB limit enabled otherwise blank
        //{5} is ' -metadata title="TITLE"' when specifying a title, otherwise blank
        //{6} is ' -quality best -lag-in-frames 16 -auto-alt-ref 1' when using HQ mode, otherwise blank

        private string _indexFile;

        private string _autoOutput;
        private string _autoTitle;
        private string _autoArguments;
        private bool _argumentError;

        bool indexing = false;
        BackgroundWorker indexbw;

        StopWatch toolTipTimer;
        string avsScriptInfo;

        int videotrack = -1;
        int audiotrack = -1;

        #region MainForm

        public MainForm()
        {
            FFMSSharp.FFMS2.Initialize(Path.Combine(Environment.CurrentDirectory, "Binaries"));

            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100);

            imageList.Images.Add("crop", Properties.Resources.crop);
            imageList.Images.Add("resize", Properties.Resources.resize);
            imageList.Images.Add("reverse", Properties.Resources.reverse);
            imageList.Images.Add("subtitles", Properties.Resources.subtitles);
            imageList.Images.Add("trim", Properties.Resources.trim);

            listViewProcessingScript.LargeImageList = imageList;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*
             * Keeping this disabled for now because threads are crashy
             *
            int threads = Environment.ProcessorCount;  //Set thread slider to default of 4
            trackThreads.Value = Math.Min(trackThreads.Maximum, Math.Max(trackThreads.Minimum, threads));
             */

            trackThreads_Scroll(sender, e); //Update label
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            // show copy cursor for files
            bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = dataPresent ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            SetFile(files[0]);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1) // We were "Open with..."ed with a file
                SetFile(args[1]);

            clearToolTip();
        }

        void showToolTip(string message, int timer = 0)
        {
            clearToolTip();

            toolStripStatusLabel.Text = message;
            if (timer > 0)
            {
                toolTipTimer = new StopWatch(timer);

                toolTipTimer.Elapsed += (sender, e) => clearToolTip();

                toolTipTimer.AutoReset = false;
                toolTipTimer.Enabled = true;
            }
        }

        void clearToolTip(object sender = null, EventArgs e = null)
        {
            if (toolTipTimer != null)
                toolTipTimer.Close();
            toolStripStatusLabel.Text = "";
        }

        #endregion

        #region groupBoxMain

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
            if (indexing) // We are actually a cancel button right now.
            {
                indexbw.CancelAsync();
                buttonGo.Enabled = false;
                return;
            }

            try
            {
                Convert();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Preview();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region tabPageProcessing

        private void toolStripButtonCrop_Click(object sender, EventArgs e)
        {
            using (var form = new CropForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (toolStripButtonAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Crop = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Crop").ImageKey = "crop";
                        SetSlices();
                        toolStripButtonCrop.Enabled = false;
                    }
                }
            }
        }

        private void toolStripButtonResize_Click(object sender, EventArgs e)
        {
            using (var form = new ResizeForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (toolStripButtonAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Resize = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Resize").ImageKey = "resize";
                        SetSlices();
                        toolStripButtonResize.Enabled = false;
                    }
                }
            }
        }

        private void toolStripButtonReverse_Click(object sender, EventArgs e)
        {
            if (toolStripButtonAdvancedScripting.Checked)
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + new ReverseFilter().ToString());
            }
            else
            {
                Filters.Reverse = new ReverseFilter();
                listViewProcessingScript.Items.Add("Reverse").ImageKey = "reverse";
                toolStripButtonReverse.Enabled = false;
            }
        }

        private void toolStripButtonSubtitle_Click(object sender, EventArgs e)
        {
            using (var form = new SubtitleForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (toolStripButtonAdvancedScripting.Checked)
                    {
                        form.GeneratedFilter.BeforeEncode();
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Subtitle = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Subtitle").ImageKey = "subtitles";
                        toolStripButtonSubtitle.Enabled = false;
                    }
                }
            }
        }

        private void toolStripButtonTrim_Click(object sender, EventArgs e)
        {
            using (var form = new TrimForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (toolStripButtonAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Trim = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Trim").ImageKey = "trim";
                        GenerateArguments();
                        toolStripButtonTrim.Enabled = false;
                    }
                }
            }
        }

        private void toolStripButtonAdvancedScripting_Click(object sender, EventArgs e)
        {
            toolStripButtonAdvancedScripting.Checked = !toolStripButtonAdvancedScripting.Checked;
            //if (toolStripButton1.Checked)
            //{
            listViewProcessingScript.Hide();
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            GenerateAvisynthScript();
            ProbeScript();
            textBoxProcessingScript.Show();
            toolStripFilterButtonsEnabled(true);
            //}
            //else
            //{
            //    textBoxProcessingScript.Hide();
            //    listViewProcessingScript.Show();
            //}
            // This could be used to toggle between advanced and list view, but that's hard to code. Maybe later?
            // For now, just stay in advanced mode forever.
            toolStripButtonAdvancedScripting.Enabled = false;
            clearToolTip(sender, e);
        }

        private void toolStripButtonAdvancedScripting_CheckedChanged(object sender, EventArgs e)
        {
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross; // STUB: get better icons
        }

        private void toolStripFilterButtonsEnabled(bool enabled)
        {
            toolStripButtonCrop.Enabled = enabled;
            toolStripButtonResize.Enabled = enabled;
            toolStripButtonReverse.Enabled = enabled;
            toolStripButtonSubtitle.Enabled = enabled;
            toolStripButtonTrim.Enabled = enabled;
        }

        private void listViewProcessingScript_KeyUp(object sender, KeyEventArgs e)
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
                            SetSlices();
                            break;
                        case "Resize":
                            Filters.Resize = null;
                            toolStripButtonResize.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            SetSlices();
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
                            SetSlices();
                        }
                    }
                    break;
                case "Resize":
                    using (var form = new ResizeForm(Filters.Resize))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Filters.Resize = form.GeneratedFilter;
                            SetSlices();
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
                default:
                    MessageBox.Show("This filter has no options.");
                    break;
            }
        }

        void textBoxProcessingScript_Leave(object sender, EventArgs e)
        {
            ProbeScript();
            SetSlices();
        }

        #region Tooltips

        private void toolStripButtonTrim_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Select a clip from your video.");
        }

        private void toolStripButtonCrop_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Crop your video into a smaller frame.");
        }

        private void toolStripButtonResize_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Scale your video.");
        }

        private void toolStripButtonReverse_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Everything is backwards!");
        }

        private void toolStripButtonSubtitle_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Burn subtitles into the video.");
        }

        private void toolStripButtonDeinterlace_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Get rid of interlacing artifacts. Only useful if your input video is interlaced.");
        }

        private void toolStripButtonLevels_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Expand the color levels to PC ranges. Use this if your colors look washed out compared to the input video.");
        }

        private void buttonPreview_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Open a preview window that will loop your processing settings. Note that this doesn't reflect output encoding quality.");
        }

        private void toolStripButtonAdvancedScripting_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Are you a bad enough dude? Take care, there is no way back. You will have to start over if you fuck up.");
        }

        private void listViewProcessingScript_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("Double click a filter to edit it. Select a filter and press Delete to remove it.");
        }

        #endregion

        #endregion

        #region tabPageEncoding

        private void textBoxNumbersOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        #endregion

        #region tagPageAdvanced

        private void boxLevels_CheckedChanged(object sender, EventArgs e)
        {
            Filters.Levels = boxLevels.Checked ? new LevelsFilter() : null;
        }

        private void boxDeinterlace_CheckedChanged(object sender, EventArgs e)
        {
            Filters.Deinterlace = boxDeinterlace.Checked ? new DeinterlaceFilter() : null;
        }

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
            UpdateArguments(sender, e);
        }

        private void trackSlices_Scroll(object sender, EventArgs e)
        {
            labelSlices.Text = GetSlices().ToString();
            UpdateArguments(sender, e);
        }

        #endregion

        #region Functions

        char[] invalidChars = Path.GetInvalidPathChars();

        private void SetFile(string path)
        {
            progressBarIndexing.Style = ProgressBarStyle.Marquee;
            progressBarIndexing.Value = 30;
            panelHideTheOptions.BringToFront();

            buttonGo.Enabled = false;
            buttonPreview.Enabled = false;
            buttonBrowseIn.Enabled = false;
            textBoxIn.Enabled = false;

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
            textBoxProcessingScript.Hide();
            listViewProcessingScript.Show();
            GenerateAvisynthScript();

            // Hash some of the file to make sure we didn't index it already
            labelIndexingProgress.Text = "Hashing...";
            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(path))
            {
                var filename = new UTF8Encoding().GetBytes(name);
                var buffer = new byte[4096];

                filename.CopyTo(buffer, 0);
                stream.Read(buffer, filename.Length, 4096 - filename.Length);

                Program.FileMd5 = BitConverter.ToString(md5.ComputeHash(buffer));
                _indexFile = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".ffindex");
            }

            FFMSSharp.Index index = null;
            indexbw = new BackgroundWorker();
            BackgroundWorker extractbw = new BackgroundWorker();

            indexbw.WorkerSupportsCancellation = true;
            indexbw.WorkerReportsProgress = true;
            indexbw.ProgressChanged += new ProgressChangedEventHandler(delegate(object sender, ProgressChangedEventArgs e)
            {
                this.progressBarIndexing.Value = e.ProgressPercentage;
            });
            indexbw.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                FFMSSharp.Indexer indexer = new FFMSSharp.Indexer(path);

                indexer.UpdateIndexProgress += delegate(object sendertwo, FFMSSharp.IndexingProgressChangeEventArgs etwo)
                {
                    indexbw.ReportProgress((int)(((double)etwo.Current / (double)etwo.Total) * 100));
                    indexer.CancelIndexing = indexbw.CancellationPending;
                };

                try
                {
                    index = indexer.Index();
                }
                catch (OperationCanceledException)
                {
                    e.Cancel = true;
                    return;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    e.Cancel = true;
                    return;
                }

                index.WriteIndex(_indexFile);
            };
            extractbw.DoWork += new DoWorkEventHandler(delegate
            {
                List<int> videoTracks = new List<int>(), audioTracks = new List<int>();
                for (int i = 0; i < index.NumberOfTracks; i++)
                {
                    switch(index.GetTrack(i).TrackType)
                    {
                        case FFMSSharp.TrackType.Video:
                            videoTracks.Add(i);
                            break;
                        case FFMSSharp.TrackType.Audio:
                            audioTracks.Add(i);
                            break;
                    }
                }

                if (videoTracks.Count == 1)
                {
                    videotrack = videoTracks[0];
                }
                else
                {
                    using (var dialog = new TrackSelectForm("Video", videoTracks))
                    {
                        dialog.ShowDialog();
                        videotrack = dialog.SelectedTrack;
                    }
                }

                if (audioTracks.Count == 1)
                {
                    audiotrack = audioTracks[0];
                }
                else
                {
                    using (var dialog = new TrackSelectForm("Audio", audioTracks))
                    {
                        dialog.ShowDialog();
                        audiotrack = dialog.SelectedTrack;
                    }
                }

                Program.VideoSource = index.VideoSource(path, videotrack);
                var frame = Program.VideoSource.GetFrame(0); // We're assuming that the entire video has the same settings here, which should be fine. (These options usually don't vary, I hope.)
                Program.VideoColorRange = frame.ColorRange; 
                Program.VideoInterlaced = frame.InterlacedFrame;

                Program.SubtitleTracks = new List<int>();
                for (int i = 0; i <= index.NumberOfTracks; i++)
                {
                    if (index.GetTrack(i).TrackType == FFMSSharp.TrackType.Subtitle)
                        Program.SubtitleTracks.Add(i);
                }

                Program.AttachmentDirectory = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".attachments");
                Directory.CreateDirectory(Program.AttachmentDirectory);

                var ffmpeg = new FFmpeg(string.Format("-dump_attachment:t \"\" -y -i \"{0}\"", Program.InputFile));
                ffmpeg.StartInfo.WorkingDirectory = Program.AttachmentDirectory;
                ffmpeg.Start();

                ffmpeg.WaitForExit();
            });
            indexbw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                indexing = false;
                buttonGo.Enabled = false;
                buttonGo.Text = "Convert";

                if (e.Cancelled)
                {
                    textBoxIn.Text = "";
                    textBoxOut.Text = "";
                    Program.InputFile = path;
                    Program.FileMd5 = null;
                    buttonBrowseIn.Enabled = true;
                    textBoxIn.Enabled = true;
                    panelHideTheOptions.SendToBack();
                }
                else
                {
                    labelIndexingProgress.Text = "Looking up subtitle tracks and extracting attachments...";
                    progressBarIndexing.Value = 30;
                    progressBarIndexing.Style = ProgressBarStyle.Marquee;

                    extractbw.RunWorkerAsync();
                }
            };
            extractbw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate
            {
                buttonGo.Enabled = true;
                buttonPreview.Enabled = true;
                buttonBrowseIn.Enabled = true;
                textBoxIn.Enabled = true;
                toolStripFilterButtonsEnabled(true);

                if (Program.VideoColorRange == FFMSSharp.ColorRange.MPEG)
                    boxLevels.Checked = true;
                if (Program.VideoInterlaced)
                    boxDeinterlace.Checked = true;
                SetSlices();

                panelHideTheOptions.SendToBack();
            });

            if (File.Exists(_indexFile))
            {
                index = new FFMSSharp.Index(_indexFile);
                if (index.BelongsToFile(path))
                {
                    labelIndexingProgress.Text = "Looking up subtitle tracks and extracting attachments...";
                    progressBarIndexing.Value = 30;
                    progressBarIndexing.Style = ProgressBarStyle.Marquee;

                    extractbw.RunWorkerAsync();
                    return;
                }
            }

            indexing = true;
            buttonGo.Enabled = true;
            buttonGo.Text = "Cancel";
            progressBarIndexing.Style = ProgressBarStyle.Continuous;
            labelIndexingProgress.Text = "Indexing...";
            indexbw.RunWorkerAsync();
        }

        private void ValidateInputFile(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new Exception("No input file!");

            if (invalidChars.Any(input.Contains))
                throw new Exception("Input path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars));

            if (!File.Exists(input))
                throw new Exception("Input file doesn't exist!");

            if (input.Any(c => c > 255)) // http://stackoverflow.com/questions/4459571/
                throw new Exception("Sorry, no moonrunes in your input file name.");
        }

        private void ValidateOutputFile(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
                throw new Exception("No output file!");

            if (invalidChars.Any(output.Contains))
                throw new Exception("Output path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars));
        }

        private void UpdateArguments(object sender, EventArgs e)
        {
            try
            {
                string arguments = GenerateArguments();
                if (arguments != _autoArguments || _argumentError)
                {
                    textBoxArguments.Text = _autoArguments = arguments;
                    showToolTip(arguments, 5000);
                    _argumentError = false;
                }
            }
            catch (ArgumentException argExc)
            {
                textBoxArguments.Text = "ERROR: " + argExc.Message;
                showToolTip("ERROR: " + argExc.Message, 5000);
                _argumentError = true;
            }
        }

        private void WriteAvisynthScript(string avsFileName, string avsInputFile)
        {
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine(string.Format("PluginPath = \"{0}\\\"", Path.Combine(Environment.CurrentDirectory, "Binaries")));
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadCPlugin(PluginPath+\"assrender.dll\")");

                if (boxAudio.Checked)
                    avscript.WriteLine(string.Format("AudioDub(FFVideoSource(\"{0}\",cachefile=\"{1}\",track={2}), FFAudioSource(\"{0}\",cachefile=\"{1}\",track={3}))", avsInputFile, _indexFile, videotrack, audiotrack));
                else
                    avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\",track={2})", avsInputFile, _indexFile, videotrack));

                if (Filters.Deinterlace != null)
                {
                    avscript.WriteLine("LoadPlugin(PluginPath+\"TDeint.dll\")");
                    avscript.WriteLine(Filters.Deinterlace.ToString());
                }

                if (Filters.Levels != null)
                    avscript.WriteLine(Filters.Levels.ToString());

                avscript.Write(textBoxProcessingScript.Text);
            }
        }

        private void Preview()
        {
            string input = textBoxIn.Text;
            buttonPreview.Enabled = false;

            ValidateInputFile(input);

            // Generate the script if we're in simple mode
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            if (!toolStripButtonAdvancedScripting.Checked)
                GenerateAvisynthScript();

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            WriteAvisynthScript(avsFileName, input);

            // Run ffplay
            var ffplay = new FFplay(string.Format("-window_title Preview -loop 0 -f avisynth \"{0}\"", avsFileName));
            ffplay.Exited += delegate
            {
                toolStripProcessingScript.Invoke(new Action(() => buttonPreview.Enabled = true));
            };
            ffplay.Start();
        }

        private void Convert()
        {
            string input = textBoxIn.Text;
            string output = textBoxOut.Text;

            if (input == output)
                throw new Exception("Input and output files are the same!");

            string options = textBoxArguments.Text;

            ValidateInputFile(input);
            ValidateOutputFile(output);

            if (options.Trim() == "" || _argumentError)
                options = GenerateArguments();

            // Generate the script if we're in simple mode
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            if (!toolStripButtonAdvancedScripting.Checked)
                GenerateAvisynthScript();

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            WriteAvisynthScript(avsFileName, input);

            string[] arguments;
            if (!boxHQ.Checked)
                arguments = new[] { string.Format(_template, avsFileName, output, options, "", "") };
            else
            {
                arguments = new string[2];
                arguments[0] = string.Format(_template, avsFileName, output, options, "-pass 1");
                arguments[1] = string.Format(_template, avsFileName, output, options, "-pass 2");
            }

            new ConverterForm(this, arguments).ShowDialog();
        }

        private string GenerateArguments()
        {
            float limit = 0;
            string limitTo = "";
            if (!string.IsNullOrWhiteSpace(boxLimit.Text))
            {
                if (!float.TryParse(boxLimit.Text, out limit))
                    throw new ArgumentException("Invalid size limit!");
                limitTo = string.Format(" -fs {0}M", limit.ToString(CultureInfo.InvariantCulture)); //Should turn comma into dot
            }

            int bitrate = 900;
            if (!string.IsNullOrWhiteSpace(boxBitrate.Text))
            {
                if (!int.TryParse(boxBitrate.Text, out bitrate))
                    throw new ArgumentException("Invalid bitrate!");
            }
            else if (limit != 0)
            {
                double duration = GetDuration();

                if (duration > 0)
                    bitrate = (int)(8192 * limit / duration);
            }

            int threads = trackThreads.Value;
            int slices = GetSlices();

            string metadataTitle = "";
            if (!string.IsNullOrWhiteSpace(boxMetadataTitle.Text))
                metadataTitle = string.Format(" -metadata title=\"{0}\"", boxMetadataTitle.Text.Replace("\"", "\\\""));

            string HQ = "";
            if (boxHQ.Checked)
                HQ = " -quality best -lag-in-frames 16 -auto-alt-ref 1";

            string audioEnabled = boxAudio.Checked ? "" : "-an"; //-an if no audio
            return string.Format(_templateArguments, audioEnabled, bitrate, threads, slices, limitTo, metadataTitle, HQ);
        }

        /// <summary>
        /// Attempt to calculate the duration of the Avisynth script.
        /// </summary>
        /// <returns>The duration or -1 if automatic detection was unsuccessful.</returns>
        private double GetDuration()
        {
            if (toolStripButtonAdvancedScripting.Checked)
            {
                // The dirty way.

                using (XmlReader reader = XmlReader.Create(new StringReader(avsScriptInfo)))
                {
                    reader.ReadToFollowing("stream");

                    while (reader.MoveToNextAttribute())
                    {
                        if (reader.Name == "duration")
                        {
                            return double.Parse(reader.Value);
                        }
                    }
                }

                return -1;
            }
            else
            {
                // The easy way.

                if (Filters.Trim != null)
                    return Filters.Trim.GetDuration();

                return Program.FrameToTime(Program.VideoSource.NumberOfFrames - 1);
            }
        }

        private Size GetResolution()
        {
            if (toolStripButtonAdvancedScripting.Checked)
            {
                // The dirty way.

                int width = -1, height = -1;

                using (XmlReader reader = XmlReader.Create(new StringReader(avsScriptInfo)))
                {
                    reader.ReadToFollowing("stream");

                    while (reader.MoveToNextAttribute())
                    {
                        if (reader.Name == "width")
                        {
                            width = int.Parse(reader.Value);
                        }
                        if (reader.Name == "height")
                        {
                            height = int.Parse(reader.Value);
                        }
                    }
                }

                return new Size(width, height);
            }
            else
            {
                if (Filters.Resize != null)
                {
                    return new Size(Filters.Resize.TargetWidth, Filters.Resize.TargetHeight);
                }

                var frame = Program.VideoSource.GetFrame((Filters.Trim == null) ? 0 : Filters.Trim.TrimStart); // the video may have different frame resolutions

                if (Filters.Crop != null)
                {
                    int width = frame.EncodedResolution.Width - Filters.Crop.Left + Filters.Crop.Right;
                    int height = frame.EncodedResolution.Height - Filters.Crop.Top + Filters.Crop.Bottom;

                    return new Size(width, height);
                }

                return frame.EncodedResolution;
            }
        }

        private void GenerateAvisynthScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("# This is an AviSynth script. You may write advanced commands below, or just press the buttons above for smooth sailing.");
            if (Filters.Subtitle != null)
                script.AppendLine(Filters.Subtitle.ToString());
            if (Filters.Trim != null)
                script.AppendLine(Filters.Trim.ToString());
            if (Filters.Crop != null)
                script.AppendLine(Filters.Crop.ToString());
            if (Filters.Resize != null)
                script.AppendLine(Filters.Resize.ToString());
            if (Filters.Reverse != null)
                script.AppendLine(Filters.Reverse.ToString());

            textBoxProcessingScript.Text = script.ToString();
        }

        void ProbeScript()
        {
            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            WriteAvisynthScript(avsFileName, textBoxIn.Text);

            // ffprobe it
            var ffprobe = new FFprobe(avsFileName);
            avsScriptInfo = ffprobe.Probe();
        }

        int GetSlices()
        {
            return (int)Math.Pow(2, trackSlices.Value - 1);
        }

        void SetSlices()
        {
            var resolution = GetResolution();
            int slices;

            if (resolution.Width * resolution.Height >= 2073600) // 1080p (1920*1080)
            {
                slices = 4;
            }
            else if (resolution.Width * resolution.Height >= 921600) // 720p (1280*720)
            {
                slices = 3;
            }
            else if (resolution.Width * resolution.Height >= 307200) // 480p (640*480)
            {
                slices = 2;
            }
            else
            {
                slices = 1;
            }

            trackSlices.Value = slices;
        }

        #endregion
    }
}
