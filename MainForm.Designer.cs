namespace WebMConverter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TableLayoutPanel tableMainForm;
            System.Windows.Forms.GroupBox groupMain;
            System.Windows.Forms.TableLayoutPanel tableMain;
            System.Windows.Forms.Label labelInputFile;
            System.Windows.Forms.Label labelOutputFile;
            System.Windows.Forms.Button buttonBrowseOut;
            System.Windows.Forms.TabControl tabControlOptions;
            System.Windows.Forms.TabPage tabProcessing;
            System.Windows.Forms.TableLayoutPanel tableProcessing;
            System.Windows.Forms.ToolStrip toolStripProcessing;
            System.Windows.Forms.Panel panelProcessingInput;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TabPage tabEncoding;
            System.Windows.Forms.TableLayoutPanel tableEncoding;
            System.Windows.Forms.GroupBox groupEncodingGeneral;
            System.Windows.Forms.TableLayoutPanel tableEncodingGeneral;
            System.Windows.Forms.Label labelGeneralTitle;
            System.Windows.Forms.Label labelGeneralTitleHint;
            System.Windows.Forms.Label labelGeneralSizeLimit;
            System.Windows.Forms.Label labelGeneralSizeLimitUnit;
            System.Windows.Forms.Label labelGeneralSizeLimitHint;
            System.Windows.Forms.GroupBox groupEncodingVideo;
            System.Windows.Forms.Label labelVideoBitrate;
            System.Windows.Forms.Label labelVideoBitrateUnit;
            System.Windows.Forms.Label labelVideoBitrateHint;
            System.Windows.Forms.Label labelVideoHQHint;
            System.Windows.Forms.GroupBox groupEncodingAudio;
            System.Windows.Forms.TableLayoutPanel tableEncodingAudio;
            System.Windows.Forms.Label labelAudioHint;
            System.Windows.Forms.Label labelAudioBitrate;
            System.Windows.Forms.Label labelAudioBitrateUnit;
            System.Windows.Forms.Label labelAudioBitrateHint;
            System.Windows.Forms.TabPage tabAdvanced;
            System.Windows.Forms.TableLayoutPanel tableAdvanced;
            System.Windows.Forms.Label labelAdvancedWarning;
            System.Windows.Forms.GroupBox groupAdvancedProcessing;
            System.Windows.Forms.TableLayoutPanel tableAdvancedProcessing;
            System.Windows.Forms.Label labelProcessingLevelsHint;
            System.Windows.Forms.Label labelProcessingDeinterlaceHint;
            System.Windows.Forms.Label labelProcessingDenoiseHint;
            System.Windows.Forms.GroupBox groupAdvancedEncoding;
            System.Windows.Forms.TableLayoutPanel tableAdvancedEncoding;
            System.Windows.Forms.Label labelEncodingNGOVHint;
            System.Windows.Forms.Label labelEncodingThreads;
            System.Windows.Forms.Label labelEncodingThreadsHint;
            System.Windows.Forms.Label labelEncodingSlices;
            System.Windows.Forms.Label labelEncodingSlicesHint;
            System.Windows.Forms.Label labelEncodingCrf;
            System.Windows.Forms.Label labelEncodingCrfHint;
            System.Windows.Forms.Label labelEncodingArguments;
            System.Windows.Forms.Panel panelContainTheProgressBar;
            System.Windows.Forms.StatusStrip statusStrip;
            System.Windows.Forms.Label labelProcessingAviSourceHint;
            this.textBoxIn = new System.Windows.Forms.TextBox();
            this.buttonBrowseIn = new System.Windows.Forms.Button();
            this.textBoxOut = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonTrim = new System.Windows.Forms.ToolStripSplitButton();
            this.buttonMultipleTrim = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCrop = new System.Windows.Forms.ToolStripButton();
            this.buttonResize = new System.Windows.Forms.ToolStripButton();
            this.buttonSubtitle = new System.Windows.Forms.ToolStripButton();
            this.buttonReverse = new System.Windows.Forms.ToolStripButton();
            this.boxAdvancedScripting = new System.Windows.Forms.ToolStripButton();
            this.buttonPreview = new System.Windows.Forms.ToolStripButton();
            this.buttonOverlay = new System.Windows.Forms.ToolStripButton();
            this.buttonCaption = new System.Windows.Forms.ToolStripButton();
            this.listViewProcessingScript = new System.Windows.Forms.ListView();
            this.imageListFilters = new System.Windows.Forms.ImageList(this.components);
            this.textBoxProcessingScript = new System.Windows.Forms.TextBox();
            this.boxTitle = new System.Windows.Forms.TextBox();
            this.boxLimit = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelEncodingVideo = new System.Windows.Forms.TableLayoutPanel();
            this.boxVideoBitrate = new System.Windows.Forms.TextBox();
            this.boxHQ = new System.Windows.Forms.CheckBox();
            this.boxAudio = new System.Windows.Forms.CheckBox();
            this.boxAudioBitrate = new System.Windows.Forms.TextBox();
            this.boxLevels = new System.Windows.Forms.CheckBox();
            this.boxDeinterlace = new System.Windows.Forms.CheckBox();
            this.boxDenoise = new System.Windows.Forms.CheckBox();
            this.boxNGOV = new System.Windows.Forms.CheckBox();
            this.trackThreads = new System.Windows.Forms.TrackBar();
            this.labelThreads = new System.Windows.Forms.Label();
            this.trackSlices = new System.Windows.Forms.TrackBar();
            this.labelSlices = new System.Windows.Forms.Label();
            this.numericCrf = new System.Windows.Forms.NumericUpDown();
            this.boxArguments = new System.Windows.Forms.TextBox();
            this.labelIndexingProgress = new System.Windows.Forms.Label();
            this.progressBarIndexing = new System.Windows.Forms.ProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelHideTheOptions = new System.Windows.Forms.Panel();
            this.boxAviSource = new System.Windows.Forms.CheckBox();
            tableMainForm = new System.Windows.Forms.TableLayoutPanel();
            groupMain = new System.Windows.Forms.GroupBox();
            tableMain = new System.Windows.Forms.TableLayoutPanel();
            labelInputFile = new System.Windows.Forms.Label();
            labelOutputFile = new System.Windows.Forms.Label();
            buttonBrowseOut = new System.Windows.Forms.Button();
            tabControlOptions = new System.Windows.Forms.TabControl();
            tabProcessing = new System.Windows.Forms.TabPage();
            tableProcessing = new System.Windows.Forms.TableLayoutPanel();
            toolStripProcessing = new System.Windows.Forms.ToolStrip();
            panelProcessingInput = new System.Windows.Forms.Panel();
            tabEncoding = new System.Windows.Forms.TabPage();
            tableEncoding = new System.Windows.Forms.TableLayoutPanel();
            groupEncodingGeneral = new System.Windows.Forms.GroupBox();
            tableEncodingGeneral = new System.Windows.Forms.TableLayoutPanel();
            labelGeneralTitle = new System.Windows.Forms.Label();
            labelGeneralTitleHint = new System.Windows.Forms.Label();
            labelGeneralSizeLimit = new System.Windows.Forms.Label();
            labelGeneralSizeLimitUnit = new System.Windows.Forms.Label();
            labelGeneralSizeLimitHint = new System.Windows.Forms.Label();
            groupEncodingVideo = new System.Windows.Forms.GroupBox();
            labelVideoBitrate = new System.Windows.Forms.Label();
            labelVideoBitrateUnit = new System.Windows.Forms.Label();
            labelVideoBitrateHint = new System.Windows.Forms.Label();
            labelVideoHQHint = new System.Windows.Forms.Label();
            groupEncodingAudio = new System.Windows.Forms.GroupBox();
            tableEncodingAudio = new System.Windows.Forms.TableLayoutPanel();
            labelAudioHint = new System.Windows.Forms.Label();
            labelAudioBitrate = new System.Windows.Forms.Label();
            labelAudioBitrateUnit = new System.Windows.Forms.Label();
            labelAudioBitrateHint = new System.Windows.Forms.Label();
            tabAdvanced = new System.Windows.Forms.TabPage();
            tableAdvanced = new System.Windows.Forms.TableLayoutPanel();
            labelAdvancedWarning = new System.Windows.Forms.Label();
            groupAdvancedProcessing = new System.Windows.Forms.GroupBox();
            tableAdvancedProcessing = new System.Windows.Forms.TableLayoutPanel();
            labelProcessingLevelsHint = new System.Windows.Forms.Label();
            labelProcessingDeinterlaceHint = new System.Windows.Forms.Label();
            labelProcessingDenoiseHint = new System.Windows.Forms.Label();
            groupAdvancedEncoding = new System.Windows.Forms.GroupBox();
            tableAdvancedEncoding = new System.Windows.Forms.TableLayoutPanel();
            labelEncodingNGOVHint = new System.Windows.Forms.Label();
            labelEncodingThreads = new System.Windows.Forms.Label();
            labelEncodingThreadsHint = new System.Windows.Forms.Label();
            labelEncodingSlices = new System.Windows.Forms.Label();
            labelEncodingSlicesHint = new System.Windows.Forms.Label();
            labelEncodingCrf = new System.Windows.Forms.Label();
            labelEncodingCrfHint = new System.Windows.Forms.Label();
            labelEncodingArguments = new System.Windows.Forms.Label();
            panelContainTheProgressBar = new System.Windows.Forms.Panel();
            statusStrip = new System.Windows.Forms.StatusStrip();
            labelProcessingAviSourceHint = new System.Windows.Forms.Label();
            tableMainForm.SuspendLayout();
            groupMain.SuspendLayout();
            tableMain.SuspendLayout();
            tabControlOptions.SuspendLayout();
            tabProcessing.SuspendLayout();
            tableProcessing.SuspendLayout();
            toolStripProcessing.SuspendLayout();
            panelProcessingInput.SuspendLayout();
            tabEncoding.SuspendLayout();
            tableEncoding.SuspendLayout();
            groupEncodingGeneral.SuspendLayout();
            tableEncodingGeneral.SuspendLayout();
            groupEncodingVideo.SuspendLayout();
            this.tableLayoutPanelEncodingVideo.SuspendLayout();
            groupEncodingAudio.SuspendLayout();
            tableEncodingAudio.SuspendLayout();
            tabAdvanced.SuspendLayout();
            tableAdvanced.SuspendLayout();
            groupAdvancedProcessing.SuspendLayout();
            tableAdvancedProcessing.SuspendLayout();
            groupAdvancedEncoding.SuspendLayout();
            tableAdvancedEncoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSlices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCrf)).BeginInit();
            panelContainTheProgressBar.SuspendLayout();
            statusStrip.SuspendLayout();
            this.panelHideTheOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMainForm
            // 
            tableMainForm.ColumnCount = 1;
            tableMainForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableMainForm.Controls.Add(groupMain, 0, 0);
            tableMainForm.Controls.Add(tabControlOptions, 0, 1);
            tableMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tableMainForm.Location = new System.Drawing.Point(3, 3);
            tableMainForm.Name = "tableMainForm";
            tableMainForm.RowCount = 3;
            tableMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            tableMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableMainForm.Size = new System.Drawing.Size(1067, 443);
            tableMainForm.TabIndex = 0;
            // 
            // groupMain
            // 
            groupMain.Controls.Add(tableMain);
            groupMain.Dock = System.Windows.Forms.DockStyle.Fill;
            groupMain.Location = new System.Drawing.Point(3, 3);
            groupMain.Name = "groupMain";
            groupMain.Size = new System.Drawing.Size(1061, 78);
            groupMain.TabIndex = 0;
            groupMain.TabStop = false;
            groupMain.Text = "Main";
            // 
            // tableMain
            // 
            tableMain.ColumnCount = 4;
            tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            tableMain.Controls.Add(labelInputFile, 0, 0);
            tableMain.Controls.Add(this.textBoxIn, 1, 0);
            tableMain.Controls.Add(this.buttonBrowseIn, 2, 0);
            tableMain.Controls.Add(labelOutputFile, 0, 1);
            tableMain.Controls.Add(this.textBoxOut, 1, 1);
            tableMain.Controls.Add(buttonBrowseOut, 2, 1);
            tableMain.Controls.Add(this.buttonGo, 3, 0);
            tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tableMain.Location = new System.Drawing.Point(3, 16);
            tableMain.Name = "tableMain";
            tableMain.RowCount = 2;
            tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableMain.Size = new System.Drawing.Size(1055, 59);
            tableMain.TabIndex = 0;
            // 
            // labelInputFile
            // 
            labelInputFile.AutoSize = true;
            labelInputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            labelInputFile.Location = new System.Drawing.Point(3, 0);
            labelInputFile.Name = "labelInputFile";
            labelInputFile.Size = new System.Drawing.Size(63, 29);
            labelInputFile.TabIndex = 0;
            labelInputFile.Text = "Input file:";
            labelInputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxIn
            // 
            this.textBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIn.Location = new System.Drawing.Point(72, 4);
            this.textBoxIn.Name = "textBoxIn";
            this.textBoxIn.Size = new System.Drawing.Size(835, 20);
            this.textBoxIn.TabIndex = 1;
            // 
            // buttonBrowseIn
            // 
            this.buttonBrowseIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBrowseIn.Location = new System.Drawing.Point(913, 3);
            this.buttonBrowseIn.Name = "buttonBrowseIn";
            this.buttonBrowseIn.Size = new System.Drawing.Size(62, 23);
            this.buttonBrowseIn.TabIndex = 2;
            this.buttonBrowseIn.Text = "Browse";
            this.buttonBrowseIn.UseVisualStyleBackColor = true;
            this.buttonBrowseIn.Click += new System.EventHandler(this.buttonBrowseIn_Click);
            // 
            // labelOutputFile
            // 
            labelOutputFile.AutoSize = true;
            labelOutputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            labelOutputFile.Location = new System.Drawing.Point(3, 29);
            labelOutputFile.Name = "labelOutputFile";
            labelOutputFile.Size = new System.Drawing.Size(63, 30);
            labelOutputFile.TabIndex = 0;
            labelOutputFile.Text = "Output file:";
            labelOutputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxOut
            // 
            this.textBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOut.Location = new System.Drawing.Point(72, 34);
            this.textBoxOut.Name = "textBoxOut";
            this.textBoxOut.Size = new System.Drawing.Size(835, 20);
            this.textBoxOut.TabIndex = 3;
            // 
            // buttonBrowseOut
            // 
            buttonBrowseOut.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonBrowseOut.Location = new System.Drawing.Point(913, 32);
            buttonBrowseOut.Name = "buttonBrowseOut";
            buttonBrowseOut.Size = new System.Drawing.Size(62, 24);
            buttonBrowseOut.TabIndex = 4;
            buttonBrowseOut.Text = "Browse";
            buttonBrowseOut.UseVisualStyleBackColor = true;
            buttonBrowseOut.Click += new System.EventHandler(this.buttonBrowseOut_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGo.Enabled = false;
            this.buttonGo.Location = new System.Drawing.Point(981, 3);
            this.buttonGo.Name = "buttonGo";
            tableMain.SetRowSpan(this.buttonGo, 2);
            this.buttonGo.Size = new System.Drawing.Size(71, 53);
            this.buttonGo.TabIndex = 5;
            this.buttonGo.Text = "Convert";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // tabControlOptions
            // 
            tabControlOptions.Controls.Add(tabProcessing);
            tabControlOptions.Controls.Add(tabEncoding);
            tabControlOptions.Controls.Add(tabAdvanced);
            tabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControlOptions.Location = new System.Drawing.Point(3, 87);
            tabControlOptions.Name = "tabControlOptions";
            tabControlOptions.SelectedIndex = 0;
            tabControlOptions.Size = new System.Drawing.Size(1061, 333);
            tabControlOptions.TabIndex = 1;
            // 
            // tabProcessing
            // 
            tabProcessing.Controls.Add(tableProcessing);
            tabProcessing.Location = new System.Drawing.Point(4, 22);
            tabProcessing.Name = "tabProcessing";
            tabProcessing.Padding = new System.Windows.Forms.Padding(3);
            tabProcessing.Size = new System.Drawing.Size(1053, 307);
            tabProcessing.TabIndex = 3;
            tabProcessing.Text = "Processing";
            // 
            // tableProcessing
            // 
            tableProcessing.ColumnCount = 1;
            tableProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableProcessing.Controls.Add(toolStripProcessing, 0, 0);
            tableProcessing.Controls.Add(panelProcessingInput, 0, 1);
            tableProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            tableProcessing.Location = new System.Drawing.Point(3, 3);
            tableProcessing.Name = "tableProcessing";
            tableProcessing.RowCount = 2;
            tableProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableProcessing.Size = new System.Drawing.Size(1047, 301);
            tableProcessing.TabIndex = 0;
            // 
            // toolStripProcessing
            // 
            toolStripProcessing.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStripProcessing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonTrim,
            this.buttonCrop,
            this.buttonResize,
            this.buttonSubtitle,
            this.buttonReverse,
            this.boxAdvancedScripting,
            this.buttonPreview,
            this.buttonOverlay,
            this.buttonCaption});
            toolStripProcessing.Location = new System.Drawing.Point(0, 0);
            toolStripProcessing.Name = "toolStripProcessing";
            toolStripProcessing.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolStripProcessing.ShowItemToolTips = false;
            toolStripProcessing.Size = new System.Drawing.Size(1047, 25);
            toolStripProcessing.TabIndex = 0;
            toolStripProcessing.TabStop = true;
            // 
            // buttonTrim
            // 
            this.buttonTrim.AccessibleDescription = "Select a clip from your video.";
            this.buttonTrim.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonMultipleTrim});
            this.buttonTrim.Enabled = false;
            this.buttonTrim.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.buttonTrim.Name = "buttonTrim";
            this.buttonTrim.Size = new System.Drawing.Size(48, 22);
            this.buttonTrim.Text = "Trim";
            this.buttonTrim.ButtonClick += new System.EventHandler(this.buttonTrim_Click);
            this.buttonTrim.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonTrim.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonMultipleTrim
            // 
            this.buttonMultipleTrim.AccessibleDescription = "Select many clips from your video, and sort them on a timeline.";
            this.buttonMultipleTrim.Name = "buttonMultipleTrim";
            this.buttonMultipleTrim.Size = new System.Drawing.Size(146, 22);
            this.buttonMultipleTrim.Text = "Multiple Trim";
            this.buttonMultipleTrim.Click += new System.EventHandler(this.buttonMultipleTrim_Click);
            this.buttonMultipleTrim.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonMultipleTrim.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonCrop
            // 
            this.buttonCrop.AccessibleDescription = "Crop your video into a smaller frame.";
            this.buttonCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCrop.Enabled = false;
            this.buttonCrop.Name = "buttonCrop";
            this.buttonCrop.Size = new System.Drawing.Size(37, 22);
            this.buttonCrop.Text = "Crop";
            this.buttonCrop.Click += new System.EventHandler(this.buttonCrop_Click);
            this.buttonCrop.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonCrop.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonResize
            // 
            this.buttonResize.AccessibleDescription = "Scale your video.";
            this.buttonResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonResize.Enabled = false;
            this.buttonResize.Name = "buttonResize";
            this.buttonResize.Size = new System.Drawing.Size(43, 22);
            this.buttonResize.Text = "Resize";
            this.buttonResize.Click += new System.EventHandler(this.buttonResize_Click);
            this.buttonResize.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonResize.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonSubtitle
            // 
            this.buttonSubtitle.AccessibleDescription = "Burn subtitles into the video.";
            this.buttonSubtitle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonSubtitle.Enabled = false;
            this.buttonSubtitle.Name = "buttonSubtitle";
            this.buttonSubtitle.Size = new System.Drawing.Size(56, 22);
            this.buttonSubtitle.Text = "Subtitles";
            this.buttonSubtitle.Click += new System.EventHandler(this.buttonSubtitle_Click);
            this.buttonSubtitle.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonSubtitle.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonReverse
            // 
            this.buttonReverse.AccessibleDescription = "Everything is backwards!";
            this.buttonReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonReverse.Enabled = false;
            this.buttonReverse.Name = "buttonReverse";
            this.buttonReverse.Size = new System.Drawing.Size(51, 22);
            this.buttonReverse.Text = "Reverse";
            this.buttonReverse.Click += new System.EventHandler(this.buttonReverse_Click);
            this.buttonReverse.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonReverse.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // boxAdvancedScripting
            // 
            this.boxAdvancedScripting.AccessibleDescription = "Are you a bad enough dude? Take care, there is no way back. You will have to star" +
    "t over if you fuck up.";
            this.boxAdvancedScripting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.boxAdvancedScripting.Name = "boxAdvancedScripting";
            this.boxAdvancedScripting.Size = new System.Drawing.Size(64, 22);
            this.boxAdvancedScripting.Text = "Advanced";
            this.boxAdvancedScripting.Click += new System.EventHandler(this.boxAdvancedScripting_Click);
            this.boxAdvancedScripting.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.boxAdvancedScripting.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonPreview
            // 
            this.buttonPreview.AccessibleDescription = "Open a preview window that will loop your processing settings. Note that this doe" +
    "sn\'t reflect output encoding quality.";
            this.buttonPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPreview.Enabled = false;
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(84, 22);
            this.buttonPreview.Text = "Preview filters";
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            this.buttonPreview.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonPreview.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonOverlay
            // 
            this.buttonOverlay.AccessibleDescription = "Overlay a picture on top of your video.";
            this.buttonOverlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonOverlay.Enabled = false;
            this.buttonOverlay.Name = "buttonOverlay";
            this.buttonOverlay.Size = new System.Drawing.Size(51, 22);
            this.buttonOverlay.Text = "Overlay";
            this.buttonOverlay.Click += new System.EventHandler(this.buttonOverlay_Click);
            this.buttonOverlay.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonOverlay.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonCaption
            // 
            this.buttonCaption.AccessibleDescription = "Add some funny text to your video.";
            this.buttonCaption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCaption.Enabled = false;
            this.buttonCaption.Name = "buttonCaption";
            this.buttonCaption.Size = new System.Drawing.Size(53, 22);
            this.buttonCaption.Text = "Caption";
            this.buttonCaption.Click += new System.EventHandler(this.buttonCaption_Click);
            this.buttonCaption.MouseEnter += new System.EventHandler(this.ToolStripItemTooltip);
            this.buttonCaption.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // panelProcessingInput
            // 
            panelProcessingInput.Controls.Add(this.listViewProcessingScript);
            panelProcessingInput.Controls.Add(this.textBoxProcessingScript);
            panelProcessingInput.Dock = System.Windows.Forms.DockStyle.Fill;
            panelProcessingInput.Location = new System.Drawing.Point(0, 25);
            panelProcessingInput.Margin = new System.Windows.Forms.Padding(0);
            panelProcessingInput.Name = "panelProcessingInput";
            panelProcessingInput.Size = new System.Drawing.Size(1047, 276);
            panelProcessingInput.TabIndex = 1;
            // 
            // listViewProcessingScript
            // 
            this.listViewProcessingScript.AccessibleDescription = "Double click a filter to edit it. Select a filter and press Delete to remove it.";
            this.listViewProcessingScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcessingScript.LargeImageList = this.imageListFilters;
            this.listViewProcessingScript.Location = new System.Drawing.Point(0, 0);
            this.listViewProcessingScript.Margin = new System.Windows.Forms.Padding(0);
            this.listViewProcessingScript.Name = "listViewProcessingScript";
            this.listViewProcessingScript.Size = new System.Drawing.Size(1047, 276);
            this.listViewProcessingScript.TabIndex = 3;
            this.listViewProcessingScript.UseCompatibleStateImageBehavior = false;
            this.listViewProcessingScript.ItemActivate += new System.EventHandler(this.listViewProcessingScript_ItemActivate);
            this.listViewProcessingScript.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewProcessingScript_KeyUp);
            this.listViewProcessingScript.MouseEnter += new System.EventHandler(this.ControlTooltip);
            this.listViewProcessingScript.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // imageListFilters
            // 
            this.imageListFilters.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFilters.ImageStream")));
            this.imageListFilters.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListFilters.Images.SetKeyName(0, "Trim");
            this.imageListFilters.Images.SetKeyName(1, "Crop");
            this.imageListFilters.Images.SetKeyName(2, "Subtitles");
            this.imageListFilters.Images.SetKeyName(3, "Reverse");
            this.imageListFilters.Images.SetKeyName(4, "Resize");
            this.imageListFilters.Images.SetKeyName(5, "Overlay");
            this.imageListFilters.Images.SetKeyName(6, "Caption");
            // 
            // textBoxProcessingScript
            // 
            this.textBoxProcessingScript.AcceptsReturn = true;
            this.textBoxProcessingScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProcessingScript.Location = new System.Drawing.Point(0, 0);
            this.textBoxProcessingScript.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxProcessingScript.Multiline = true;
            this.textBoxProcessingScript.Name = "textBoxProcessingScript";
            this.textBoxProcessingScript.Size = new System.Drawing.Size(1047, 276);
            this.textBoxProcessingScript.TabIndex = 2;
            this.textBoxProcessingScript.Text = "# This is an AviSynth script. You may write advanced commands below, or just pres" +
    "s the buttons above for smooth sailing.";
            this.textBoxProcessingScript.Visible = false;
            this.textBoxProcessingScript.Leave += new System.EventHandler(this.textBoxProcessingScript_Leave);
            // 
            // tabEncoding
            // 
            tabEncoding.BackColor = System.Drawing.SystemColors.Control;
            tabEncoding.Controls.Add(tableEncoding);
            tabEncoding.Location = new System.Drawing.Point(4, 22);
            tabEncoding.Name = "tabEncoding";
            tabEncoding.Padding = new System.Windows.Forms.Padding(3);
            tabEncoding.Size = new System.Drawing.Size(1053, 307);
            tabEncoding.TabIndex = 0;
            tabEncoding.Text = "Encoding";
            // 
            // tableEncoding
            // 
            tableEncoding.ColumnCount = 1;
            tableEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableEncoding.Controls.Add(groupEncodingGeneral, 0, 0);
            tableEncoding.Controls.Add(groupEncodingVideo, 0, 1);
            tableEncoding.Controls.Add(groupEncodingAudio, 0, 2);
            tableEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            tableEncoding.Location = new System.Drawing.Point(3, 3);
            tableEncoding.Name = "tableEncoding";
            tableEncoding.RowCount = 4;
            tableEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableEncoding.Size = new System.Drawing.Size(1047, 301);
            tableEncoding.TabIndex = 0;
            // 
            // groupEncodingGeneral
            // 
            groupEncodingGeneral.Controls.Add(tableEncodingGeneral);
            groupEncodingGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            groupEncodingGeneral.Location = new System.Drawing.Point(3, 3);
            groupEncodingGeneral.Name = "groupEncodingGeneral";
            groupEncodingGeneral.Size = new System.Drawing.Size(1041, 73);
            groupEncodingGeneral.TabIndex = 1;
            groupEncodingGeneral.TabStop = false;
            groupEncodingGeneral.Text = "General";
            // 
            // tableEncodingGeneral
            // 
            tableEncodingGeneral.ColumnCount = 5;
            tableEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            tableEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            tableEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableEncodingGeneral.Controls.Add(labelGeneralTitle, 0, 0);
            tableEncodingGeneral.Controls.Add(this.boxTitle, 1, 0);
            tableEncodingGeneral.Controls.Add(labelGeneralTitleHint, 4, 0);
            tableEncodingGeneral.Controls.Add(labelGeneralSizeLimit, 0, 1);
            tableEncodingGeneral.Controls.Add(this.boxLimit, 1, 1);
            tableEncodingGeneral.Controls.Add(labelGeneralSizeLimitUnit, 2, 1);
            tableEncodingGeneral.Controls.Add(labelGeneralSizeLimitHint, 3, 1);
            tableEncodingGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            tableEncodingGeneral.Location = new System.Drawing.Point(3, 16);
            tableEncodingGeneral.Name = "tableEncodingGeneral";
            tableEncodingGeneral.RowCount = 2;
            tableEncodingGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableEncodingGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableEncodingGeneral.Size = new System.Drawing.Size(1035, 54);
            tableEncodingGeneral.TabIndex = 0;
            // 
            // labelGeneralTitle
            // 
            labelGeneralTitle.AutoSize = true;
            labelGeneralTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelGeneralTitle.Location = new System.Drawing.Point(3, 0);
            labelGeneralTitle.Name = "labelGeneralTitle";
            labelGeneralTitle.Size = new System.Drawing.Size(73, 28);
            labelGeneralTitle.TabIndex = 0;
            labelGeneralTitle.Text = "Title:";
            labelGeneralTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // boxTitle
            // 
            this.boxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            tableEncodingGeneral.SetColumnSpan(this.boxTitle, 3);
            this.boxTitle.Location = new System.Drawing.Point(82, 4);
            this.boxTitle.Name = "boxTitle";
            this.boxTitle.Size = new System.Drawing.Size(390, 20);
            this.boxTitle.TabIndex = 1;
            this.boxTitle.TextChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // labelGeneralTitleHint
            // 
            labelGeneralTitleHint.AutoSize = true;
            labelGeneralTitleHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelGeneralTitleHint.Location = new System.Drawing.Point(478, 0);
            labelGeneralTitleHint.Name = "labelGeneralTitleHint";
            labelGeneralTitleHint.Size = new System.Drawing.Size(554, 28);
            labelGeneralTitleHint.TabIndex = 0;
            labelGeneralTitleHint.Text = "Adds a string of text to the metadata of the video, which can be used to indicate" +
    " the source of a video, for example. Leave blank for no title.";
            labelGeneralTitleHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGeneralSizeLimit
            // 
            labelGeneralSizeLimit.AutoSize = true;
            labelGeneralSizeLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            labelGeneralSizeLimit.Location = new System.Drawing.Point(3, 28);
            labelGeneralSizeLimit.Name = "labelGeneralSizeLimit";
            labelGeneralSizeLimit.Size = new System.Drawing.Size(73, 28);
            labelGeneralSizeLimit.TabIndex = 0;
            labelGeneralSizeLimit.Text = "Size limit:";
            labelGeneralSizeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // boxLimit
            // 
            this.boxLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxLimit.Location = new System.Drawing.Point(82, 32);
            this.boxLimit.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.boxLimit.Name = "boxLimit";
            this.boxLimit.Size = new System.Drawing.Size(115, 20);
            this.boxLimit.TabIndex = 2;
            this.boxLimit.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelGeneralSizeLimitUnit
            // 
            labelGeneralSizeLimitUnit.AutoSize = true;
            labelGeneralSizeLimitUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            labelGeneralSizeLimitUnit.Location = new System.Drawing.Point(197, 28);
            labelGeneralSizeLimitUnit.Margin = new System.Windows.Forms.Padding(0);
            labelGeneralSizeLimitUnit.Name = "labelGeneralSizeLimitUnit";
            labelGeneralSizeLimitUnit.Size = new System.Drawing.Size(30, 28);
            labelGeneralSizeLimitUnit.TabIndex = 0;
            labelGeneralSizeLimitUnit.Text = "MB";
            labelGeneralSizeLimitUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGeneralSizeLimitHint
            // 
            labelGeneralSizeLimitHint.AutoSize = true;
            tableEncodingGeneral.SetColumnSpan(labelGeneralSizeLimitHint, 2);
            labelGeneralSizeLimitHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelGeneralSizeLimitHint.Location = new System.Drawing.Point(230, 28);
            labelGeneralSizeLimitHint.Name = "labelGeneralSizeLimitHint";
            labelGeneralSizeLimitHint.Size = new System.Drawing.Size(802, 28);
            labelGeneralSizeLimitHint.TabIndex = 0;
            labelGeneralSizeLimitHint.Text = "Will adjust the quality to attempt to stay below this limit, and cut off the end " +
    "of a video if needed. Leave blank for no limit. The limit on 4chan is 3 MB.";
            labelGeneralSizeLimitHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupEncodingVideo
            // 
            groupEncodingVideo.Controls.Add(this.tableLayoutPanelEncodingVideo);
            groupEncodingVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            groupEncodingVideo.Location = new System.Drawing.Point(3, 82);
            groupEncodingVideo.Name = "groupEncodingVideo";
            groupEncodingVideo.Size = new System.Drawing.Size(1041, 73);
            groupEncodingVideo.TabIndex = 2;
            groupEncodingVideo.TabStop = false;
            groupEncodingVideo.Text = "Video";
            // 
            // tableLayoutPanelEncodingVideo
            // 
            this.tableLayoutPanelEncodingVideo.ColumnCount = 4;
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncodingVideo.Controls.Add(labelVideoBitrate, 0, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.boxVideoBitrate, 1, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(labelVideoBitrateUnit, 2, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(labelVideoBitrateHint, 4, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.boxHQ, 0, 1);
            this.tableLayoutPanelEncodingVideo.Controls.Add(labelVideoHQHint, 3, 1);
            this.tableLayoutPanelEncodingVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncodingVideo.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelEncodingVideo.Name = "tableLayoutPanelEncodingVideo";
            this.tableLayoutPanelEncodingVideo.RowCount = 2;
            this.tableLayoutPanelEncodingVideo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingVideo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingVideo.Size = new System.Drawing.Size(1035, 54);
            this.tableLayoutPanelEncodingVideo.TabIndex = 0;
            // 
            // labelVideoBitrate
            // 
            labelVideoBitrate.AutoSize = true;
            labelVideoBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVideoBitrate.Location = new System.Drawing.Point(3, 0);
            labelVideoBitrate.Name = "labelVideoBitrate";
            labelVideoBitrate.Size = new System.Drawing.Size(73, 28);
            labelVideoBitrate.TabIndex = 0;
            labelVideoBitrate.Text = "Bitrate:";
            labelVideoBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // boxVideoBitrate
            // 
            this.boxVideoBitrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxVideoBitrate.Location = new System.Drawing.Point(82, 4);
            this.boxVideoBitrate.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.boxVideoBitrate.Name = "boxVideoBitrate";
            this.boxVideoBitrate.Size = new System.Drawing.Size(115, 20);
            this.boxVideoBitrate.TabIndex = 1;
            this.boxVideoBitrate.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxVideoBitrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelVideoBitrateUnit
            // 
            labelVideoBitrateUnit.AutoSize = true;
            labelVideoBitrateUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVideoBitrateUnit.Location = new System.Drawing.Point(197, 0);
            labelVideoBitrateUnit.Margin = new System.Windows.Forms.Padding(0);
            labelVideoBitrateUnit.Name = "labelVideoBitrateUnit";
            labelVideoBitrateUnit.Size = new System.Drawing.Size(30, 28);
            labelVideoBitrateUnit.TabIndex = 0;
            labelVideoBitrateUnit.Text = "Kb/s";
            labelVideoBitrateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVideoBitrateHint
            // 
            labelVideoBitrateHint.AutoSize = true;
            labelVideoBitrateHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVideoBitrateHint.Location = new System.Drawing.Point(230, 0);
            labelVideoBitrateHint.Name = "labelVideoBitrateHint";
            labelVideoBitrateHint.Size = new System.Drawing.Size(802, 28);
            labelVideoBitrateHint.TabIndex = 0;
            labelVideoBitrateHint.Text = "Determines the quality of the video. Keep blank to let the program pick one based" +
    " on size limit and duration.";
            labelVideoBitrateHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxHQ
            // 
            this.boxHQ.AutoSize = true;
            this.boxHQ.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanelEncodingVideo.SetColumnSpan(this.boxHQ, 3);
            this.boxHQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxHQ.Location = new System.Drawing.Point(6, 31);
            this.boxHQ.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.boxHQ.Name = "boxHQ";
            this.boxHQ.Size = new System.Drawing.Size(215, 22);
            this.boxHQ.TabIndex = 2;
            this.boxHQ.Text = "Enable high quality mode:";
            this.boxHQ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxHQ.UseVisualStyleBackColor = true;
            this.boxHQ.CheckedChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // labelVideoHQHint
            // 
            labelVideoHQHint.AutoSize = true;
            labelVideoHQHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVideoHQHint.Location = new System.Drawing.Point(230, 28);
            labelVideoHQHint.Name = "labelVideoHQHint";
            labelVideoHQHint.Size = new System.Drawing.Size(802, 28);
            labelVideoHQHint.TabIndex = 0;
            labelVideoHQHint.Text = "Enables two-pass encoding and adds some extra encoding arguments, increasing outp" +
    "ut quality, but increases the time it takes to encode your file.";
            labelVideoHQHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupEncodingAudio
            // 
            groupEncodingAudio.Controls.Add(tableEncodingAudio);
            groupEncodingAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            groupEncodingAudio.Location = new System.Drawing.Point(3, 161);
            groupEncodingAudio.Name = "groupEncodingAudio";
            groupEncodingAudio.Size = new System.Drawing.Size(1041, 73);
            groupEncodingAudio.TabIndex = 3;
            groupEncodingAudio.TabStop = false;
            groupEncodingAudio.Text = "Audio";
            // 
            // tableEncodingAudio
            // 
            tableEncodingAudio.ColumnCount = 4;
            tableEncodingAudio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableEncodingAudio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            tableEncodingAudio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableEncodingAudio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableEncodingAudio.Controls.Add(this.boxAudio, 0, 0);
            tableEncodingAudio.Controls.Add(labelAudioHint, 3, 0);
            tableEncodingAudio.Controls.Add(labelAudioBitrate, 0, 1);
            tableEncodingAudio.Controls.Add(this.boxAudioBitrate, 1, 1);
            tableEncodingAudio.Controls.Add(labelAudioBitrateUnit, 2, 1);
            tableEncodingAudio.Controls.Add(labelAudioBitrateHint, 3, 1);
            tableEncodingAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            tableEncodingAudio.Location = new System.Drawing.Point(3, 16);
            tableEncodingAudio.Margin = new System.Windows.Forms.Padding(0);
            tableEncodingAudio.Name = "tableEncodingAudio";
            tableEncodingAudio.RowCount = 2;
            tableEncodingAudio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableEncodingAudio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableEncodingAudio.Size = new System.Drawing.Size(1035, 54);
            tableEncodingAudio.TabIndex = 0;
            // 
            // boxAudio
            // 
            this.boxAudio.AutoSize = true;
            this.boxAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            tableEncodingAudio.SetColumnSpan(this.boxAudio, 3);
            this.boxAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxAudio.Location = new System.Drawing.Point(6, 3);
            this.boxAudio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.boxAudio.Name = "boxAudio";
            this.boxAudio.Size = new System.Drawing.Size(215, 21);
            this.boxAudio.TabIndex = 1;
            this.boxAudio.Text = "Enable audio:";
            this.boxAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxAudio.UseVisualStyleBackColor = true;
            this.boxAudio.CheckedChanged += new System.EventHandler(this.boxAudio_CheckedChanged);
            // 
            // labelAudioHint
            // 
            labelAudioHint.AutoSize = true;
            labelAudioHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAudioHint.Location = new System.Drawing.Point(230, 0);
            labelAudioHint.Name = "labelAudioHint";
            labelAudioHint.Size = new System.Drawing.Size(802, 27);
            labelAudioHint.TabIndex = 0;
            labelAudioHint.Text = "Keep this disabled until Moot allows audio.";
            labelAudioHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAudioBitrate
            // 
            labelAudioBitrate.AutoSize = true;
            labelAudioBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAudioBitrate.Location = new System.Drawing.Point(3, 27);
            labelAudioBitrate.Name = "labelAudioBitrate";
            labelAudioBitrate.Size = new System.Drawing.Size(73, 27);
            labelAudioBitrate.TabIndex = 0;
            labelAudioBitrate.Text = "Bitrate:";
            labelAudioBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // boxAudioBitrate
            // 
            this.boxAudioBitrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxAudioBitrate.Enabled = false;
            this.boxAudioBitrate.Location = new System.Drawing.Point(82, 30);
            this.boxAudioBitrate.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.boxAudioBitrate.Name = "boxAudioBitrate";
            this.boxAudioBitrate.Size = new System.Drawing.Size(115, 20);
            this.boxAudioBitrate.TabIndex = 2;
            this.boxAudioBitrate.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxAudioBitrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelAudioBitrateUnit
            // 
            labelAudioBitrateUnit.AutoSize = true;
            labelAudioBitrateUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAudioBitrateUnit.Location = new System.Drawing.Point(197, 27);
            labelAudioBitrateUnit.Margin = new System.Windows.Forms.Padding(0);
            labelAudioBitrateUnit.Name = "labelAudioBitrateUnit";
            labelAudioBitrateUnit.Size = new System.Drawing.Size(30, 27);
            labelAudioBitrateUnit.TabIndex = 0;
            labelAudioBitrateUnit.Text = "Kb/s";
            labelAudioBitrateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAudioBitrateHint
            // 
            labelAudioBitrateHint.AutoSize = true;
            labelAudioBitrateHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAudioBitrateHint.Location = new System.Drawing.Point(230, 27);
            labelAudioBitrateHint.Name = "labelAudioBitrateHint";
            labelAudioBitrateHint.Size = new System.Drawing.Size(802, 27);
            labelAudioBitrateHint.TabIndex = 0;
            labelAudioBitrateHint.Text = "Determines the quality of the audio.";
            labelAudioBitrateHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabAdvanced
            // 
            tabAdvanced.AutoScroll = true;
            tabAdvanced.BackColor = System.Drawing.SystemColors.Control;
            tabAdvanced.Controls.Add(tableAdvanced);
            tabAdvanced.Location = new System.Drawing.Point(4, 22);
            tabAdvanced.Name = "tabAdvanced";
            tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
            tabAdvanced.Size = new System.Drawing.Size(1053, 307);
            tabAdvanced.TabIndex = 4;
            tabAdvanced.Text = "Advanced";
            // 
            // tableAdvanced
            // 
            tableAdvanced.ColumnCount = 1;
            tableAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableAdvanced.Controls.Add(labelAdvancedWarning, 0, 0);
            tableAdvanced.Controls.Add(groupAdvancedProcessing, 0, 1);
            tableAdvanced.Controls.Add(groupAdvancedEncoding, 0, 2);
            tableAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            tableAdvanced.Location = new System.Drawing.Point(3, 3);
            tableAdvanced.Name = "tableAdvanced";
            tableAdvanced.RowCount = 3;
            tableAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            tableAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            tableAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableAdvanced.Size = new System.Drawing.Size(1047, 301);
            tableAdvanced.TabIndex = 1;
            // 
            // labelAdvancedWarning
            // 
            labelAdvancedWarning.AutoSize = true;
            tableAdvanced.SetColumnSpan(labelAdvancedWarning, 3);
            labelAdvancedWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAdvancedWarning.Location = new System.Drawing.Point(3, 3);
            labelAdvancedWarning.Margin = new System.Windows.Forms.Padding(3);
            labelAdvancedWarning.Name = "labelAdvancedWarning";
            labelAdvancedWarning.Size = new System.Drawing.Size(1041, 22);
            labelAdvancedWarning.TabIndex = 0;
            labelAdvancedWarning.Text = "Do not modify these settings unless you are 100% sure you know what you\'re doing." +
    "";
            labelAdvancedWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupAdvancedProcessing
            // 
            groupAdvancedProcessing.Controls.Add(tableAdvancedProcessing);
            groupAdvancedProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            groupAdvancedProcessing.Location = new System.Drawing.Point(3, 31);
            groupAdvancedProcessing.Name = "groupAdvancedProcessing";
            groupAdvancedProcessing.Size = new System.Drawing.Size(1041, 104);
            groupAdvancedProcessing.TabIndex = 1;
            groupAdvancedProcessing.TabStop = false;
            groupAdvancedProcessing.Text = "Processing";
            // 
            // tableAdvancedProcessing
            // 
            tableAdvancedProcessing.ColumnCount = 4;
            tableAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            tableAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            tableAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableAdvancedProcessing.Controls.Add(labelProcessingAviSourceHint, 3, 0);
            tableAdvancedProcessing.Controls.Add(this.boxAviSource, 2, 0);
            tableAdvancedProcessing.Controls.Add(this.boxLevels, 0, 0);
            tableAdvancedProcessing.Controls.Add(labelProcessingLevelsHint, 1, 0);
            tableAdvancedProcessing.Controls.Add(this.boxDeinterlace, 0, 1);
            tableAdvancedProcessing.Controls.Add(labelProcessingDeinterlaceHint, 1, 1);
            tableAdvancedProcessing.Controls.Add(this.boxDenoise, 0, 2);
            tableAdvancedProcessing.Controls.Add(labelProcessingDenoiseHint, 1, 2);
            tableAdvancedProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            tableAdvancedProcessing.Location = new System.Drawing.Point(3, 16);
            tableAdvancedProcessing.Name = "tableAdvancedProcessing";
            tableAdvancedProcessing.RowCount = 3;
            tableAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedProcessing.Size = new System.Drawing.Size(1035, 85);
            tableAdvancedProcessing.TabIndex = 1;
            // 
            // boxLevels
            // 
            this.boxLevels.AutoSize = true;
            this.boxLevels.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxLevels.Location = new System.Drawing.Point(3, 3);
            this.boxLevels.Name = "boxLevels";
            this.boxLevels.Size = new System.Drawing.Size(142, 22);
            this.boxLevels.TabIndex = 1;
            this.boxLevels.Text = "Expand color ranges:";
            this.boxLevels.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxLevels.UseVisualStyleBackColor = true;
            this.boxLevels.CheckedChanged += new System.EventHandler(this.boxLevels_CheckedChanged);
            // 
            // labelProcessingLevelsHint
            // 
            labelProcessingLevelsHint.AutoSize = true;
            labelProcessingLevelsHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProcessingLevelsHint.Location = new System.Drawing.Point(151, 0);
            labelProcessingLevelsHint.Name = "labelProcessingLevelsHint";
            labelProcessingLevelsHint.Size = new System.Drawing.Size(363, 28);
            labelProcessingLevelsHint.TabIndex = 0;
            labelProcessingLevelsHint.Text = "If your output colors look a bit washed out, it might be because of a poorly enco" +
    "ded input file. This option expands the color levels to the full range.";
            labelProcessingLevelsHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxDeinterlace
            // 
            this.boxDeinterlace.AutoSize = true;
            this.boxDeinterlace.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDeinterlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxDeinterlace.Location = new System.Drawing.Point(3, 31);
            this.boxDeinterlace.Name = "boxDeinterlace";
            this.boxDeinterlace.Size = new System.Drawing.Size(142, 22);
            this.boxDeinterlace.TabIndex = 2;
            this.boxDeinterlace.Text = "Deinterlace:";
            this.boxDeinterlace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDeinterlace.UseVisualStyleBackColor = true;
            this.boxDeinterlace.CheckedChanged += new System.EventHandler(this.boxDeinterlace_CheckedChanged);
            // 
            // labelProcessingDeinterlaceHint
            // 
            labelProcessingDeinterlaceHint.AutoSize = true;
            labelProcessingDeinterlaceHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProcessingDeinterlaceHint.Location = new System.Drawing.Point(151, 28);
            labelProcessingDeinterlaceHint.Name = "labelProcessingDeinterlaceHint";
            labelProcessingDeinterlaceHint.Size = new System.Drawing.Size(363, 28);
            labelProcessingDeinterlaceHint.TabIndex = 0;
            labelProcessingDeinterlaceHint.Text = "Attempt to deinterlace an interlaced input video.";
            labelProcessingDeinterlaceHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxDenoise
            // 
            this.boxDenoise.AutoSize = true;
            this.boxDenoise.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDenoise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxDenoise.Location = new System.Drawing.Point(3, 59);
            this.boxDenoise.Name = "boxDenoise";
            this.boxDenoise.Size = new System.Drawing.Size(142, 23);
            this.boxDenoise.TabIndex = 3;
            this.boxDenoise.Text = "Denoise:";
            this.boxDenoise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDenoise.UseVisualStyleBackColor = true;
            this.boxDenoise.CheckedChanged += new System.EventHandler(this.boxDenoise_CheckedChanged);
            // 
            // labelProcessingDenoiseHint
            // 
            labelProcessingDenoiseHint.AutoSize = true;
            labelProcessingDenoiseHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProcessingDenoiseHint.Location = new System.Drawing.Point(151, 56);
            labelProcessingDenoiseHint.Name = "labelProcessingDenoiseHint";
            labelProcessingDenoiseHint.Size = new System.Drawing.Size(363, 29);
            labelProcessingDenoiseHint.TabIndex = 0;
            labelProcessingDenoiseHint.Text = "Denoise the video, resulting in less detailed video but more bang for your buck w" +
    "hen it comes to bitrate.";
            labelProcessingDenoiseHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupAdvancedEncoding
            // 
            groupAdvancedEncoding.AutoSize = true;
            groupAdvancedEncoding.Controls.Add(tableAdvancedEncoding);
            groupAdvancedEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            groupAdvancedEncoding.Location = new System.Drawing.Point(3, 141);
            groupAdvancedEncoding.Name = "groupAdvancedEncoding";
            groupAdvancedEncoding.Size = new System.Drawing.Size(1041, 157);
            groupAdvancedEncoding.TabIndex = 2;
            groupAdvancedEncoding.TabStop = false;
            groupAdvancedEncoding.Text = "Encoding";
            // 
            // tableAdvancedEncoding
            // 
            tableAdvancedEncoding.ColumnCount = 4;
            tableAdvancedEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            tableAdvancedEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            tableAdvancedEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            tableAdvancedEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableAdvancedEncoding.Controls.Add(labelEncodingNGOVHint, 3, 3);
            tableAdvancedEncoding.Controls.Add(this.boxNGOV, 0, 3);
            tableAdvancedEncoding.Controls.Add(labelEncodingThreads, 0, 0);
            tableAdvancedEncoding.Controls.Add(this.trackThreads, 1, 0);
            tableAdvancedEncoding.Controls.Add(this.labelThreads, 2, 0);
            tableAdvancedEncoding.Controls.Add(labelEncodingThreadsHint, 3, 0);
            tableAdvancedEncoding.Controls.Add(labelEncodingSlices, 0, 1);
            tableAdvancedEncoding.Controls.Add(this.trackSlices, 1, 1);
            tableAdvancedEncoding.Controls.Add(this.labelSlices, 2, 1);
            tableAdvancedEncoding.Controls.Add(labelEncodingSlicesHint, 3, 1);
            tableAdvancedEncoding.Controls.Add(labelEncodingCrf, 0, 2);
            tableAdvancedEncoding.Controls.Add(this.numericCrf, 1, 2);
            tableAdvancedEncoding.Controls.Add(labelEncodingCrfHint, 3, 2);
            tableAdvancedEncoding.Controls.Add(labelEncodingArguments, 0, 4);
            tableAdvancedEncoding.Controls.Add(this.boxArguments, 1, 4);
            tableAdvancedEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            tableAdvancedEncoding.Location = new System.Drawing.Point(3, 16);
            tableAdvancedEncoding.Name = "tableAdvancedEncoding";
            tableAdvancedEncoding.RowCount = 5;
            tableAdvancedEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableAdvancedEncoding.Size = new System.Drawing.Size(1035, 138);
            tableAdvancedEncoding.TabIndex = 0;
            // 
            // labelEncodingNGOVHint
            // 
            labelEncodingNGOVHint.AutoSize = true;
            labelEncodingNGOVHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingNGOVHint.Location = new System.Drawing.Point(230, 84);
            labelEncodingNGOVHint.Name = "labelEncodingNGOVHint";
            labelEncodingNGOVHint.Size = new System.Drawing.Size(802, 28);
            labelEncodingNGOVHint.TabIndex = 0;
            labelEncodingNGOVHint.Text = "Use the next-gen VP9/Opus encoders instead of the standard VP8/Vorbis. Will resul" +
    "t in extremely long encoding times and less compatibility.\r\nKeep this disabled u" +
    "ntil Moot allows VP9 WebMs.";
            labelEncodingNGOVHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxNGOV
            // 
            this.boxNGOV.AutoSize = true;
            this.boxNGOV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            tableAdvancedEncoding.SetColumnSpan(this.boxNGOV, 3);
            this.boxNGOV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxNGOV.Location = new System.Drawing.Point(3, 87);
            this.boxNGOV.Name = "boxNGOV";
            this.boxNGOV.Size = new System.Drawing.Size(221, 22);
            this.boxNGOV.TabIndex = 4;
            this.boxNGOV.Text = "VP9/Opus:";
            this.boxNGOV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxNGOV.UseVisualStyleBackColor = true;
            this.boxNGOV.CheckedChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // labelEncodingThreads
            // 
            labelEncodingThreads.AutoSize = true;
            labelEncodingThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingThreads.Location = new System.Drawing.Point(3, 0);
            labelEncodingThreads.Name = "labelEncodingThreads";
            labelEncodingThreads.Size = new System.Drawing.Size(73, 28);
            labelEncodingThreads.TabIndex = 0;
            labelEncodingThreads.Text = "Threads:";
            labelEncodingThreads.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackThreads
            // 
            this.trackThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackThreads.Location = new System.Drawing.Point(79, 0);
            this.trackThreads.Margin = new System.Windows.Forms.Padding(0);
            this.trackThreads.Maximum = 16;
            this.trackThreads.Minimum = 1;
            this.trackThreads.Name = "trackThreads";
            this.trackThreads.Size = new System.Drawing.Size(118, 28);
            this.trackThreads.TabIndex = 1;
            this.trackThreads.Value = 1;
            this.trackThreads.ValueChanged += new System.EventHandler(this.trackThreads_ValueChanged);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelThreads.Location = new System.Drawing.Point(197, 0);
            this.labelThreads.Margin = new System.Windows.Forms.Padding(0);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(30, 28);
            this.labelThreads.TabIndex = 0;
            this.labelThreads.Text = "1";
            this.labelThreads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingThreadsHint
            // 
            labelEncodingThreadsHint.AutoSize = true;
            labelEncodingThreadsHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingThreadsHint.Location = new System.Drawing.Point(230, 0);
            labelEncodingThreadsHint.Name = "labelEncodingThreadsHint";
            labelEncodingThreadsHint.Size = new System.Drawing.Size(802, 28);
            labelEncodingThreadsHint.TabIndex = 0;
            labelEncodingThreadsHint.Text = "Determines amount of threads ffmpeg uses. Try setting this to 1 if ffmpeg.exe cra" +
    "shes as soon as you click Convert.";
            labelEncodingThreadsHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingSlices
            // 
            labelEncodingSlices.AutoSize = true;
            labelEncodingSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingSlices.Location = new System.Drawing.Point(3, 28);
            labelEncodingSlices.Name = "labelEncodingSlices";
            labelEncodingSlices.Size = new System.Drawing.Size(73, 28);
            labelEncodingSlices.TabIndex = 0;
            labelEncodingSlices.Text = "Slices:";
            labelEncodingSlices.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trackSlices
            // 
            this.trackSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackSlices.Location = new System.Drawing.Point(79, 28);
            this.trackSlices.Margin = new System.Windows.Forms.Padding(0);
            this.trackSlices.Maximum = 4;
            this.trackSlices.Minimum = 1;
            this.trackSlices.Name = "trackSlices";
            this.trackSlices.Size = new System.Drawing.Size(118, 28);
            this.trackSlices.TabIndex = 2;
            this.trackSlices.Value = 1;
            this.trackSlices.ValueChanged += new System.EventHandler(this.trackSlices_ValueChanged);
            // 
            // labelSlices
            // 
            this.labelSlices.AutoSize = true;
            this.labelSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSlices.Location = new System.Drawing.Point(197, 28);
            this.labelSlices.Margin = new System.Windows.Forms.Padding(0);
            this.labelSlices.Name = "labelSlices";
            this.labelSlices.Size = new System.Drawing.Size(30, 28);
            this.labelSlices.TabIndex = 0;
            this.labelSlices.Text = "1";
            this.labelSlices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingSlicesHint
            // 
            labelEncodingSlicesHint.AutoSize = true;
            labelEncodingSlicesHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingSlicesHint.Location = new System.Drawing.Point(230, 28);
            labelEncodingSlicesHint.Name = "labelEncodingSlicesHint";
            labelEncodingSlicesHint.Size = new System.Drawing.Size(802, 28);
            labelEncodingSlicesHint.TabIndex = 0;
            labelEncodingSlicesHint.Text = "Split frames into slices before encoding them. Results in a higher quality per fr" +
    "ame. 4 slices is standard for 720p resolutions.";
            labelEncodingSlicesHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingCrf
            // 
            labelEncodingCrf.AutoSize = true;
            labelEncodingCrf.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingCrf.Location = new System.Drawing.Point(3, 56);
            labelEncodingCrf.Name = "labelEncodingCrf";
            labelEncodingCrf.Size = new System.Drawing.Size(73, 28);
            labelEncodingCrf.TabIndex = 0;
            labelEncodingCrf.Text = "CRF:";
            labelEncodingCrf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericCrf
            // 
            tableAdvancedEncoding.SetColumnSpan(this.numericCrf, 2);
            this.numericCrf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericCrf.Location = new System.Drawing.Point(82, 59);
            this.numericCrf.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.numericCrf.Name = "numericCrf";
            this.numericCrf.Size = new System.Drawing.Size(142, 20);
            this.numericCrf.TabIndex = 3;
            this.numericCrf.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericCrf.ValueChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // labelEncodingCrfHint
            // 
            labelEncodingCrfHint.AutoSize = true;
            labelEncodingCrfHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingCrfHint.Location = new System.Drawing.Point(230, 56);
            labelEncodingCrfHint.Name = "labelEncodingCrfHint";
            labelEncodingCrfHint.Size = new System.Drawing.Size(802, 28);
            labelEncodingCrfHint.TabIndex = 0;
            labelEncodingCrfHint.Text = "Constant rate factor for the video; basically, the overall quality. A lower value" +
    " means higher quality and filesize.";
            labelEncodingCrfHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingArguments
            // 
            labelEncodingArguments.AutoSize = true;
            labelEncodingArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            labelEncodingArguments.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            labelEncodingArguments.Location = new System.Drawing.Point(3, 112);
            labelEncodingArguments.Name = "labelEncodingArguments";
            labelEncodingArguments.Size = new System.Drawing.Size(73, 28);
            labelEncodingArguments.TabIndex = 0;
            labelEncodingArguments.Text = "Arguments:";
            labelEncodingArguments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // boxArguments
            // 
            this.boxArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            tableAdvancedEncoding.SetColumnSpan(this.boxArguments, 3);
            this.boxArguments.Location = new System.Drawing.Point(82, 116);
            this.boxArguments.Name = "boxArguments";
            this.boxArguments.Size = new System.Drawing.Size(950, 20);
            this.boxArguments.TabIndex = 5;
            // 
            // panelContainTheProgressBar
            // 
            panelContainTheProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            panelContainTheProgressBar.AutoSize = true;
            panelContainTheProgressBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panelContainTheProgressBar.BackColor = System.Drawing.SystemColors.Control;
            panelContainTheProgressBar.Controls.Add(this.labelIndexingProgress);
            panelContainTheProgressBar.Controls.Add(this.progressBarIndexing);
            panelContainTheProgressBar.Location = new System.Drawing.Point(299, 148);
            panelContainTheProgressBar.Name = "panelContainTheProgressBar";
            panelContainTheProgressBar.Size = new System.Drawing.Size(469, 61);
            panelContainTheProgressBar.TabIndex = 0;
            // 
            // labelIndexingProgress
            // 
            this.labelIndexingProgress.BackColor = System.Drawing.Color.Transparent;
            this.labelIndexingProgress.Location = new System.Drawing.Point(4, 5);
            this.labelIndexingProgress.Name = "labelIndexingProgress";
            this.labelIndexingProgress.Size = new System.Drawing.Size(460, 23);
            this.labelIndexingProgress.TabIndex = 1;
            this.labelIndexingProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarIndexing
            // 
            this.progressBarIndexing.Location = new System.Drawing.Point(6, 32);
            this.progressBarIndexing.Margin = new System.Windows.Forms.Padding(6);
            this.progressBarIndexing.Name = "progressBarIndexing";
            this.progressBarIndexing.Size = new System.Drawing.Size(457, 23);
            this.progressBarIndexing.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarIndexing.TabIndex = 0;
            this.progressBarIndexing.Value = 30;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            statusStrip.Location = new System.Drawing.Point(3, 424);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1067, 22);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 6;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(1052, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelHideTheOptions
            // 
            this.panelHideTheOptions.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelHideTheOptions.Controls.Add(panelContainTheProgressBar);
            this.panelHideTheOptions.Location = new System.Drawing.Point(3, 88);
            this.panelHideTheOptions.Name = "panelHideTheOptions";
            this.panelHideTheOptions.Size = new System.Drawing.Size(1067, 356);
            this.panelHideTheOptions.TabIndex = 3;
            // 
            // boxAviSource
            // 
            this.boxAviSource.AutoSize = true;
            this.boxAviSource.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxAviSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxAviSource.Location = new System.Drawing.Point(520, 3);
            this.boxAviSource.Name = "boxAviSource";
            this.boxAviSource.Size = new System.Drawing.Size(142, 22);
            this.boxAviSource.TabIndex = 4;
            this.boxAviSource.Text = "Use AVISource:";
            this.boxAviSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxAviSource.UseVisualStyleBackColor = true;
            // 
            // labelProcessingAviSourceHint
            // 
            labelProcessingAviSourceHint.AutoSize = true;
            labelProcessingAviSourceHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProcessingAviSourceHint.Location = new System.Drawing.Point(668, 0);
            labelProcessingAviSourceHint.Name = "labelProcessingAviSourceHint";
            labelProcessingAviSourceHint.Size = new System.Drawing.Size(364, 28);
            labelProcessingAviSourceHint.TabIndex = 5;
            labelProcessingAviSourceHint.Text = "In some rare cases, FFVideoSource will fail where AVISource succeeds. Use this in" +
    " that case.";
            labelProcessingAviSourceHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonGo;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 446);
            this.Controls.Add(statusStrip);
            this.Controls.Add(tableMainForm);
            this.Controls.Add(this.panelHideTheOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(975, 270);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Text = "WebM for Retards";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            tableMainForm.ResumeLayout(false);
            groupMain.ResumeLayout(false);
            tableMain.ResumeLayout(false);
            tableMain.PerformLayout();
            tabControlOptions.ResumeLayout(false);
            tabProcessing.ResumeLayout(false);
            tableProcessing.ResumeLayout(false);
            tableProcessing.PerformLayout();
            toolStripProcessing.ResumeLayout(false);
            toolStripProcessing.PerformLayout();
            panelProcessingInput.ResumeLayout(false);
            panelProcessingInput.PerformLayout();
            tabEncoding.ResumeLayout(false);
            tableEncoding.ResumeLayout(false);
            groupEncodingGeneral.ResumeLayout(false);
            tableEncodingGeneral.ResumeLayout(false);
            tableEncodingGeneral.PerformLayout();
            groupEncodingVideo.ResumeLayout(false);
            this.tableLayoutPanelEncodingVideo.ResumeLayout(false);
            this.tableLayoutPanelEncodingVideo.PerformLayout();
            groupEncodingAudio.ResumeLayout(false);
            tableEncodingAudio.ResumeLayout(false);
            tableEncodingAudio.PerformLayout();
            tabAdvanced.ResumeLayout(false);
            tableAdvanced.ResumeLayout(false);
            tableAdvanced.PerformLayout();
            groupAdvancedProcessing.ResumeLayout(false);
            tableAdvancedProcessing.ResumeLayout(false);
            tableAdvancedProcessing.PerformLayout();
            groupAdvancedEncoding.ResumeLayout(false);
            tableAdvancedEncoding.ResumeLayout(false);
            tableAdvancedEncoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSlices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCrf)).EndInit();
            panelContainTheProgressBar.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            this.panelHideTheOptions.ResumeLayout(false);
            this.panelHideTheOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxIn;
        public System.Windows.Forms.TextBox textBoxOut;
        private System.Windows.Forms.ToolStripButton buttonCrop;
        private System.Windows.Forms.ToolStripButton buttonResize;
        private System.Windows.Forms.ToolStripButton buttonReverse;
        private System.Windows.Forms.ToolStripButton boxAdvancedScripting;
        private System.Windows.Forms.ToolStripButton buttonSubtitle;
        private System.Windows.Forms.TextBox textBoxProcessingScript;
        private System.Windows.Forms.TextBox boxTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingVideo;
        private System.Windows.Forms.CheckBox boxHQ;
        private System.Windows.Forms.Panel panelHideTheOptions;
        private System.Windows.Forms.ProgressBar progressBarIndexing;
        private System.Windows.Forms.Label labelIndexingProgress;
        private System.Windows.Forms.TextBox boxArguments;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.CheckBox boxDeinterlace;
        private System.Windows.Forms.CheckBox boxLevels;
        private System.Windows.Forms.ToolStripSplitButton buttonTrim;
        private System.Windows.Forms.ToolStripMenuItem buttonMultipleTrim;
        private System.Windows.Forms.ToolStripButton buttonCaption;
        private System.Windows.Forms.ToolStripButton buttonOverlay;
        private System.Windows.Forms.CheckBox boxAudio;
        private System.Windows.Forms.CheckBox boxDenoise;
        private System.Windows.Forms.NumericUpDown numericCrf;
        private System.Windows.Forms.Button buttonBrowseIn;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.ToolStripButton buttonPreview;
        private System.Windows.Forms.ImageList imageListFilters;
        private System.Windows.Forms.ListView listViewProcessingScript;
        private System.Windows.Forms.TextBox boxLimit;
        private System.Windows.Forms.TextBox boxVideoBitrate;
        private System.Windows.Forms.TextBox boxAudioBitrate;
        private System.Windows.Forms.TrackBar trackSlices;
        private System.Windows.Forms.Label labelSlices;
        private System.Windows.Forms.TrackBar trackThreads;
        private System.Windows.Forms.Label labelThreads;
        private System.Windows.Forms.CheckBox boxNGOV;
        private System.Windows.Forms.CheckBox boxAviSource;
    }
}

