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
using WebMConverter.Components;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        #region FFmpeg argument base strings

        /// <summary>
        /// {0} is output file
        /// {1} is TemplateArguments
        /// {2} is PassArgument if HQ mode enabled, otherwise blank
        /// </summary>
        private const string Template = " {1}{2} -f webm -y \"{0}\"";

        /// <summary>
        /// {0} is pass number (1 or 2)
        /// {1} is the prefix for the pass .log file
        /// </summary>
        private const string PassArgument = " -pass {0} -passlogfile \"{1}\"";

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
        private const string TemplateArguments = "{0} -c:v {5} -pix_fmt yuv420p -threads {1} -slices {2}{3}{4}{6}{7}{8}";

        /// <summary>
        /// {0} is video bitrate
        /// {1} is ' -fs XM' if X MB limit enabled otherwise blank
        /// {2} is bufsize
        /// {3} is rc_init_occupancy
        /// </summary>
        private const string ConstantVideoArguments = " -minrate:v {0}K -b:v {0}K -maxrate:v {0}K -bufsize {2}K -rc_init_occupancy {3}K -qcomp 1{1}";
        /// <summary>
        /// {0} is audio bitrate
        /// </summary>
        private const string ConstantAudioArguments = " -b:a {0}K";

        /// <summary>
        /// {0} is qmin
        /// {1} is crf
        /// {2} is qmax
        /// </summary>
        private const string VariableVideoArguments = " -qmin {0} -crf {1} -qmax {2} -qcomp 0";
        /// <summary>
        /// {0} is audio quality scale
        /// </summary>
        private const string VariableAudioArguments = " -qscale:a {0}";

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
        FileStream inputFile;

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

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inputFile != null)
                inputFile.Close();
            UnloadFonts();
        }

        void boxIndexingProgressDetails_CheckedChanged(object sender, EventArgs e)
        {
            boxIndexingProgress.Visible = (sender as CheckBox).Checked;
            panelContainTheProgressBar.Location = new Point(
                panelHideTheOptions.ClientSize.Width / 2 - panelContainTheProgressBar.Size.Width / 2,
                panelHideTheOptions.ClientSize.Height / 2 - panelContainTheProgressBar.Size.Height / 2);
        }

        const int _boxIndexingProgressMaxLines = 11;
        void logIndexingProgress(string message)
        {
#if DEBUG
            Console.WriteLine(message);
#endif
            this.InvokeIfRequired(() =>
            {
                boxIndexingProgress.Text += message + Environment.NewLine;
                var indexingProgressLines = boxIndexingProgress.Text.Split('\n');
                if (indexingProgressLines.Length > _boxIndexingProgressMaxLines)
                {
                    boxIndexingProgress.Text = String.Join(Environment.NewLine, indexingProgressLines.Skip(1));
                }
            });
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

        private void opusQualityScalingTooltip()
        {
            if (boxNGOV.Checked && boxAudio.Checked && boxVariable.Checked)
                showToolTip("Audio quality scaling disabled -- Opus doesn't support it!", 5000);
        }

        #endregion

        #region groupMain

        void buttonBrowseIn_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;
                dialog.InitialDirectory = Properties.Settings.Default.RememberedFolderIn;

                if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                    return;

                SetFile(dialog.FileName);
                Properties.Settings.Default.RememberedFolderIn = Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();
            }
        }

        void buttonBrowseOut_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.OverwritePrompt = true;
                dialog.ValidateNames = true;
                dialog.Filter = "WebM files|*.webm";
                dialog.InitialDirectory = Properties.Settings.Default.RememberedFolderOut;

                if (dialog.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.FileName))
                    return;

                textBoxOut.Text = dialog.FileName;
                Properties.Settings.Default.RememberedFolderOut = Path.GetDirectoryName(dialog.FileName);
                Properties.Settings.Default.Save();
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

        private void buttonDub_Click(object sender, EventArgs e)
        {
            using (var form = new DubForm())
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                if (boxAdvancedScripting.Checked)
                {
                    textBoxProcessingScript.AppendText(Environment.NewLine + form.GeneratedFilter);
                }
                else
                {
                    Filters.Dub = form.GeneratedFilter;
                    listViewProcessingScript.Items.Add("Dub", "dub");

                    if (Filters.Dub.Mode != DubMode.TrimAudio) // the video duration may have changed
                        UpdateArguments(sender, e);

                    ((ToolStripItem)sender).Enabled = false;
                }
                boxAudio.Checked = boxAudio.Enabled = true;
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
            boxAudio.Enabled = true;

            (sender as ToolStripButton).Enabled = false;
            clearToolTip(sender, e);
        }

        void toolStripFilterButtonsEnabled(bool enabled)
        {
            buttonCaption.Enabled = enabled;
            buttonCrop.Enabled = enabled;
            buttonDub.Enabled = enabled;
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
                        case @"Dub":
                            var oldfilter = Filters.Dub;
                            Filters.Dub = null;
                            buttonDub.Enabled = true;
                            if (oldfilter.Mode != DubMode.TrimAudio) // the video duration may have changed
                                UpdateArguments(sender, e);
                            if (!Program.InputHasAudio)
                                boxAudio.Checked = boxAudio.Enabled = false;
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
                case @"Dub":
                    using (var form = new DubForm(Filters.Dub))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            var oldfilter = Filters.Dub;
                            Filters.Dub = form.GeneratedFilter;
                            if (oldfilter.Mode != DubMode.TrimAudio || Filters.Dub.Mode != DubMode.TrimAudio) // the video duration may have changed
                                UpdateArguments(sender, e);
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

            opusQualityScalingTooltip();
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
            numericAudioQuality.Enabled = boxAudioBitrate.Enabled = ((CheckBox)sender).Checked;
            
            if (boxNGOV.Checked)
                numericAudioQuality.Enabled = false;

            UpdateArguments(sender, e);

            opusQualityScalingTooltip();
        }

        #endregion

        #region tabAdvanced

        void comboLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboLevels.SelectedIndex)
            {
                case 0: // Leave them alone
                    Filters.Levels = null;
                    break;
                case 1: // TV -> PC
                    Filters.Levels = new LevelsFilter(LevelsConversion.TVtoPC);
                    break;
                case 2: // PC -> TV
                    Filters.Levels = new LevelsFilter(LevelsConversion.PCtoTV);
                    break;
            }
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

        private void boxNGOV_CheckedChanged(object sender, EventArgs e)
        {
            numericAudioQuality.Enabled = !(sender as CheckBox).Checked;
            UpdateArguments(sender, e);

            opusQualityScalingTooltip();
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

            if (inputFile != null)
                inputFile.Close();
            UnloadFonts();

            textBoxIn.Text = path;
            string fullPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string title = name;
            if (textBoxOut.Text == _autoOutput || textBoxOut.Text == "")
                textBoxOut.Text = _autoOutput = Path.Combine(string.IsNullOrWhiteSpace(Properties.Settings.Default.RememberedFolderOut) ? fullPath : Properties.Settings.Default.RememberedFolderOut, name + ".webm");
            audioDisabled = false;

            progressBarIndexing.Style = ProgressBarStyle.Marquee;
            progressBarIndexing.Value = 30;
            boxIndexingProgress.Text = "";
            panelHideTheOptions.BringToFront();

            buttonGo.Enabled = false;
            buttonPreview.Enabled = false;
            buttonBrowseIn.Enabled = false;
            textBoxIn.Enabled = false;

            inputFile = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Reset filters
            Filters.ResetFilters();
            listViewProcessingScript.Clear();
            boxAdvancedScripting.Checked = false; // STUB: this part is weak
            boxAdvancedScripting.Enabled = true;
            textBoxProcessingScript.Hide();
            listViewProcessingScript.Show();

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

                    comboLevels.Enabled = boxDeinterlace.Enabled = boxDenoise.Enabled = false;
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
                comboLevels.Enabled = boxDeinterlace.Enabled = boxDenoise.Enabled = true;
                comboLevels.SelectedIndex = 0;
            }

            GenerateAvisynthScript();

            // Hash some of the file to make sure we didn't index it already
            labelIndexingProgress.Text = "Hashing...";
            logIndexingProgress("Hashing...");
            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(path))
            {
                var filename = new UTF8Encoding().GetBytes(name);
                var buffer = new byte[4096];

                filename.CopyTo(buffer, 0);
                stream.Read(buffer, filename.Length, 4096 - filename.Length);

                Program.FileMd5 = BitConverter.ToString(md5.ComputeHash(buffer));
                logIndexingProgress("File hash is " + Program.FileMd5.Replace("-", ""));
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
                logIndexingProgress("Indexing starting...");
                FFMSSharp.Indexer indexer = new FFMSSharp.Indexer(path, FFMSSharp.Source.Lavf);

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
                        DialogResult result = DialogResult.Cancel;

                        this.InvokeIfRequired(() =>
                        {
                            result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        });

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
                logIndexingProgress("Extraction starting...");

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
                        logIndexingProgress("Waiting for user input...");
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
                            logIndexingProgress("Waiting for user input...");
                            dialog.ShowDialog(this);
                            audiotrack = dialog.SelectedTrack;
                        });
                        audioDisabled = false;
                    }
                }

                Program.AttachmentDirectory = Path.Combine(Path.GetTempPath(), Program.FileMd5 + ".attachments");
                Directory.CreateDirectory(Program.AttachmentDirectory);

                logIndexingProgress("Probing input file...");
                using (var prober = new FFprobe(Program.InputFile, format: "", argument: "-show_streams -show_format"))
                {
                    string streamInfo = prober.Probe();
                    Program.SubtitleTracks = new Dictionary<int, Tuple<string, SubtitleType>>();
                    Program.AttachmentList = new List<string>();

                    using (var s = new StringReader(streamInfo))
                    {
                        var doc = new XPathDocument(s);
                        var attachindex = 0; // mkvextract separates track and attachment indices

                        foreach (XPathNavigator nav in doc.CreateNavigator().Select("//ffprobe/streams/stream"))
                        {
                            int streamindex;
                            string streamtitle;
                            string file;

                            streamindex = int.Parse(nav.GetAttribute("index", ""));

                            switch (nav.GetAttribute("codec_type", ""))
                            {
                                case "video":
                                    if (streamindex != videotrack) break;

                                    // Check if this is a FRAPS yuvj420p video - if so, we need to do something weird here.
                                    if (nav.GetAttribute("codec_name", "") == "fraps" && nav.GetAttribute("pix_fmt", "") == "yuvj420p")
                                    {
                                        logIndexingProgress("Detected yuvj420p FRAPS video, the Color Level fixing setting has been set for you.");
                                        this.InvokeIfRequired(() =>
                                        {
                                            comboLevels.SelectedIndex = 2; // PC -> TV conversion
                                        });
                                        // If we don't do this, the contrast gets fucked.
                                        // See: https://github.com/nixxquality/WebMConverter/issues/89
                                    }

                                    // Probe for sample aspect ratio
                                    string[] sar = null, dar = null;

                                    sar = nav.GetAttribute("sample_aspect_ratio", "").Split(':');
                                    if ((sar[0] == "1" && sar[1] == "1") || (sar[0] == "0" || sar[1] == "0")) break;

                                    dar = nav.GetAttribute("display_aspect_ratio", "").Split(':');

                                    float SarNum, SarDen, DarNum, DarDen;
                                    SarNum = float.Parse(sar[0]);
                                    SarDen = float.Parse(sar[1]);
                                    DarNum = float.Parse(dar[0]);
                                    DarDen = float.Parse(dar[1]);

                                    SarWidth = int.Parse(nav.GetAttribute("width", ""));
                                    SarHeight = int.Parse(nav.GetAttribute("height", ""));
                                    if (DarNum < DarDen)
                                    {
                                        SarHeight = (int)(SarHeight / (SarNum / SarDen));
                                    }
                                    else
                                    {
                                        SarWidth = (int)(SarWidth * (SarNum / SarDen));
                                    }
                                    SarCompensate = true;
                                    logIndexingProgress("We need to compensate for Sample Aspect Ratio, it seems.");

                                    break;
                                case "subtitle": // Extract the subtitle file
                                    // Get a title
                                    streamtitle = nav.GetAttribute("codec_name", "");
                                    SubtitleType type = SubtitleType.TextSub;

                                    if (streamtitle == "dvdsub") // Hold on a moment, this is a vobsub!
                                    {
                                        type = SubtitleType.VobSub;
                                        // Not supported, see https://github.com/nixxquality/WebMConverter/issues/60
                                        break;
                                    }
                                    
                                    file = Path.Combine(Program.AttachmentDirectory, string.Format("sub{0}.ass", streamindex));
                                    logIndexingProgress(string.Format("Found subtitle track #{0}", streamindex));

                                    if (!File.Exists(file)) // If we didn't extract it already
                                    {
                                        string arg = string.Format("-i \"{0}\" -map 0:{1} \"{2}\" -y", Program.InputFile, streamindex, file);

                                        logIndexingProgress("Extracting...");
                                        using (var ffmpeg = new FFmpeg(arg))
                                        {
                                            ffmpeg.Start();
                                            ffmpeg.WaitForExit();
                                        }
                                    }
                                    else
                                    {
                                        logIndexingProgress("Already extracted! Skipping...");
                                    }

                                    if (!File.Exists(file)) // Holy shit, it still doesn't exist?
                                        break; // Whatever, skip it.

                                    if (!nav.IsEmptyElement) // There might be a tag element
                                    {
                                        nav.MoveTo(nav.SelectSingleNode(".//tag[@key='title']"));
                                        var titleTag = nav.GetAttribute("value", "");

                                        streamtitle = titleTag == "" ? streamtitle : titleTag;
                                    }

                                    // Save it
                                    Program.SubtitleTracks.Add(streamindex, new Tuple<string, SubtitleType>(streamtitle, type));
                                    break;
                                case @"attachment": // Extract the attachment using mkvmerge
                                    nav.MoveTo(nav.SelectSingleNode(".//tag[@key='filename']"));
                                    var filename = nav.GetAttribute("value", "");

                                    nav.MoveToNext();
                                    var mimetype = nav.GetAttribute("value", "");

                                    file = Path.Combine(Program.AttachmentDirectory, filename);
                                    logIndexingProgress(string.Format("Found attachment '{0}'", filename));

                                    attachindex += 1;

                                    if (!mimetype.Contains(@"font"))
                                    {
                                        logIndexingProgress("Not a font! Skipping...");
                                        break;
                                    }

                                    Program.AttachmentList.Add(filename);

                                    if (File.Exists(file)) // Did we extract it already?
                                    {
                                        logIndexingProgress("Already extracted! Skipping...");
                                        break;
                                    }

                                    logIndexingProgress("Extracting...");
                                    using (var mkvextract = new MkvExtract(string.Format(@"attachments ""{0}"" ""{1}:{2}""", Program.InputFile, attachindex, file)))
                                    {
                                        mkvextract.Start();
                                        mkvextract.WaitForExit();
                                    }
                                    break;
                            }
                        }

                        var selectSingleNode = doc.CreateNavigator().SelectSingleNode("//ffprobe/format/tag[@key='title']");
                        if (selectSingleNode != null)
                        {
                            title = selectSingleNode.GetAttribute("value", "");
                            logIndexingProgress("Found title " + title);
                        }
                    }
                }

                Program.VideoSource = index.VideoSource(path, videotrack);
                var frame = Program.VideoSource.GetFrame(0); // We're assuming that the entire video has the same settings here, which should be fine. (These options usually don't vary, I hope.)
                Program.VideoColorRange = frame.ColorRange;
                Program.VideoInterlaced = frame.InterlacedFrame;
                SetSlices(frame.EncodedResolution);
                LoadFonts(msg => logIndexingProgress((string)msg));
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
                boxAudio.Enabled = Program.InputHasAudio = true;
                if (audioDisabled)
                {
                    const string text = "We couldn't find any audio tracks.\nIf you want sound, please use another input file.\nIf you don't want audio in your output webm, there's nothing to worry about.";
                    const string caption = "FYI";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    boxAudio.Enabled = Program.InputHasAudio = false;
                }

                buttonGo.Enabled = true;
                buttonPreview.Enabled = true;
                buttonBrowseIn.Enabled = true;
                textBoxIn.Enabled = true;
                toolStripFilterButtonsEnabled(true);

                if (boxTitle.Text == _autoTitle || boxTitle.Text == "")
                    boxTitle.Text = _autoTitle = title;

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
            progressBarIndexing.Value = 0;
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
                // AviSynth can't LoadPlugin if the path contains non-english characters, so we convert the possibly weird path into 8.3 notation
                StringBuilder shortPluginPath = new StringBuilder(255);
                string pluginPath = Path.Combine(Environment.CurrentDirectory, "Binaries", "Win32");
                NativeMethods.GetShortPathName(@"\\?\" + pluginPath, shortPluginPath, shortPluginPath.Capacity);
                // the \\?\ is added because GetShortPathName will fail if pluginPath is longer than 256 characters otherwise.

                avscript.WriteLine(@"PluginPath = ""{0}\""", shortPluginPath);
                avscript.WriteLine(@"LoadPlugin(PluginPath+""ffms2.dll"")");
                if (Filters.Subtitle != null)
                    avscript.WriteLine(@"LoadPlugin(PluginPath+""VSFilter.dll"")");

                // UTF-8 argument bug workaround
                // see https://github.com/FFMS/ffms2/pull/167
                avscript.WriteLine(@"FFIndex(""{0}"", cachefile=""{1}"", utf8=True)", avsInputFile, _indexFile);
                // this won't actually index file file (we already did), but it will call FFMS_Init with utf8 = true properly, unlike FFVideoSource

                if (Program.InputHasAudio)
                    avscript.WriteLine(@"AudioDub(FFVideoSource(""{0}"",cachefile=""{1}"",track={2}), FFAudioSource(""{0}"",cachefile=""{1}"",track={3}))", avsInputFile, _indexFile, videotrack, audiotrack);
                else
                    avscript.WriteLine(@"FFVideoSource(""{0}"",cachefile=""{1}"",track={2})", avsInputFile, _indexFile, videotrack);

                if (Filters.Deinterlace != null)
                {
                    avscript.WriteLine(@"LoadPlugin(PluginPath+""TDeint.dll"")");
                    avscript.WriteLine(Filters.Deinterlace.ToString());
                }

                if (Filters.Denoise != null)
                {
                    avscript.WriteLine(@"LoadPlugin(PluginPath+""hqdn3d.dll"")");
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
            var ffplay = new FFplay(string.Format("-window_title Preview -loop 0 -f avisynth -v error {1} \"{0}\"", avsFileName, boxAudio.Checked ? "" : "-an"));
            ffplay.Exited += delegate
            {
                string error = null;
                // if (ffplay.ExitCode != 0) // There was an error
                // This is what I would like to do, but ffplay returns 0 even when there's an error in the script.
                error = ffplay.ErrorLog;

                this.InvokeIfRequired(() =>
                {
                    if (error.Length > 0)
                        MessageBox.Show(error, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                arguments = new[] { string.Format(Template, output, options, "", "") };
            else
            {
                arguments = new string[2]; //           vvv is Windows only
                arguments[0] = string.Format(Template, "NUL", options, string.Format(PassArgument, 1, Path.Combine(Path.GetTempPath(), "ffmpeg2pass")));
                arguments[1] = string.Format(Template, output, options, string.Format(PassArgument, 2, Path.Combine(Path.GetTempPath(), "ffmpeg2pass")));

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
                    var limitTo = string.Empty;
                    if (!string.IsNullOrWhiteSpace(boxLimit.Text))
                    {
                        if (!float.TryParse(boxLimit.Text, out limit))
                            throw new ArgumentException("Invalid size limit!");

                        limitTo = string.Format(@" -fs {0}M", limit.ToString(CultureInfo.InvariantCulture)); //Should turn comma into dot
                    }

                    var audiobitrate = -1;
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

                    var videobitrate = 900;
                    if (limitTo != string.Empty)
                    {
                        var duration = GetDuration();

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

                    /*
                     * ffmpeg doesn't let you input the cbr arguments manually (I think) so we do a bit of math here.
                     * ffmpeg lists this in the documentation (http://ffmpeg.org/ffmpeg-all.html#libvpx):
                     *  rc_buf_sz         = (bufsize * 1000 / vb)
                     *  rc_buf_optimal_sz = (bufsize * 1000 / vb * 5 / 6)
                     *  rc_buf_initial_sz = (rc_init_occupancy * 1000 / vb)
                     * libvpx lists this in the documentation (http://www.webmproject.org/docs/encoder-parameters/#vbr-cbr-and-cq-mode):
                     *  --buf-initial-sz=<arg>
                     *  --buf-optimal-sz=<arg>
                     *  --buf-sz=<arg>
                     *  These three parameters set (respectively) the initial assumed buffer level,
                     *   the optimal level and an upper limit that the codec should try not to exceed.
                     *  The numbers given are in 'milliseconds worth of data' so the actual number of bits
                     *   that these number represent depends also on the target bit rate that the user has set.
                     *  Typical recommended values for these three parameters might be 4000, 5000 and 6000 ms, respectively.
                     * a bit of algebra leads us to the following conclusion:
                     *  bufsize           = vb * 6
                     *  rc_init_occupancy = vb * 4
                     * However, because ffmpeg is really weird or something, init_occupancy doesn't seem to work properly
                     *  unless we divide bufsize by 10. Don't ask me why! I don't know!
                     */
                    var bufsize = videobitrate * 6 / 10;
                    var initoccupancy = videobitrate * 4;

                    qualityarguments = string.Format(ConstantVideoArguments, videobitrate, limitTo, bufsize, initoccupancy);
                    if (audiobitrate != -1)
                        qualityarguments += string.Format(ConstantAudioArguments, audiobitrate);

                    break;
                case EncodingMode.Variable:
                    var qmin = Math.Max(0, (int)(numericCrf.Value - numericCrfTolerance.Value));
                    var qmax = Math.Min(63, (int)(numericCrf.Value + numericCrfTolerance.Value));

                    qualityarguments = string.Format(VariableVideoArguments, qmin, numericCrf.Value, qmax);
                    if (boxAudio.Checked &! boxNGOV.Checked) // only for vorbis
                        qualityarguments += string.Format(VariableAudioArguments, numericAudioQuality.Value);

                    break;
            }

            var threads = trackThreads.Value;
            var slices = GetSlices();

            var metadataTitle = "";
            if (!string.IsNullOrWhiteSpace(boxTitle.Text))
                metadataTitle = string.Format(@" -metadata title=""{0}""", boxTitle.Text.Replace("\"", "\\\""));

            var hq = "";
            if (boxHQ.Checked)
                hq = @" -quality best -lag-in-frames 16 -auto-alt-ref 1";

            var vcodec = boxNGOV.Checked ? @"libvpx-vp9" : @"libvpx";
            var acodec = boxNGOV.Checked ? @"libopus" : @"libvorbis";

            string audio;
            if (boxAudio.Checked)
            {
                audio = "";
                acodec = " -ac 2 -c:a " + acodec;
            }
            else
            {
                audio = " -an";
                acodec = "";
            }

            var framerate = "";
            if (!string.IsNullOrWhiteSpace(boxFrameRate.Text))
            {
                framerate = @" -r " + boxFrameRate.Text;
            }

            return string.Format(TemplateArguments, audio, threads, slices, metadataTitle, hq, vcodec, acodec, framerate, qualityarguments);
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
                            return double.Parse(reader.Value, CultureInfo.InvariantCulture);
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
            if (Filters.Dub != null)
                script.AppendLine(Filters.Dub.ToString());

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

        private void LoadFonts(Action<object> action = null)
        {
            Program.AttachmentList.ForEach(filename =>
            {
                if (action != null) action(string.Format("Loading font {0}", filename));

                var ret = NativeMethods.AddFontResourceEx(Path.Combine(Program.AttachmentDirectory, filename), 0, IntPtr.Zero);

                if (ret == 0 && action != null) action("Failed!");
            });
        }

        private void UnloadFonts()
        {
            if (Program.AttachmentList == null)
                return;

            Program.AttachmentList.ForEach(filename =>
            {
                NativeMethods.RemoveFontResourceEx(Path.Combine(Program.AttachmentDirectory, filename), 0, IntPtr.Zero);
            });
            Program.AttachmentList = null;
        }

        #endregion
    }

    public enum EncodingMode
    {
        Constant,
        Variable
    }
}
