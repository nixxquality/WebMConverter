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

        bool indexing = false;
        BackgroundWorker indexbw;

        #region MainForm

        public MainForm()
        {
            FFMSSharp.FFMS2.Initialize(Path.Combine(Environment.CurrentDirectory, "Binaries"));

            InitializeComponent();

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

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100);

            imageList.Images.Add("crop", Properties.Resources.crop);
            imageList.Images.Add("deinterlace", Properties.Resources.deinterlace);
            imageList.Images.Add("levels", Properties.Resources.levels);
            imageList.Images.Add("resize", Properties.Resources.resize);
            imageList.Images.Add("reverse", Properties.Resources.reverse);
            imageList.Images.Add("subtitles", Properties.Resources.subtitles);
            imageList.Images.Add("trim", Properties.Resources.trim);

            listViewProcessingScript.LargeImageList = imageList;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Keeping this disabled for now because threads are crashy

            //int threads = Environment.ProcessorCount;  //Set thread slider to default of 4
            //trackThreads.Value = Math.Min(trackThreads.Maximum, Math.Max(trackThreads.Minimum, threads));

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
                        toolStripButtonCrop.Enabled = false;
                    }
                }
            }
        }

        private void toolStripButtonDeinterlace_Click(object sender, EventArgs e)
        {
            if (toolStripButtonAdvancedScripting.Checked)
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + new DeinterlaceFilter().ToString());
            }
            else
            {
                Filters.Deinterlace = new DeinterlaceFilter();
                listViewProcessingScript.Items.Add("Deinterlace").ImageKey = "deinterlace";
                toolStripButtonDeinterlace.Enabled = false;
            }
        }

        private void toolStripButtonLevels_Click(object sender, EventArgs e)
        {
            if (Program.VideoColorRange == FFMSSharp.ColorRange.JPEG)
            {
                const string message = "From what I can see, there is no need to expand the color ranges.\n" + 
                                       "Are you sure you want to mess with the color balance?\n" + 
                                       "Only press OK if you're 100% sure you know what you're doing.";
                const string caption = "Are you sure?";
                var result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Cancel)
                    return;
            }

            if (toolStripButtonAdvancedScripting.Checked)
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + new LevelsFilter().ToString());
            }
            else
            {
                Filters.Levels = new LevelsFilter();
                listViewProcessingScript.Items.Add("Levels").ImageKey = "levels";
                toolStripButtonLevels.Enabled = false;
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
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross; // STUB: get better icons
            //if (toolStripButton1.Checked)
            //{
            listViewProcessingScript.Hide();
            if (Filters.Subtitle != null)
                Filters.Subtitle.BeforeEncode();
            GenerateAvisynthScript();
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

        private void toolStripFilterButtonsEnabled(bool enabled)
        {
            toolStripButtonCrop.Enabled = enabled;
            toolStripButtonDeinterlace.Enabled = enabled;
            toolStripButtonLevels.Enabled = enabled;
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
                            break;
                        case "Deinterlace":
                            Filters.Deinterlace = null;
                            toolStripButtonDeinterlace.Enabled = true;
                            listViewProcessingScript.Items.Remove(item);
                            break;
                        case "Levels":
                            if (Program.VideoColorRange == FFMSSharp.ColorRange.MPEG)
                            {
                                const string message = "From what I can see, you should be expanding the color ranges.\n" +
                                                       "Are you sure you want to mess with the color balance?\n" +
                                                       "Only press OK if you're 100% sure you know what you're doing.";
                                const string caption = "Are you sure?";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                if (result == DialogResult.Cancel)
                                    break;
                            }
                            Filters.Levels = null;
                            toolStripButtonLevels.Enabled = true;
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
                default:
                    MessageBox.Show("This filter has no options.");
                    break;
            }
        }

        #region Tooltips

        private void clearToolTip(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
        }

        private void toolStripButtonTrim_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Select a clip from your video.";
        }

        private void toolStripButtonCrop_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Crop your video into a smaller frame.";
        }

        private void toolStripButtonResize_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Scale your video.";
        }

        private void toolStripButtonReverse_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Everything is backwards!";
        }

        private void toolStripButtonSubtitle_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Burn subtitles into the video.";
        }

        private void toolStripButtonDeinterlace_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Get rid of interlacing artifacts. Only useful if your input video is interlaced.";
        }

        private void toolStripButtonLevels_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Expand the color levels to PC ranges. Use this if your colors look washed out compared to the input video.";
        }

        private void buttonPreview_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Open a preview window that will loop your processing settings. Note that this doesn't reflect output encoding quality.";
        }

        private void toolStripButtonAdvancedScripting_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Are you a bad enough dude? Take care, there is no way back. You will have to start over if you fuck up.";
        }

        private void listViewProcessingScript_MouseEnter(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Double click a filter to edit it. Select a filter and press Delete to remove it.";
        }

        #endregion

        #endregion

        #region tabPageEncoding

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
            UpdateArguments(sender, e);
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
            toolStripButtonAdvancedScripting.Image = toolStripButtonAdvancedScripting.Checked ? WebMConverter.Properties.Resources.tick : WebMConverter.Properties.Resources.cross;
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

                index.WriteIndex(_indexFile);
            };
            extractbw.DoWork += new DoWorkEventHandler(delegate
            {
                Program.VideoSource = index.VideoSource(path, index.GetFirstTrackOfType(FFMSSharp.TrackType.Video));
                Program.VideoColorRange = Program.VideoSource.GetFrame(0).ColorRange; // We're assuming that the entire video has the same color range, which should be fine.

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
                {
                    Filters.Levels = new LevelsFilter();
                    listViewProcessingScript.Items.Add("Levels").ImageKey = "levels";
                    toolStripButtonLevels.Enabled = false;
                }

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

        private void GenerateAvisynthScript(string avsFileName, string avsInputFile)
        {
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine(string.Format("PluginPath = \"{0}\\\"", Path.Combine(Environment.CurrentDirectory, "Binaries")));
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadCPlugin(PluginPath+\"assrender.dll\")");
                avscript.WriteLine("LoadPlugin(PluginPath+\"TDeint.dll\")");
                if (boxAudio.Checked)
                    avscript.WriteLine(string.Format("AudioDub(FFVideoSource(\"{0}\",cachefile=\"{1}\"), FFAudioSource(\"{0}\",cachefile=\"{1}\"))", avsInputFile, _indexFile));
                else
                    avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\")", avsInputFile, _indexFile));
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
            GenerateAvisynthScript(avsFileName, input);

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
            GenerateAvisynthScript(avsFileName, input);

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
            if (Filters.Deinterlace != null)
                script.AppendLine(Filters.Deinterlace.ToString());
            if (Filters.Levels != null)
                script.AppendLine(Filters.Levels.ToString());
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

        #endregion
    }
}
