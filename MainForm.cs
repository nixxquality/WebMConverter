using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using StopWatch = System.Timers.Timer;

using GitHubUpdate;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        #region FFmpeg argument base strings

        /// <summary>
        /// {0} is output file
        /// {1} is templateArguments
        /// {2} is '-pass X -passlogfile X' if HQ mode enabled, otherwise blank
        /// </summary>
        const string template = " {1}{2} -f webm -y \"{0}\"";

        /// <summary>
        /// {0} is pass number (1 or 2)
        /// {1} is the prefix for the pass .log file
        /// </summary>
        const string passArgument = " -pass {0} -passlogfile \"{1}\"";

        /// <summary>
        /// {0} is '-an' if no audio, otherwise blank
        /// {1} is amount of threads to use
        /// {2} is amount of slices to split the frame into
        /// {3} is ' -metadata title="TITLE"' when specifying a title, otherwise blank
        /// {4} is ' -quality best -lag-in-frames 16 -auto-alt-ref 1' when using HQ mode, otherwise blank
        /// {5} is 'libvpx(-vp9)' changing depending on NGOV mode
        /// {6} is ' -ac 2 -c:a libopus/libvorbis' if audio is enabled, changing depending on NGOV mode
        /// {7} is ' -r XX' if frame rate is modified, otherwise blank
        /// {8} is encoding mode-dependent arguments
        /// </summary>
        const string templateArguments = "{0} -c:v {5} -threads {1} -slices {2}{3}{4}{6}{7}{8}";

        /// <summary>
        /// {0} is video bitrate
        /// {1} is ' -fs XM' if X MB limit enabled otherwise blank
        /// {2} is buffer size (bitrate / 2)
        /// </summary>
        const string constantVideoArguments = " -minrate:v {0}K -b:v {0}K -maxrate:v {0}K -bufsize {2}K{1}";
        /// <summary>
        /// {0} is audio bitrate
        /// </summary>
        const string constantAudioArguments = " -b:a {0}K";

        /// <summary>
        /// {0} is qmin
        /// {1} is crf
        /// {2} is qmax
        /// </summary>
        const string variableVideoArguments = " -qmin {0} -crf {1} -qmax {2}";
        /// <summary>
        /// {0} is audio quality scale
        /// </summary>
        const string variableAudioArguments = " -qscale:a {0}";

        #endregion

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
        bool audioDisabled;

        public bool SarCompensate = false;
        public int SarWidth;
        public int SarHeight;

        #region MainForm

        public MainForm()
        {
            FFMSSharp.FFMS2.Initialize(Path.Combine(Environment.CurrentDirectory, "Binaries", "Win32"));

            InitializeComponent();

            if (Properties.Settings.Default.EncodingMode != EncodingMode.Constant)
            {
                boxVariable.Checked = true;
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            int threads = Environment.ProcessorCount;
            trackThreads.Value = Math.Min(trackThreads.Maximum, Math.Max(trackThreads.Minimum, threads));
        }

        void HandleDragEnter(object sender, DragEventArgs e)
        {
            // show copy cursor for files
            bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = dataPresent ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void HandleDragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            SetFile(files[0]);
        }

        void MainForm_Shown(object sender, EventArgs e)
        {
            clearToolTip();

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1) // We were "Open with..."ed with a file
                SetFile(args[1]);
#if !DEBUG
            CheckUpdate();
#endif
        }

        async void CheckUpdate()
        {
            try
            {
                var checker = new UpdateChecker("nixxquality", "WebMConverter");

                var update = await checker.CheckUpdate();

                if (update == UpdateType.None)
                {
                    this.InvokeIfRequired(() =>
                    {
                        showToolTip("Up to date!", 1000);
                    });
                }
                else
                {
                    this.InvokeIfRequired(() =>
                    {
                        var result = new UpdateNotifyDialog(checker).ShowDialog(this);
                        if (result == DialogResult.Yes)
                        {
                            checker.DownloadAsset("Converter.zip");
                            Application.Exit();
                        }
                    });
                }
            }
            catch (Exception e)
            {
                this.InvokeIfRequired(() =>
                {
                    showToolTip("Update checking failed! " + e.Message, 2000);
                });
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        void setToolTip(string message)
        {
            if (this.IsDisposed || toolStripStatusLabel.IsDisposed)
                return;

            this.InvokeIfRequired(() =>
            {
                toolStripStatusLabel.Text = message;
            });
        }

        [System.Diagnostics.DebuggerStepThrough]
        void showToolTip(string message, int timer = 0)
        {
            clearToolTip();

            setToolTip(message);

            if (timer > 0)
            {
                toolTipTimer = new StopWatch(timer);

                toolTipTimer.Elapsed += (sender, e) => clearToolTip();

                toolTipTimer.AutoReset = false;
                toolTipTimer.Enabled = true;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        void clearToolTip(object sender = null, EventArgs e = null)
        {
            if (toolTipTimer != null)
                toolTipTimer.Close();

            setToolTip("");
        }

        [System.Diagnostics.DebuggerStepThrough]
        void ToolStripItemTooltip(object sender, EventArgs e)
        {
            showToolTip((sender as ToolStripItem).AccessibleDescription);
        }

        [System.Diagnostics.DebuggerStepThrough]
        void ControlTooltip(object sender, EventArgs e)
        {
            showToolTip((sender as Control).AccessibleDescription);
        }

        #endregion

        #region groupMain

        void buttonBrowseIn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;

                if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    SetFile(dialog.FileName);
            }
        }

        void buttonBrowseOut_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.OverwritePrompt = true;
                dialog.ValidateNames = true;
                dialog.Filter = "WebM files|*.webm";

                if (dialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    textBoxOut.Text = dialog.FileName;
            }
        }

        void buttonGo_Click(object sender, EventArgs e)
        {
            if (indexing) // We are actually a cancel button right now.
            {
                indexbw.CancelAsync();
                (sender as Button).Enabled = false;
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

        void buttonPreview_Click(object sender, EventArgs e)
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

        #region tabProcessing

        void buttonCaption_Click(object sender, EventArgs e)
        {
            using (var form = new CaptionForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Caption = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Caption", "caption");
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonCrop_Click(object sender, EventArgs e)
        {
            using (var form = new CropForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Crop = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Crop", "crop");
                        SetSlices();
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonMultipleTrim_Click(object sender, EventArgs e)
        {
            using (var form = new MultipleTrimForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.MultipleTrim = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Multiple Trim", "trim");
                        UpdateArguments(sender, e);
                        (sender as ToolStripMenuItem).OwnerItem.Enabled = false;
                    }
                }
            }
        }

        void buttonOverlay_Click(object sender, EventArgs e)
        {
            using (var form = new OverlayForm())
            {
                if (form.IsDisposed) // The user cancelled the file picker
                    return;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Overlay = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Overlay", "overlay");
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonResize_Click(object sender, EventArgs e)
        {
            using (var form = new ResizeForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Resize = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Resize", "resize");
                        SetSlices();
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonReverse_Click(object sender, EventArgs e)
        {
            if (boxAdvancedScripting.Checked)
            {
                textBoxProcessingScript.AppendText(Environment.NewLine + new ReverseFilter().ToString());
            }
            else
            {
                Filters.Reverse = new ReverseFilter();
                listViewProcessingScript.Items.Add("Reverse", "reverse");
                (sender as ToolStripItem).Enabled = false;
            }
        }

        void buttonSubtitle_Click(object sender, EventArgs e)
        {
            using (var form = new SubtitleForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Subtitle = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Subtitle", "subtitles");
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonTrim_Click(object sender, EventArgs e)
        {
            using (var form = new TrimForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (boxAdvancedScripting.Checked)
                    {
                        textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter.ToString());
                    }
                    else
                    {
                        Filters.Trim = form.GeneratedFilter;
                        listViewProcessingScript.Items.Add("Trim", "trim");
                        UpdateArguments(sender, e);
                        (sender as ToolStripItem).Enabled = false;
                    }
                }
            }
        }

        void buttonExportProcessing_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "AviSynth script (*.avs)|*.avs";
            dialog.FileName = Path.GetFileName(Path.ChangeExtension(Program.InputFile, "avs"));

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Generate the script if we're in simple mode
                if (!boxAdvancedScripting.Checked)
                    GenerateAvisynthScript();

                WriteAvisynthScript(dialog.FileName, Program.InputFile);
            }
        }

        void boxAdvancedScripting_Click(object sender, EventArgs e)
        {
            ProbeScript();
            (sender as ToolStripButton).Checked = !(sender as ToolStripButton).Checked;

            listViewProcessingScript.Hide();
            GenerateAvisynthScript();
            textBoxProcessingScript.Show();
            toolStripFilterButtonsEnabled(true);

            (sender as ToolStripButton).Enabled = false;
            clearToolTip(sender, e);
        }

        void toolStripFilterButtonsEnabled(bool enabled)
        {
            buttonCaption.Enabled = enabled;
            buttonCrop.Enabled = enabled;
            buttonOverlay.Enabled = enabled;
            buttonResize.Enabled = enabled;
            buttonReverse.Enabled = enabled;
            buttonSubtitle.Enabled = enabled;
            buttonTrim.Enabled = enabled;
            buttonExportProcessing.Enabled = enabled;
        }

        void listViewProcessingScript_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem item in (sender as ListView).SelectedItems)
                {
                    switch (item.Text)
                    {
                        case "Caption":
                            Filters.Caption = null;
                            buttonCaption.Enabled = true;
                            break;
                        case "Crop":
                            Filters.Crop = null;
                            buttonCrop.Enabled = true;
                            SetSlices();
                            break;
                        case "Multiple Trim":
                            Filters.MultipleTrim = null;
                            buttonTrim.Enabled = true;
                            GenerateArguments();
                            break;
                        case "Overlay":
                            Filters.Overlay = null;
                            buttonOverlay.Enabled = true;
                            break;
                        case "Resize":
                            Filters.Resize = null;
                            buttonResize.Enabled = true;
                            SetSlices();
                            break;
                        case "Reverse":
                            Filters.Reverse = null;
                            buttonReverse.Enabled = true;
                            break;
                        case "Subtitle":
                            Filters.Subtitle = null;
                            buttonSubtitle.Enabled = true;
                            break;
                        case "Trim":
                            Filters.Trim = null;
                            buttonTrim.Enabled = true;
                            GenerateArguments();
                            break;
                    }
                    (sender as ListView).Items.Remove(item);
                }
            }
        }

        void listViewProcessingScript_ItemActivate(object sender, EventArgs e)
        {
            switch ((sender as ListView).FocusedItem.Text)
            {
                case "Caption":
                    using (var form = new CaptionForm(Filters.Caption))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Caption = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Crop":
                    using (var form = new CropForm(Filters.Crop))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Crop = form.GeneratedFilter;
                            SetSlices();
                        }
                    }
                    break;
                case "Multiple Trim":
                    using (var form = new MultipleTrimForm(Filters.MultipleTrim))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.MultipleTrim = form.GeneratedFilter;
                            UpdateArguments(sender, e);
                        }
                    }
                    break;
                case "Overlay":
                    using (var form = new OverlayForm(Filters.Overlay))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Overlay = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Resize":
                    using (var form = new ResizeForm(Filters.Resize))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Resize = form.GeneratedFilter;
                            SetSlices();
                        }
                    }
                    break;
                case "Subtitle":
                    using (var form = new SubtitleForm(Filters.Subtitle))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Subtitle = form.GeneratedFilter;
                        }
                    }
                    break;
                case "Trim":
                    using (var form = new TrimForm(Filters.Trim))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            Filters.Trim = form.GeneratedFilter;
                            UpdateArguments(sender, e);
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

        [System.Diagnostics.DebuggerStepThrough]
        void listViewProcessingScript_MouseEnter(object sender, EventArgs e)
        {
            showToolTip("");
        }

        #endregion

        #region tabEncoding

        void textBoxNumbersOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        EncodingMode encodingMode = EncodingMode.Constant;

        void boxConstant_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked) return;

            tableVideoConstantOptions.BringToFront();
            tableAudioConstantOptions.BringToFront();
            encodingMode = EncodingMode.Constant;

            buttonVariableDefault.Visible = false;
            if (encodingMode != Properties.Settings.Default.EncodingMode)
            {
                buttonConstantDefault.Visible = true;
            }

            UpdateArguments(sender, e);
        }

        void buttonConstantDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EncodingMode = EncodingMode.Constant;
            Properties.Settings.Default.Save();
            buttonConstantDefault.Visible = false;

            showToolTip("Saved!", 1000);
        }

        void boxVariable_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked) return;

            tableVideoVariableOptions.BringToFront();
            tableAudioVariableOptions.BringToFront();
            encodingMode = EncodingMode.Variable;

            buttonConstantDefault.Visible = false;
            if (encodingMode != Properties.Settings.Default.EncodingMode)
            {
                buttonVariableDefault.Visible = true;
            }

            UpdateArguments(sender, e);
        }

        void buttonVariableDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EncodingMode = EncodingMode.Variable;
            Properties.Settings.Default.Save();
            buttonVariableDefault.Visible = false;

            showToolTip("Saved!", 1000);
        }

        void boxAudio_CheckedChanged(object sender, EventArgs e)
        {
            numericAudioQuality.Enabled = boxAudioBitrate.Enabled = (sender as CheckBox).Checked;
            UpdateArguments(sender, e);
        }

        #endregion

        #region tabAdvanced

        void boxLevels_CheckedChanged(object sender, EventArgs e)
        {
            Filters.Levels = (sender as CheckBox).Checked ? new LevelsFilter() : null;
        }

        void boxDeinterlace_CheckedChanged(object sender, EventArgs e)
        {
            Filters.Deinterlace = (sender as CheckBox).Checked ? new DeinterlaceFilter() : null;
        }

        void boxDenoise_CheckedChanged(object sender, EventArgs e)
        {
            Filters.Denoise = (sender as CheckBox).Checked ? new DenoiseFilter() : null;
        }

        void trackThreads_ValueChanged(object sender, EventArgs e)
        {
            labelThreads.Text = (sender as TrackBar).Value.ToString();
            UpdateArguments(sender, e);
        }

        void trackSlices_ValueChanged(object sender, EventArgs e)
        {
            labelSlices.Text = GetSlices().ToString();
            UpdateArguments(sender, e);
        }

        #endregion

        #region Functions

        char[] invalidChars = Path.GetInvalidPathChars();

        void SetFile(string path)
        {
            try
            {
                ValidateInputFile(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textBoxIn.Text = path;
            string fullPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            if (boxTitle.Text == _autoTitle || boxTitle.Text == "")
                boxTitle.Text = _autoTitle = name;
            if (textBoxOut.Text == _autoOutput || textBoxOut.Text == "")
                textBoxOut.Text = _autoOutput = Path.Combine(fullPath, name + ".webm");
            audioDisabled = false;

            progressBarIndexing.Style = ProgressBarStyle.Marquee;
            progressBarIndexing.Value = 30;
            panelHideTheOptions.BringToFront();

            buttonGo.Enabled = false;
            buttonPreview.Enabled = false;
            buttonBrowseIn.Enabled = false;
            textBoxIn.Enabled = false;

            if (Path.GetExtension(path) == ".avs")
            {
                Program.InputFile = path;
                Program.InputType = FileType.Avisynth;

                BackgroundWorker probebw = new BackgroundWorker();
                probebw.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    ProbeScript();
                };
                probebw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    boxAdvancedScripting.Enabled = false;
                    listViewProcessingScript.Enabled = false;
                    showToolTip("You're loading an AviSynth script, so Processing is disabled!", 3000);

                    boxLevels.Enabled = boxDeinterlace.Enabled = boxDenoise.Enabled = false;
                    buttonGo.Enabled = buttonBrowseIn.Enabled = textBoxIn.Enabled = true;
                    toolStripFilterButtonsEnabled(false);
                    panelHideTheOptions.SendToBack();
                };

                labelIndexingProgress.Text = "Probing script...";
                probebw.RunWorkerAsync();
                return;
            }
            else
            {
                Program.InputFile = path;
                Program.InputType = FileType.Video;
                Program.FileMd5 = null;
                listViewProcessingScript.Enabled = true;
                boxLevels.Enabled = boxDeinterlace.Enabled = boxDenoise.Enabled = true;
            }

            // Reset filters
            Filters.ResetFilters();
            listViewProcessingScript.Clear();
            boxAdvancedScripting.Checked = false; // STUB: this part is weak
            boxAdvancedScripting.Enabled = true;
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
                    if (audioDisabled) // Indexing failed because of the audio, so the user disabled it.
                    {
                        List<int> indexList = new List<int>(); // An empty list means index no tracks.
                        index = indexer.Index(indexList);
                    }
                    else
                    {
                        index = indexer.Index();
                    }
                }
                catch (OperationCanceledException)
                {
                    audioDisabled = false; // This enables us to cancel the bw even if audio was disabled by the user.
                    e.Cancel = true;
                    return;
                }
                catch (Exception error)
                {
                    if (error.Message.StartsWith("Audio format change detected"))
                    {
                        const string message = "\nIf you were planning on making a WebM with audio, I'm afraid that's not going to happen.\nWould you like to index the file without audio?";
                        const string caption = "Error";
                        var result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                        audioDisabled = (result == DialogResult.OK);
                    }
                    else
                    {
                        MessageBox.Show(error.Message);
                    }
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
                    switch (index.GetTrack(i).TrackType)
                    {
                        case FFMSSharp.TrackType.Video:
                            videoTracks.Add(i);
                            break;
                        case FFMSSharp.TrackType.Audio:
                            audioTracks.Add(i);
                            break;
                    }
                }

                if (videoTracks.Count == 0)
                    throw new Exception("No video tracks found!");

                else if (videoTracks.Count == 1)
                {
                    videotrack = videoTracks[0];
                }
                else
                {
                    this.InvokeIfRequired(() =>
                    {
                        var dialog = new TrackSelectDialog("Video", videoTracks);
                        dialog.ShowDialog(this);
                        videotrack = dialog.SelectedTrack;
                    });
                }

                if (!audioDisabled)
                {
                    if (audioTracks.Count == 0)
                    {
                        audioDisabled = true;
                    }
                    else if (audioTracks.Count == 1)
                    {
                        audiotrack = audioTracks[0];
                        audioDisabled = false;
                    }
                    else
                    {
                        this.InvokeIfRequired(() =>
                        {
                            var dialog = new TrackSelectDialog("Audio", audioTracks);
                            dialog.ShowDialog(this);
                            audiotrack = dialog.SelectedTrack;
                        });
                        audioDisabled = false;
                    }
                }

                Program.AttachmentDirectory = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".attachments");
                Directory.CreateDirectory(Program.AttachmentDirectory);

                using (var prober = new FFprobe(Program.InputFile, format: "", argument: "-show_streams"))
                {
                    string streamInfo = prober.Probe();
                    Program.SubtitleTracks = new Dictionary<int, string>();

                    using (var s = new StringReader(streamInfo))
                    {
                        var doc = new XPathDocument(s);

                        foreach (XPathNavigator nav in doc.CreateNavigator().Select("//ffprobe/streams/stream"))
                        {
                            int streamindex;
                            string title;

                            streamindex = int.Parse(nav.GetAttribute("index", ""));

                            switch (nav.GetAttribute("codec_type", ""))
                            {
                                case "video": // Probe for sample aspect ratio
                                    if (streamindex != videotrack) break;
                                    string[] sar = null, dar = null;

                                    sar = nav.GetAttribute("sample_aspect_ratio", "").Split(':');
                                    if (sar[0] == "1" && sar[1] == "1") break;

                                    dar = nav.GetAttribute("display_aspect_ratio", "").Split(':');

                                    float SarNum, SarDen, DarNum, DarDen;
                                    SarNum = float.Parse(sar[0]);
                                    SarDen = float.Parse(sar[1]);
                                    DarNum = float.Parse(dar[0]);
                                    DarDen = float.Parse(dar[1]);

                                    SarWidth = int.Parse(nav.GetAttribute("width", ""));
                                    SarHeight = int.Parse(nav.GetAttribute("height", ""));
                                    if (SarNum != DarNum)
                                    {
                                        SarHeight = (int)(SarHeight / (SarNum / SarDen));
                                    }
                                    if (SarDen != DarDen)
                                    {
                                        SarWidth = (int)(SarWidth * (SarNum / SarDen));
                                    }
                                    SarCompensate = true;
                                    break;
                                case "subtitle": // Extract the subtitle file
                                    string file = Path.Combine(Program.AttachmentDirectory, string.Format("sub{0}.ass", streamindex));

                                    if (!File.Exists(file)) // If we didn't extract it already
                                    {
                                        string arg = string.Format("-i \"{0}\" -map 0:{1} \"{2}\" -y", Program.InputFile, streamindex, file);

                                        using (var ffmpeg = new FFmpeg(arg))
                                        {
                                            ffmpeg.Start();
                                            ffmpeg.WaitForExit();
                                        }
                                    }

                                    if (!File.Exists(file)) // Holy shit, it still doesn't exist?
                                        break; // Whatever, skip it.

                                    // Get a title
                                    title = nav.GetAttribute("codec_name", "");

                                    if (!nav.IsEmptyElement) // There might be a tag element
                                    {
                                        nav.MoveTo(nav.SelectSingleNode(".//tag[@key='title']"));
                                        var titleTag = nav.GetAttribute("value", "");

                                        title = titleTag == "" ? title : titleTag;
                                    }

                                    // Save it
                                    Program.SubtitleTracks.Add(streamindex, title);
                                    break;
                            }
                        }
                    }
                }

                // Extract attachments (internal fonts)
                using (var ffmpeg = new FFmpeg(string.Format("-dump_attachment:t \"\" -y -i \"{0}\"", Program.InputFile)))
                {
                    ffmpeg.StartInfo.WorkingDirectory = Program.AttachmentDirectory;
                    ffmpeg.Start();
                    ffmpeg.WaitForExit();
                }

                Program.VideoSource = index.VideoSource(path, videotrack);
                var frame = Program.VideoSource.GetFrame(0); // We're assuming that the entire video has the same settings here, which should be fine. (These options usually don't vary, I hope.)
                Program.VideoColorRange = frame.ColorRange;
                Program.VideoInterlaced = frame.InterlacedFrame;
                SetSlices(frame.EncodedResolution);
            });
            indexbw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                if (audioDisabled == true && e.Cancelled)
                {
                    indexbw.RunWorkerAsync();
                    return;
                }

                indexing = false;
                buttonGo.Enabled = false;
                buttonGo.Text = "Convert";

                if (e.Cancelled)
                {
                    CancelIndexing();
                }
                else
                {
                    labelIndexingProgress.Text = "Extracting subtitle tracks and attachments...";
                    progressBarIndexing.Value = 30;
                    progressBarIndexing.Style = ProgressBarStyle.Marquee;

                    extractbw.RunWorkerAsync();
                }
            };
            extractbw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    CancelIndexing();
                    const string text = "We couldn't find any video tracks!\nPlease use another input file.";
                    const string caption = "ERROR";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (audioDisabled)
                {
                    const string text = "We couldn't find any audio tracks.\nIf you want sound, please use another input file.\nIf you don't want audio in your output webm, there's nothing to worry about.";
                    const string caption = "FYI";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    boxAudio.Enabled = false;
                }

                buttonGo.Enabled = true;
                buttonPreview.Enabled = true;
                buttonBrowseIn.Enabled = true;
                textBoxIn.Enabled = true;
                toolStripFilterButtonsEnabled(true);

                if (Program.VideoColorRange == FFMSSharp.ColorRange.MPEG)
                    //boxLevels.Checked = true;
                    if (Program.VideoInterlaced)
                        boxDeinterlace.Checked = true;

                panelHideTheOptions.SendToBack();
            };

            if (File.Exists(_indexFile))
            {
                try
                {
                    index = new FFMSSharp.Index(_indexFile);

                    if (index.BelongsToFile(path))
                    {
                        try
                        {
                            index.GetFirstIndexedTrackOfType(FFMSSharp.TrackType.Audio);
                        }
                        catch (KeyNotFoundException)
                        {
                            audioDisabled = true;
                        }

                        labelIndexingProgress.Text = "Extracting subtitle tracks and attachments...";
                        progressBarIndexing.Value = 30;
                        progressBarIndexing.Style = ProgressBarStyle.Marquee;

                        extractbw.RunWorkerAsync();
                        return;
                    }
                }
                catch
                {
                    File.Delete(_indexFile);
                }
            }

            indexing = true;
            buttonGo.Enabled = true;
            buttonGo.Text = "Cancel";
            progressBarIndexing.Style = ProgressBarStyle.Continuous;
            labelIndexingProgress.Text = "Indexing...";
            indexbw.RunWorkerAsync();
        }

        void CancelIndexing()
        {
            textBoxIn.Text = "";
            textBoxOut.Text = "";
            Program.InputFile = null;
            Program.FileMd5 = null;
            buttonBrowseIn.Enabled = true;
            textBoxIn.Enabled = true;
            panelHideTheOptions.SendToBack();
        }

        void ValidateInputFile(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new Exception("No input file!");

            if (invalidChars.Any(input.Contains))
                throw new Exception("Input path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars));

            if (!File.Exists(input))
                throw new Exception("Input file doesn't exist!");
        }

        void ValidateOutputFile(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
                throw new Exception("No output file!");

            if (invalidChars.Any(output.Contains))
                throw new Exception("Output path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars));
        }

        void UpdateArguments(object sender, EventArgs e)
        {
            if (Program.InputFile == null)
                return;

            try
            {
                string arguments = GenerateArguments();
                if (arguments != _autoArguments || _argumentError)
                {
                    boxArguments.Text = _autoArguments = arguments;
                    showToolTip(arguments, 5000);
                    _argumentError = false;
                }
            }
            catch (ArgumentException argExc)
            {
                boxArguments.Text = "ERROR: " + argExc.Message;
                showToolTip("ERROR: " + argExc.Message, 5000);
                _argumentError = true;
            }
        }

        void WriteAvisynthScript(string avsFileName, string avsInputFile)
        {
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine(string.Format("PluginPath = \"{0}\\\"", Path.Combine(Environment.CurrentDirectory, "Binaries", "Win32")));
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadCPlugin(PluginPath+\"assrender.dll\")");

                // UTF-8 argument bug workaround
                // see https://github.com/FFMS/ffms2/pull/167
                avscript.WriteLine(string.Format("FFIndex(\"{0}\", cachefile=\"{1}\", utf8=True)", avsInputFile, _indexFile));
                // this won't actually index file file (we already did), but it will call FFMS_Init with utf8 = true properly, unlike FFVideoSource

                if (boxAudio.Checked)
                    avscript.WriteLine(string.Format("AudioDub(FFVideoSource(\"{0}\",cachefile=\"{1}\",track={2}), FFAudioSource(\"{0}\",cachefile=\"{1}\",track={3}))", avsInputFile, _indexFile, videotrack, audiotrack));
                else
                    avscript.WriteLine(string.Format("FFVideoSource(\"{0}\",cachefile=\"{1}\",track={2})", avsInputFile, _indexFile, videotrack));

                if (Filters.Deinterlace != null)
                {
                    avscript.WriteLine("LoadPlugin(PluginPath+\"TDeint.dll\")");
                    avscript.WriteLine(Filters.Deinterlace.ToString());
                }

                if (Filters.Denoise != null)
                {
                    avscript.WriteLine("LoadPlugin(PluginPath+\"hqdn3d.dll\")");
                    avscript.WriteLine(Filters.Denoise.ToString());
                }

                if (Filters.Levels != null)
                    avscript.WriteLine(Filters.Levels.ToString());

                if (SarCompensate)
                    avscript.WriteLine(new ResizeFilter(SarWidth, SarHeight));

                avscript.Write(textBoxProcessingScript.Text);
            }
        }

        void Preview()
        {
            string input = textBoxIn.Text;
            buttonPreview.Enabled = false;

            ValidateInputFile(input);

            // Generate the script if we're in simple mode
            if (!boxAdvancedScripting.Checked)
                GenerateAvisynthScript();

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            WriteAvisynthScript(avsFileName, input);

            // Run ffplay
            var ffplay = new FFplay(string.Format("-window_title Preview -loop 0 -f avisynth \"{0}\"", avsFileName));
            ffplay.Exited += delegate
            {
                this.InvokeIfRequired(() =>
                {
                    buttonPreview.Enabled = true;
                });
            };
            ffplay.Start();
        }

        void Convert()
        {
            string input = textBoxIn.Text;
            string output = textBoxOut.Text;

            if (input == output)
                throw new Exception("Input and output files are the same!");

            string options = boxArguments.Text;

            ValidateInputFile(input);
            ValidateOutputFile(output);

            if (options.Trim() == "" || _argumentError)
                options = GenerateArguments();

            string avsFileName = null;
            switch (Program.InputType)
            {
                case FileType.Video:
                    // Generate the script if we're in simple mode
                    if (!boxAdvancedScripting.Checked)
                        GenerateAvisynthScript();

                    // Make our temporary file for the AviSynth script
                    avsFileName = Path.GetTempFileName();
                    WriteAvisynthScript(avsFileName, input);
                    break;
                case FileType.Avisynth:
                    avsFileName = Program.InputFile;
                    break;
            }

            string[] arguments;
            if (!boxHQ.Checked)
                arguments = new[] { string.Format(template, output, options, "", "") };
            else
            {
                arguments = new string[2]; //           vvv is Windows only
                arguments[0] = string.Format(template, "NUL", options, string.Format(passArgument, 1, Path.Combine(Path.GetTempPath(), "ffmpeg2pass")));
                arguments[1] = string.Format(template, output, options, string.Format(passArgument, 2, Path.Combine(Path.GetTempPath(), "ffmpeg2pass")));

                if (!arguments[0].Contains("-an")) // skip audio encoding on the first pass
                    arguments[0] = arguments[0].Replace("-c:v libvpx", "-an -c:v libvpx"); // ugly as hell
            }

            new ConverterDialog(this, avsFileName, arguments).ShowDialog(this);
        }

        string GenerateArguments()
        {
            string qualityarguments = null;
            switch (encodingMode)
            {
                case EncodingMode.Constant:
                    float limit = 0;
                    string limitTo = string.Empty;
                    if (!string.IsNullOrWhiteSpace(boxLimit.Text))
                    {
                        if (!float.TryParse(boxLimit.Text, out limit))
                            throw new ArgumentException("Invalid size limit!");
                        limitTo = string.Format(" -fs {0}M", limit.ToString(CultureInfo.InvariantCulture)); //Should turn comma into dot
                    }

                    int audiobitrate = -1;
                    if (boxAudio.Checked)
                        audiobitrate = 64;

                    if (!string.IsNullOrWhiteSpace(boxAudioBitrate.Text))
                    {
                        if (!int.TryParse(boxAudioBitrate.Text, out audiobitrate))
                            throw new ArgumentException("Invalid audio bitrate!");

                        if (audiobitrate < 45)
                            throw new ArgumentException("Audio bitrate is too low! It has to be at least 45Kb/s");

                        if (audiobitrate > 500)
                            throw new ArgumentException("Audio bitrate is too high! It can not be higher than 500Kb/s");
                    }

                    int videobitrate = 900;
                    if (limitTo != string.Empty)
                    {
                        double duration = GetDuration();

                        if (duration > 0)
                            videobitrate = (int)(8192 * limit / duration) - audiobitrate;

                        if (videobitrate < 0)
                            throw new ArgumentException("Audio bitrate is too high! With that size limit, you won't be able to fit any video!");
                    }
                    if (!string.IsNullOrWhiteSpace(boxBitrate.Text))
                    {
                        if (!int.TryParse(boxBitrate.Text, out videobitrate))
                            throw new ArgumentException("Invalid video bitrate!");
                    }

                    qualityarguments = string.Format(constantVideoArguments, videobitrate, limitTo, videobitrate / 2);
                    if (audiobitrate != -1)
                        qualityarguments += string.Format(constantAudioArguments, audiobitrate);

                    break;
                case EncodingMode.Variable:
                    int qmin = Math.Max(0, (int)(numericCrf.Value - numericCrfTolerance.Value));
                    int qmax = Math.Min(63, (int)(numericCrf.Value + numericCrfTolerance.Value));

                    qualityarguments = string.Format(variableVideoArguments, qmin, numericCrf.Value, qmax);
                    if (boxAudio.Checked)
                        qualityarguments += string.Format(variableAudioArguments, numericAudioQuality.Value);

                    break;
            }

            int threads = trackThreads.Value;
            int slices = GetSlices();

            string metadataTitle = "";
            if (!string.IsNullOrWhiteSpace(boxTitle.Text))
                metadataTitle = string.Format(" -metadata title=\"{0}\"", boxTitle.Text.Replace("\"", "\\\""));

            string HQ = "";
            if (boxHQ.Checked)
                HQ = " -quality best -lag-in-frames 16 -auto-alt-ref 1";

            string vcodec;
            string acodec;
            if (boxNGOV.Checked)
            {
                vcodec = "libvpx-vp9";
                acodec = "libopus";
            }
            else
            {
                vcodec = "libvpx";
                acodec = "libvorbis";
            }

            string audioEnabled;
            if (boxAudio.Checked)
            {
                audioEnabled = "";
                acodec = " -ac 2 -c:a " + acodec;
            }
            else
            {
                audioEnabled = acodec = "";
            }

            string framerate = "";
            if (!string.IsNullOrWhiteSpace(boxFrameRate.Text))
            {
                framerate = " -r " + boxFrameRate.Text;
            }

            return string.Format(templateArguments, audioEnabled, threads, slices, metadataTitle, HQ, vcodec, acodec, framerate, qualityarguments);
        }

        /// <summary>
        /// Attempt to calculate the duration of the Avisynth script.
        /// </summary>
        /// <returns>The duration or -1 if automatic detection was unsuccessful.</returns>
        public double GetDuration()
        {
            if (boxAdvancedScripting.Checked || Program.InputType == FileType.Avisynth)
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
                if (Filters.MultipleTrim != null)
                    return Filters.MultipleTrim.GetDuration();

                return Program.FrameToTimeSpan(Program.VideoSource.NumberOfFrames - 1).TotalSeconds;
            }
        }

        public Size GetResolution()
        {
            if (boxAdvancedScripting.Checked || Program.InputType == FileType.Avisynth)
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

        void GenerateAvisynthScript()
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("# This is an AviSynth script. You may write advanced commands below, or just press the buttons above for smooth sailing.");
            if (Filters.Subtitle != null)
                script.AppendLine(Filters.Subtitle.ToString());
            if (Filters.Caption != null)
            {
                Filters.Caption.BeforeEncode(Program.VideoSource.GetFrame(0).EncodedResolution);
                script.AppendLine(Filters.Caption.ToString());
            }
            if (Filters.Overlay != null)
                script.AppendLine(Filters.Overlay.ToString());
            if (Filters.Trim != null)
                script.AppendLine(Filters.Trim.ToString());
            if (Filters.MultipleTrim != null)
                script.AppendLine(Filters.MultipleTrim.ToString());
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
            string avsFileName = null;

            switch (Program.InputType)
            {
                case FileType.Video:
                    // Make our temporary file for the AviSynth script
                    avsFileName = Path.GetTempFileName();
                    WriteAvisynthScript(avsFileName, textBoxIn.Text);
                    break;
                case FileType.Avisynth:
                    avsFileName = Program.InputFile;
                    break;
            }

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
            SetSlices(GetResolution());
        }

        void SetSlices(Size resolution)
        {
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

            this.InvokeIfRequired(() =>
            {
                trackSlices.Value = slices;
            });
        }

        #endregion
    }

    public enum EncodingMode
    {
        Constant,
        Variable
    }
}
