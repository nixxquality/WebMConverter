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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanelMainForm = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelGroupMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelInputFile = new System.Windows.Forms.Label();
            this.textBoxIn = new System.Windows.Forms.TextBox();
            this.buttonBrowseIn = new System.Windows.Forms.Button();
            this.labelOutputFile = new System.Windows.Forms.Label();
            this.textBoxOut = new System.Windows.Forms.TextBox();
            this.buttonBrowseOut = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPageProcessing = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelProcessing = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripProcessingScript = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTrim = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripButtonMultipleTrim = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonCrop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSubtitle = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReverse = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAdvancedScripting = new System.Windows.Forms.ToolStripButton();
            this.buttonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOverlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCaption = new System.Windows.Forms.ToolStripButton();
            this.panelProcessingScriptInput = new System.Windows.Forms.Panel();
            this.listViewProcessingScript = new System.Windows.Forms.ListView();
            this.textBoxProcessingScript = new System.Windows.Forms.TextBox();
            this.tabPageEncoding = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelEncoding = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEncodingGeneral = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelEncodingGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.labelGeneralSizeLimitHint = new System.Windows.Forms.Label();
            this.tableLayoutPanelGeneralSizeLimit = new System.Windows.Forms.TableLayoutPanel();
            this.boxLimit = new System.Windows.Forms.TextBox();
            this.labelGeneralSizeLimitUnit = new System.Windows.Forms.Label();
            this.labelGeneralSizeLimit = new System.Windows.Forms.Label();
            this.labelGeneralTitle = new System.Windows.Forms.Label();
            this.labelMetadataTitleHint = new System.Windows.Forms.Label();
            this.boxMetadataTitle = new System.Windows.Forms.TextBox();
            this.groupBoxEncodingVideo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelEncodingVideo = new System.Windows.Forms.TableLayoutPanel();
            this.labelVideoBitrate = new System.Windows.Forms.Label();
            this.tableLayoutPanelVideoBitrate = new System.Windows.Forms.TableLayoutPanel();
            this.boxVideoBitrate = new System.Windows.Forms.TextBox();
            this.labelVideoBitrateUnit = new System.Windows.Forms.Label();
            this.labelVideoBitrateHint = new System.Windows.Forms.Label();
            this.boxHQ = new System.Windows.Forms.CheckBox();
            this.labelVideoHQHint = new System.Windows.Forms.Label();
            this.groupBoxEncodingAudio = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelAudioBitrate = new System.Windows.Forms.TableLayoutPanel();
            this.boxAudioBitrate = new System.Windows.Forms.TextBox();
            this.labelAudioBitrateUnit = new System.Windows.Forms.Label();
            this.labelGeneralAudioHint = new System.Windows.Forms.Label();
            this.boxAudio = new System.Windows.Forms.CheckBox();
            this.labelAudioBitrate = new System.Windows.Forms.Label();
            this.labelAudioBitrateHint = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxAdvancedEncoding = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelEncodingAdvanced = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelEncodingSlices = new System.Windows.Forms.TableLayoutPanel();
            this.trackSlices = new System.Windows.Forms.TrackBar();
            this.labelSlices = new System.Windows.Forms.Label();
            this.labelEncodingThreads = new System.Windows.Forms.Label();
            this.tableLayoutPanelEncodingThreads = new System.Windows.Forms.TableLayoutPanel();
            this.trackThreads = new System.Windows.Forms.TrackBar();
            this.labelThreads = new System.Windows.Forms.Label();
            this.labelEncodingThreadsHint = new System.Windows.Forms.Label();
            this.labelEncodingArguments = new System.Windows.Forms.Label();
            this.textBoxArguments = new System.Windows.Forms.TextBox();
            this.labelEncodingSlices = new System.Windows.Forms.Label();
            this.labelEncodingSlicesHint = new System.Windows.Forms.Label();
            this.labelAdvancedWarning = new System.Windows.Forms.Label();
            this.groupBoxAdvancedProcessing = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelAdvancedProcessing = new System.Windows.Forms.TableLayoutPanel();
            this.labelProcessingDenoiseHint = new System.Windows.Forms.Label();
            this.boxDenoise = new System.Windows.Forms.CheckBox();
            this.labelProcessingLevelsHint = new System.Windows.Forms.Label();
            this.labelProcessingDeinterlaceHint = new System.Windows.Forms.Label();
            this.boxDeinterlace = new System.Windows.Forms.CheckBox();
            this.boxLevels = new System.Windows.Forms.CheckBox();
            this.panelHideTheOptions = new System.Windows.Forms.Panel();
            this.panelContainTheProgressBar = new System.Windows.Forms.Panel();
            this.labelIndexingProgress = new System.Windows.Forms.Label();
            this.progressBarIndexing = new System.Windows.Forms.ProgressBar();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelVideoCrf = new System.Windows.Forms.Label();
            this.numericCrf = new System.Windows.Forms.NumericUpDown();
            this.labelVideoCrfHint = new System.Windows.Forms.Label();
            this.tableLayoutPanelMainForm.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.tableLayoutPanelGroupMain.SuspendLayout();
            this.tabControlOptions.SuspendLayout();
            this.tabPageProcessing.SuspendLayout();
            this.tableLayoutPanelProcessing.SuspendLayout();
            this.toolStripProcessingScript.SuspendLayout();
            this.panelProcessingScriptInput.SuspendLayout();
            this.tabPageEncoding.SuspendLayout();
            this.tableLayoutPanelEncoding.SuspendLayout();
            this.groupBoxEncodingGeneral.SuspendLayout();
            this.tableLayoutPanelEncodingGeneral.SuspendLayout();
            this.tableLayoutPanelGeneralSizeLimit.SuspendLayout();
            this.groupBoxEncodingVideo.SuspendLayout();
            this.tableLayoutPanelEncodingVideo.SuspendLayout();
            this.tableLayoutPanelVideoBitrate.SuspendLayout();
            this.groupBoxEncodingAudio.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelAudioBitrate.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxAdvancedEncoding.SuspendLayout();
            this.tableLayoutPanelEncodingAdvanced.SuspendLayout();
            this.tableLayoutPanelEncodingSlices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackSlices)).BeginInit();
            this.tableLayoutPanelEncodingThreads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).BeginInit();
            this.groupBoxAdvancedProcessing.SuspendLayout();
            this.tableLayoutPanelAdvancedProcessing.SuspendLayout();
            this.panelHideTheOptions.SuspendLayout();
            this.panelContainTheProgressBar.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCrf)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMainForm
            // 
            this.tableLayoutPanelMainForm.ColumnCount = 1;
            this.tableLayoutPanelMainForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMainForm.Controls.Add(this.groupBoxMain, 0, 0);
            this.tableLayoutPanelMainForm.Controls.Add(this.tabControlOptions, 0, 1);
            this.tableLayoutPanelMainForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMainForm.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMainForm.Name = "tableLayoutPanelMainForm";
            this.tableLayoutPanelMainForm.RowCount = 3;
            this.tableLayoutPanelMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanelMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMainForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMainForm.Size = new System.Drawing.Size(1067, 443);
            this.tableLayoutPanelMainForm.TabIndex = 0;
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.tableLayoutPanelGroupMain);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(1061, 78);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "Main";
            // 
            // tableLayoutPanelGroupMain
            // 
            this.tableLayoutPanelGroupMain.ColumnCount = 4;
            this.tableLayoutPanelGroupMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanelGroupMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGroupMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanelGroupMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanelGroupMain.Controls.Add(this.labelInputFile, 0, 0);
            this.tableLayoutPanelGroupMain.Controls.Add(this.textBoxIn, 1, 0);
            this.tableLayoutPanelGroupMain.Controls.Add(this.buttonBrowseIn, 2, 0);
            this.tableLayoutPanelGroupMain.Controls.Add(this.labelOutputFile, 0, 1);
            this.tableLayoutPanelGroupMain.Controls.Add(this.textBoxOut, 1, 1);
            this.tableLayoutPanelGroupMain.Controls.Add(this.buttonBrowseOut, 2, 1);
            this.tableLayoutPanelGroupMain.Controls.Add(this.buttonGo, 3, 0);
            this.tableLayoutPanelGroupMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGroupMain.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelGroupMain.Name = "tableLayoutPanelGroupMain";
            this.tableLayoutPanelGroupMain.RowCount = 2;
            this.tableLayoutPanelGroupMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGroupMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGroupMain.Size = new System.Drawing.Size(1055, 59);
            this.tableLayoutPanelGroupMain.TabIndex = 0;
            // 
            // labelInputFile
            // 
            this.labelInputFile.AutoSize = true;
            this.labelInputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInputFile.Location = new System.Drawing.Point(3, 0);
            this.labelInputFile.Name = "labelInputFile";
            this.labelInputFile.Size = new System.Drawing.Size(63, 29);
            this.labelInputFile.TabIndex = 0;
            this.labelInputFile.Text = "Input file:";
            this.labelInputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxIn
            // 
            this.textBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIn.Location = new System.Drawing.Point(72, 4);
            this.textBoxIn.Name = "textBoxIn";
            this.textBoxIn.Size = new System.Drawing.Size(835, 20);
            this.textBoxIn.TabIndex = 0;
            // 
            // buttonBrowseIn
            // 
            this.buttonBrowseIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBrowseIn.Location = new System.Drawing.Point(913, 3);
            this.buttonBrowseIn.Name = "buttonBrowseIn";
            this.buttonBrowseIn.Size = new System.Drawing.Size(62, 23);
            this.buttonBrowseIn.TabIndex = 1;
            this.buttonBrowseIn.Text = "Browse";
            this.buttonBrowseIn.UseVisualStyleBackColor = true;
            this.buttonBrowseIn.Click += new System.EventHandler(this.buttonBrowseIn_Click);
            // 
            // labelOutputFile
            // 
            this.labelOutputFile.AutoSize = true;
            this.labelOutputFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOutputFile.Location = new System.Drawing.Point(3, 29);
            this.labelOutputFile.Name = "labelOutputFile";
            this.labelOutputFile.Size = new System.Drawing.Size(63, 30);
            this.labelOutputFile.TabIndex = 3;
            this.labelOutputFile.Text = "Output file:";
            this.labelOutputFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxOut
            // 
            this.textBoxOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOut.Location = new System.Drawing.Point(72, 34);
            this.textBoxOut.Name = "textBoxOut";
            this.textBoxOut.Size = new System.Drawing.Size(835, 20);
            this.textBoxOut.TabIndex = 2;
            // 
            // buttonBrowseOut
            // 
            this.buttonBrowseOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBrowseOut.Location = new System.Drawing.Point(913, 32);
            this.buttonBrowseOut.Name = "buttonBrowseOut";
            this.buttonBrowseOut.Size = new System.Drawing.Size(62, 24);
            this.buttonBrowseOut.TabIndex = 3;
            this.buttonBrowseOut.Text = "Browse";
            this.buttonBrowseOut.UseVisualStyleBackColor = true;
            this.buttonBrowseOut.Click += new System.EventHandler(this.buttonBrowseOut_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGo.Enabled = false;
            this.buttonGo.Location = new System.Drawing.Point(981, 3);
            this.buttonGo.Name = "buttonGo";
            this.tableLayoutPanelGroupMain.SetRowSpan(this.buttonGo, 2);
            this.buttonGo.Size = new System.Drawing.Size(71, 53);
            this.buttonGo.TabIndex = 4;
            this.buttonGo.Text = "Convert";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabPageProcessing);
            this.tabControlOptions.Controls.Add(this.tabPageEncoding);
            this.tabControlOptions.Controls.Add(this.tabPageAdvanced);
            this.tabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOptions.Location = new System.Drawing.Point(3, 87);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(1061, 333);
            this.tabControlOptions.TabIndex = 1;
            // 
            // tabPageProcessing
            // 
            this.tabPageProcessing.Controls.Add(this.tableLayoutPanelProcessing);
            this.tabPageProcessing.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcessing.Name = "tabPageProcessing";
            this.tabPageProcessing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProcessing.Size = new System.Drawing.Size(1053, 307);
            this.tabPageProcessing.TabIndex = 3;
            this.tabPageProcessing.Text = "Processing";
            // 
            // tableLayoutPanelProcessing
            // 
            this.tableLayoutPanelProcessing.ColumnCount = 1;
            this.tableLayoutPanelProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelProcessing.Controls.Add(this.toolStripProcessingScript, 0, 0);
            this.tableLayoutPanelProcessing.Controls.Add(this.panelProcessingScriptInput, 0, 1);
            this.tableLayoutPanelProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelProcessing.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelProcessing.Name = "tableLayoutPanelProcessing";
            this.tableLayoutPanelProcessing.RowCount = 2;
            this.tableLayoutPanelProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelProcessing.Size = new System.Drawing.Size(1047, 301);
            this.tableLayoutPanelProcessing.TabIndex = 0;
            // 
            // toolStripProcessingScript
            // 
            this.toolStripProcessingScript.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripProcessingScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTrim,
            this.toolStripButtonCrop,
            this.toolStripButtonResize,
            this.toolStripButtonSubtitle,
            this.toolStripButtonReverse,
            this.toolStripButtonAdvancedScripting,
            this.buttonPreview,
            this.toolStripButtonOverlay,
            this.toolStripButtonCaption});
            this.toolStripProcessingScript.Location = new System.Drawing.Point(0, 0);
            this.toolStripProcessingScript.Name = "toolStripProcessingScript";
            this.toolStripProcessingScript.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripProcessingScript.ShowItemToolTips = false;
            this.toolStripProcessingScript.Size = new System.Drawing.Size(1047, 25);
            this.toolStripProcessingScript.TabIndex = 0;
            this.toolStripProcessingScript.TabStop = true;
            // 
            // toolStripButtonTrim
            // 
            this.toolStripButtonTrim.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMultipleTrim});
            this.toolStripButtonTrim.Enabled = false;
            this.toolStripButtonTrim.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            this.toolStripButtonTrim.Name = "toolStripButtonTrim";
            this.toolStripButtonTrim.Size = new System.Drawing.Size(48, 22);
            this.toolStripButtonTrim.Text = "Trim";
            this.toolStripButtonTrim.ButtonClick += new System.EventHandler(this.toolStripButtonTrim_Click);
            this.toolStripButtonTrim.MouseEnter += new System.EventHandler(this.toolStripButtonTrim_MouseEnter);
            this.toolStripButtonTrim.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonMultipleTrim
            // 
            this.toolStripButtonMultipleTrim.Name = "toolStripButtonMultipleTrim";
            this.toolStripButtonMultipleTrim.Size = new System.Drawing.Size(146, 22);
            this.toolStripButtonMultipleTrim.Text = "Multiple Trim";
            this.toolStripButtonMultipleTrim.Click += new System.EventHandler(this.toolStripButtonMultipleTrim_Click);
            this.toolStripButtonMultipleTrim.MouseEnter += new System.EventHandler(this.toolStripButtonMultipleTrim_MouseEnter);
            this.toolStripButtonMultipleTrim.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonCrop
            // 
            this.toolStripButtonCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCrop.Enabled = false;
            this.toolStripButtonCrop.Name = "toolStripButtonCrop";
            this.toolStripButtonCrop.Size = new System.Drawing.Size(37, 22);
            this.toolStripButtonCrop.Text = "Crop";
            this.toolStripButtonCrop.Click += new System.EventHandler(this.toolStripButtonCrop_Click);
            this.toolStripButtonCrop.MouseEnter += new System.EventHandler(this.toolStripButtonCrop_MouseEnter);
            this.toolStripButtonCrop.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonResize
            // 
            this.toolStripButtonResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonResize.Enabled = false;
            this.toolStripButtonResize.Name = "toolStripButtonResize";
            this.toolStripButtonResize.Size = new System.Drawing.Size(43, 22);
            this.toolStripButtonResize.Text = "Resize";
            this.toolStripButtonResize.Click += new System.EventHandler(this.toolStripButtonResize_Click);
            this.toolStripButtonResize.MouseEnter += new System.EventHandler(this.toolStripButtonResize_MouseEnter);
            this.toolStripButtonResize.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonSubtitle
            // 
            this.toolStripButtonSubtitle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSubtitle.Enabled = false;
            this.toolStripButtonSubtitle.Name = "toolStripButtonSubtitle";
            this.toolStripButtonSubtitle.Size = new System.Drawing.Size(56, 22);
            this.toolStripButtonSubtitle.Text = "Subtitles";
            this.toolStripButtonSubtitle.Click += new System.EventHandler(this.toolStripButtonSubtitle_Click);
            this.toolStripButtonSubtitle.MouseEnter += new System.EventHandler(this.toolStripButtonSubtitle_MouseEnter);
            this.toolStripButtonSubtitle.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonReverse
            // 
            this.toolStripButtonReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonReverse.Enabled = false;
            this.toolStripButtonReverse.Name = "toolStripButtonReverse";
            this.toolStripButtonReverse.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonReverse.Text = "Reverse";
            this.toolStripButtonReverse.Click += new System.EventHandler(this.toolStripButtonReverse_Click);
            this.toolStripButtonReverse.MouseEnter += new System.EventHandler(this.toolStripButtonReverse_MouseEnter);
            this.toolStripButtonReverse.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonAdvancedScripting
            // 
            this.toolStripButtonAdvancedScripting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonAdvancedScripting.Image = global::WebMConverter.Properties.Resources.cross;
            this.toolStripButtonAdvancedScripting.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonAdvancedScripting.Name = "toolStripButtonAdvancedScripting";
            this.toolStripButtonAdvancedScripting.Size = new System.Drawing.Size(80, 22);
            this.toolStripButtonAdvancedScripting.Text = "Advanced";
            this.toolStripButtonAdvancedScripting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonAdvancedScripting.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonAdvancedScripting.CheckedChanged += new System.EventHandler(this.toolStripButtonAdvancedScripting_CheckedChanged);
            this.toolStripButtonAdvancedScripting.Click += new System.EventHandler(this.toolStripButtonAdvancedScripting_Click);
            this.toolStripButtonAdvancedScripting.MouseEnter += new System.EventHandler(this.toolStripButtonAdvancedScripting_MouseEnter);
            this.toolStripButtonAdvancedScripting.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPreview.Enabled = false;
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(84, 22);
            this.buttonPreview.Text = "Preview filters";
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            this.buttonPreview.MouseEnter += new System.EventHandler(this.buttonPreview_MouseEnter);
            this.buttonPreview.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonOverlay
            // 
            this.toolStripButtonOverlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonOverlay.Enabled = false;
            this.toolStripButtonOverlay.Name = "toolStripButtonOverlay";
            this.toolStripButtonOverlay.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonOverlay.Text = "Overlay";
            this.toolStripButtonOverlay.Click += new System.EventHandler(this.toolStripButtonOverlay_Click);
            this.toolStripButtonOverlay.MouseEnter += new System.EventHandler(this.toolStripButtonOverlay_MouseEnter);
            this.toolStripButtonOverlay.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // toolStripButtonCaption
            // 
            this.toolStripButtonCaption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCaption.Enabled = false;
            this.toolStripButtonCaption.Name = "toolStripButtonCaption";
            this.toolStripButtonCaption.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonCaption.Text = "Caption";
            this.toolStripButtonCaption.Click += new System.EventHandler(this.toolStripButtonCaption_Click);
            this.toolStripButtonCaption.MouseEnter += new System.EventHandler(this.toolStripButtonCaption_MouseEnter);
            this.toolStripButtonCaption.MouseLeave += new System.EventHandler(this.clearToolTip);
            // 
            // panelProcessingScriptInput
            // 
            this.panelProcessingScriptInput.Controls.Add(this.listViewProcessingScript);
            this.panelProcessingScriptInput.Controls.Add(this.textBoxProcessingScript);
            this.panelProcessingScriptInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProcessingScriptInput.Location = new System.Drawing.Point(0, 25);
            this.panelProcessingScriptInput.Margin = new System.Windows.Forms.Padding(0);
            this.panelProcessingScriptInput.Name = "panelProcessingScriptInput";
            this.panelProcessingScriptInput.Size = new System.Drawing.Size(1047, 276);
            this.panelProcessingScriptInput.TabIndex = 1;
            // 
            // listViewProcessingScript
            // 
            this.listViewProcessingScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProcessingScript.Location = new System.Drawing.Point(0, 0);
            this.listViewProcessingScript.Margin = new System.Windows.Forms.Padding(0);
            this.listViewProcessingScript.Name = "listViewProcessingScript";
            this.listViewProcessingScript.Size = new System.Drawing.Size(1047, 276);
            this.listViewProcessingScript.TabIndex = 3;
            this.listViewProcessingScript.UseCompatibleStateImageBehavior = false;
            this.listViewProcessingScript.ItemActivate += new System.EventHandler(this.listViewProcessingScript_ItemActivate);
            this.listViewProcessingScript.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewProcessingScript_KeyUp);
            this.listViewProcessingScript.MouseEnter += new System.EventHandler(this.listViewProcessingScript_MouseEnter);
            this.listViewProcessingScript.MouseLeave += new System.EventHandler(this.clearToolTip);
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
            // tabPageEncoding
            // 
            this.tabPageEncoding.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEncoding.Controls.Add(this.tableLayoutPanelEncoding);
            this.tabPageEncoding.Location = new System.Drawing.Point(4, 22);
            this.tabPageEncoding.Name = "tabPageEncoding";
            this.tabPageEncoding.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEncoding.Size = new System.Drawing.Size(1053, 307);
            this.tabPageEncoding.TabIndex = 0;
            this.tabPageEncoding.Text = "Encoding";
            // 
            // tableLayoutPanelEncoding
            // 
            this.tableLayoutPanelEncoding.ColumnCount = 1;
            this.tableLayoutPanelEncoding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncoding.Controls.Add(this.groupBoxEncodingGeneral, 0, 0);
            this.tableLayoutPanelEncoding.Controls.Add(this.groupBoxEncodingVideo, 0, 1);
            this.tableLayoutPanelEncoding.Controls.Add(this.groupBoxEncodingAudio, 0, 2);
            this.tableLayoutPanelEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncoding.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelEncoding.Name = "tableLayoutPanelEncoding";
            this.tableLayoutPanelEncoding.RowCount = 4;
            this.tableLayoutPanelEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncoding.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncoding.Size = new System.Drawing.Size(1047, 301);
            this.tableLayoutPanelEncoding.TabIndex = 0;
            // 
            // groupBoxEncodingGeneral
            // 
            this.groupBoxEncodingGeneral.Controls.Add(this.tableLayoutPanelEncodingGeneral);
            this.groupBoxEncodingGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEncodingGeneral.Location = new System.Drawing.Point(3, 3);
            this.groupBoxEncodingGeneral.Name = "groupBoxEncodingGeneral";
            this.groupBoxEncodingGeneral.Size = new System.Drawing.Size(1041, 73);
            this.groupBoxEncodingGeneral.TabIndex = 0;
            this.groupBoxEncodingGeneral.TabStop = false;
            this.groupBoxEncodingGeneral.Text = "General";
            // 
            // tableLayoutPanelEncodingGeneral
            // 
            this.tableLayoutPanelEncodingGeneral.ColumnCount = 4;
            this.tableLayoutPanelEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanelEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.tableLayoutPanelEncodingGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.labelGeneralSizeLimitHint, 2, 1);
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.tableLayoutPanelGeneralSizeLimit, 1, 1);
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.labelGeneralSizeLimit, 0, 1);
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.labelGeneralTitle, 0, 0);
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.labelMetadataTitleHint, 3, 0);
            this.tableLayoutPanelEncodingGeneral.Controls.Add(this.boxMetadataTitle, 1, 0);
            this.tableLayoutPanelEncodingGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncodingGeneral.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelEncodingGeneral.Name = "tableLayoutPanelEncodingGeneral";
            this.tableLayoutPanelEncodingGeneral.RowCount = 2;
            this.tableLayoutPanelEncodingGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingGeneral.Size = new System.Drawing.Size(1035, 54);
            this.tableLayoutPanelEncodingGeneral.TabIndex = 0;
            // 
            // labelGeneralSizeLimitHint
            // 
            this.labelGeneralSizeLimitHint.AutoSize = true;
            this.tableLayoutPanelEncodingGeneral.SetColumnSpan(this.labelGeneralSizeLimitHint, 2);
            this.labelGeneralSizeLimitHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGeneralSizeLimitHint.Location = new System.Drawing.Point(230, 28);
            this.labelGeneralSizeLimitHint.Name = "labelGeneralSizeLimitHint";
            this.labelGeneralSizeLimitHint.Size = new System.Drawing.Size(802, 28);
            this.labelGeneralSizeLimitHint.TabIndex = 12;
            this.labelGeneralSizeLimitHint.Text = "Will adjust the quality to attempt to stay below this limit, and cut off the end " +
    "of a video if needed. Leave blank for no limit. The limit on 4chan is 3 MB.";
            this.labelGeneralSizeLimitHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanelGeneralSizeLimit
            // 
            this.tableLayoutPanelGeneralSizeLimit.ColumnCount = 2;
            this.tableLayoutPanelGeneralSizeLimit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGeneralSizeLimit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelGeneralSizeLimit.Controls.Add(this.boxLimit, 0, 0);
            this.tableLayoutPanelGeneralSizeLimit.Controls.Add(this.labelGeneralSizeLimitUnit, 1, 0);
            this.tableLayoutPanelGeneralSizeLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGeneralSizeLimit.Location = new System.Drawing.Point(82, 28);
            this.tableLayoutPanelGeneralSizeLimit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanelGeneralSizeLimit.Name = "tableLayoutPanelGeneralSizeLimit";
            this.tableLayoutPanelGeneralSizeLimit.RowCount = 1;
            this.tableLayoutPanelGeneralSizeLimit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGeneralSizeLimit.Size = new System.Drawing.Size(142, 28);
            this.tableLayoutPanelGeneralSizeLimit.TabIndex = 11;
            // 
            // boxLimit
            // 
            this.boxLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxLimit.Location = new System.Drawing.Point(3, 4);
            this.boxLimit.Name = "boxLimit";
            this.boxLimit.Size = new System.Drawing.Size(96, 20);
            this.boxLimit.TabIndex = 0;
            this.boxLimit.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelGeneralSizeLimitUnit
            // 
            this.labelGeneralSizeLimitUnit.AutoSize = true;
            this.labelGeneralSizeLimitUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGeneralSizeLimitUnit.Location = new System.Drawing.Point(105, 0);
            this.labelGeneralSizeLimitUnit.Name = "labelGeneralSizeLimitUnit";
            this.labelGeneralSizeLimitUnit.Size = new System.Drawing.Size(34, 28);
            this.labelGeneralSizeLimitUnit.TabIndex = 1;
            this.labelGeneralSizeLimitUnit.Text = "MB";
            this.labelGeneralSizeLimitUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGeneralSizeLimit
            // 
            this.labelGeneralSizeLimit.AutoSize = true;
            this.labelGeneralSizeLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGeneralSizeLimit.Location = new System.Drawing.Point(3, 28);
            this.labelGeneralSizeLimit.Name = "labelGeneralSizeLimit";
            this.labelGeneralSizeLimit.Size = new System.Drawing.Size(73, 28);
            this.labelGeneralSizeLimit.TabIndex = 10;
            this.labelGeneralSizeLimit.Text = "Size limit:";
            this.labelGeneralSizeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelGeneralTitle
            // 
            this.labelGeneralTitle.AutoSize = true;
            this.labelGeneralTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGeneralTitle.Location = new System.Drawing.Point(3, 0);
            this.labelGeneralTitle.Name = "labelGeneralTitle";
            this.labelGeneralTitle.Size = new System.Drawing.Size(73, 28);
            this.labelGeneralTitle.TabIndex = 0;
            this.labelGeneralTitle.Text = "Title:";
            this.labelGeneralTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelMetadataTitleHint
            // 
            this.labelMetadataTitleHint.AutoSize = true;
            this.labelMetadataTitleHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMetadataTitleHint.Location = new System.Drawing.Point(478, 0);
            this.labelMetadataTitleHint.Name = "labelMetadataTitleHint";
            this.labelMetadataTitleHint.Size = new System.Drawing.Size(554, 28);
            this.labelMetadataTitleHint.TabIndex = 2;
            this.labelMetadataTitleHint.Text = "Adds a string of text to the metadata of the video, which can be used to indicate" +
    " the source of a video, for example. Leave blank for no title.";
            this.labelMetadataTitleHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxMetadataTitle
            // 
            this.boxMetadataTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelEncodingGeneral.SetColumnSpan(this.boxMetadataTitle, 2);
            this.boxMetadataTitle.Location = new System.Drawing.Point(85, 4);
            this.boxMetadataTitle.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.boxMetadataTitle.Name = "boxMetadataTitle";
            this.boxMetadataTitle.Size = new System.Drawing.Size(384, 20);
            this.boxMetadataTitle.TabIndex = 0;
            this.boxMetadataTitle.TextChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // groupBoxEncodingVideo
            // 
            this.groupBoxEncodingVideo.Controls.Add(this.tableLayoutPanelEncodingVideo);
            this.groupBoxEncodingVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEncodingVideo.Location = new System.Drawing.Point(3, 82);
            this.groupBoxEncodingVideo.Name = "groupBoxEncodingVideo";
            this.groupBoxEncodingVideo.Size = new System.Drawing.Size(1041, 73);
            this.groupBoxEncodingVideo.TabIndex = 1;
            this.groupBoxEncodingVideo.TabStop = false;
            this.groupBoxEncodingVideo.Text = "Video";
            // 
            // tableLayoutPanelEncodingVideo
            // 
            this.tableLayoutPanelEncodingVideo.ColumnCount = 3;
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanelEncodingVideo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.labelVideoBitrate, 0, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.tableLayoutPanelVideoBitrate, 1, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.labelVideoBitrateHint, 3, 0);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.boxHQ, 0, 1);
            this.tableLayoutPanelEncodingVideo.Controls.Add(this.labelVideoHQHint, 2, 1);
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
            this.labelVideoBitrate.AutoSize = true;
            this.labelVideoBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoBitrate.Location = new System.Drawing.Point(3, 0);
            this.labelVideoBitrate.Name = "labelVideoBitrate";
            this.labelVideoBitrate.Size = new System.Drawing.Size(73, 28);
            this.labelVideoBitrate.TabIndex = 6;
            this.labelVideoBitrate.Text = "Bitrate:";
            this.labelVideoBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanelVideoBitrate
            // 
            this.tableLayoutPanelVideoBitrate.ColumnCount = 2;
            this.tableLayoutPanelVideoBitrate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelVideoBitrate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelVideoBitrate.Controls.Add(this.boxVideoBitrate, 0, 0);
            this.tableLayoutPanelVideoBitrate.Controls.Add(this.labelVideoBitrateUnit, 1, 0);
            this.tableLayoutPanelVideoBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelVideoBitrate.Location = new System.Drawing.Point(82, 0);
            this.tableLayoutPanelVideoBitrate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanelVideoBitrate.Name = "tableLayoutPanelVideoBitrate";
            this.tableLayoutPanelVideoBitrate.RowCount = 1;
            this.tableLayoutPanelVideoBitrate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelVideoBitrate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelVideoBitrate.Size = new System.Drawing.Size(142, 28);
            this.tableLayoutPanelVideoBitrate.TabIndex = 0;
            // 
            // boxVideoBitrate
            // 
            this.boxVideoBitrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxVideoBitrate.Location = new System.Drawing.Point(3, 4);
            this.boxVideoBitrate.Name = "boxVideoBitrate";
            this.boxVideoBitrate.Size = new System.Drawing.Size(96, 20);
            this.boxVideoBitrate.TabIndex = 0;
            this.boxVideoBitrate.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxVideoBitrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelVideoBitrateUnit
            // 
            this.labelVideoBitrateUnit.AutoSize = true;
            this.labelVideoBitrateUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoBitrateUnit.Location = new System.Drawing.Point(105, 0);
            this.labelVideoBitrateUnit.Name = "labelVideoBitrateUnit";
            this.labelVideoBitrateUnit.Size = new System.Drawing.Size(34, 28);
            this.labelVideoBitrateUnit.TabIndex = 1;
            this.labelVideoBitrateUnit.Text = "Kb/s";
            this.labelVideoBitrateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVideoBitrateHint
            // 
            this.labelVideoBitrateHint.AutoSize = true;
            this.labelVideoBitrateHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoBitrateHint.Location = new System.Drawing.Point(230, 0);
            this.labelVideoBitrateHint.Name = "labelVideoBitrateHint";
            this.labelVideoBitrateHint.Size = new System.Drawing.Size(802, 28);
            this.labelVideoBitrateHint.TabIndex = 8;
            this.labelVideoBitrateHint.Text = "Determines the quality of the video. Keep blank to let the program pick one based" +
    " on size limit and duration.";
            this.labelVideoBitrateHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxHQ
            // 
            this.boxHQ.AutoSize = true;
            this.boxHQ.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanelEncodingVideo.SetColumnSpan(this.boxHQ, 2);
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
            this.labelVideoHQHint.AutoSize = true;
            this.labelVideoHQHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoHQHint.Location = new System.Drawing.Point(230, 28);
            this.labelVideoHQHint.Name = "labelVideoHQHint";
            this.labelVideoHQHint.Size = new System.Drawing.Size(802, 28);
            this.labelVideoHQHint.TabIndex = 16;
            this.labelVideoHQHint.Text = "Enables two-pass encoding and adds some extra encoding arguments, increasing outp" +
    "ut quality, but increases the time it takes to encode your file.";
            this.labelVideoHQHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxEncodingAudio
            // 
            this.groupBoxEncodingAudio.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxEncodingAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEncodingAudio.Location = new System.Drawing.Point(3, 161);
            this.groupBoxEncodingAudio.Name = "groupBoxEncodingAudio";
            this.groupBoxEncodingAudio.Size = new System.Drawing.Size(1041, 73);
            this.groupBoxEncodingAudio.TabIndex = 2;
            this.groupBoxEncodingAudio.TabStop = false;
            this.groupBoxEncodingAudio.Text = "Audio";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelAudioBitrate, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelGeneralAudioHint, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.boxAudio, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelAudioBitrate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelAudioBitrateHint, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1035, 54);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanelAudioBitrate
            // 
            this.tableLayoutPanelAudioBitrate.ColumnCount = 2;
            this.tableLayoutPanelAudioBitrate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAudioBitrate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelAudioBitrate.Controls.Add(this.boxAudioBitrate, 0, 0);
            this.tableLayoutPanelAudioBitrate.Controls.Add(this.labelAudioBitrateUnit, 1, 0);
            this.tableLayoutPanelAudioBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAudioBitrate.Location = new System.Drawing.Point(82, 27);
            this.tableLayoutPanelAudioBitrate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanelAudioBitrate.Name = "tableLayoutPanelAudioBitrate";
            this.tableLayoutPanelAudioBitrate.RowCount = 1;
            this.tableLayoutPanelAudioBitrate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAudioBitrate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelAudioBitrate.Size = new System.Drawing.Size(142, 27);
            this.tableLayoutPanelAudioBitrate.TabIndex = 21;
            // 
            // boxAudioBitrate
            // 
            this.boxAudioBitrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxAudioBitrate.Enabled = false;
            this.boxAudioBitrate.Location = new System.Drawing.Point(3, 3);
            this.boxAudioBitrate.Name = "boxAudioBitrate";
            this.boxAudioBitrate.Size = new System.Drawing.Size(96, 20);
            this.boxAudioBitrate.TabIndex = 0;
            this.boxAudioBitrate.TextChanged += new System.EventHandler(this.UpdateArguments);
            this.boxAudioBitrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNumbersOnly);
            // 
            // labelAudioBitrateUnit
            // 
            this.labelAudioBitrateUnit.AutoSize = true;
            this.labelAudioBitrateUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAudioBitrateUnit.Location = new System.Drawing.Point(105, 0);
            this.labelAudioBitrateUnit.Name = "labelAudioBitrateUnit";
            this.labelAudioBitrateUnit.Size = new System.Drawing.Size(34, 27);
            this.labelAudioBitrateUnit.TabIndex = 1;
            this.labelAudioBitrateUnit.Text = "Kb/s";
            this.labelAudioBitrateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGeneralAudioHint
            // 
            this.labelGeneralAudioHint.AutoSize = true;
            this.labelGeneralAudioHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGeneralAudioHint.Location = new System.Drawing.Point(230, 0);
            this.labelGeneralAudioHint.Name = "labelGeneralAudioHint";
            this.labelGeneralAudioHint.Size = new System.Drawing.Size(802, 27);
            this.labelGeneralAudioHint.TabIndex = 19;
            this.labelGeneralAudioHint.Text = "Keep this disabled until Moot allows audio.";
            this.labelGeneralAudioHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxAudio
            // 
            this.boxAudio.AutoSize = true;
            this.boxAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel2.SetColumnSpan(this.boxAudio, 2);
            this.boxAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxAudio.Location = new System.Drawing.Point(6, 3);
            this.boxAudio.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.boxAudio.Name = "boxAudio";
            this.boxAudio.Size = new System.Drawing.Size(215, 21);
            this.boxAudio.TabIndex = 4;
            this.boxAudio.Text = "Enable audio:";
            this.boxAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxAudio.UseVisualStyleBackColor = true;
            this.boxAudio.CheckedChanged += new System.EventHandler(this.boxAudio_CheckedChanged);
            // 
            // labelAudioBitrate
            // 
            this.labelAudioBitrate.AutoSize = true;
            this.labelAudioBitrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAudioBitrate.Location = new System.Drawing.Point(3, 27);
            this.labelAudioBitrate.Name = "labelAudioBitrate";
            this.labelAudioBitrate.Size = new System.Drawing.Size(73, 27);
            this.labelAudioBitrate.TabIndex = 20;
            this.labelAudioBitrate.Text = "Bitrate:";
            this.labelAudioBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAudioBitrateHint
            // 
            this.labelAudioBitrateHint.AutoSize = true;
            this.labelAudioBitrateHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAudioBitrateHint.Location = new System.Drawing.Point(230, 27);
            this.labelAudioBitrateHint.Name = "labelAudioBitrateHint";
            this.labelAudioBitrateHint.Size = new System.Drawing.Size(802, 27);
            this.labelAudioBitrateHint.TabIndex = 22;
            this.labelAudioBitrateHint.Text = "Determines the quality of the audio.";
            this.labelAudioBitrateHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAdvanced.Controls.Add(this.tableLayoutPanel1);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(1053, 307);
            this.tabPageAdvanced.TabIndex = 4;
            this.tabPageAdvanced.Text = "Advanced";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAdvancedEncoding, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelAdvancedWarning, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxAdvancedProcessing, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 301);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBoxAdvancedEncoding
            // 
            this.groupBoxAdvancedEncoding.AutoSize = true;
            this.groupBoxAdvancedEncoding.Controls.Add(this.tableLayoutPanelEncodingAdvanced);
            this.groupBoxAdvancedEncoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAdvancedEncoding.Location = new System.Drawing.Point(3, 141);
            this.groupBoxAdvancedEncoding.Name = "groupBoxAdvancedEncoding";
            this.groupBoxAdvancedEncoding.Size = new System.Drawing.Size(1041, 132);
            this.groupBoxAdvancedEncoding.TabIndex = 33;
            this.groupBoxAdvancedEncoding.TabStop = false;
            this.groupBoxAdvancedEncoding.Text = "Encoding";
            // 
            // tableLayoutPanelEncodingAdvanced
            // 
            this.tableLayoutPanelEncodingAdvanced.ColumnCount = 3;
            this.tableLayoutPanelEncodingAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelEncodingAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanelEncodingAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelVideoCrfHint, 2, 2);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.numericCrf, 1, 2);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelVideoCrf, 0, 2);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.tableLayoutPanelEncodingSlices, 1, 1);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelEncodingThreads, 0, 0);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.tableLayoutPanelEncodingThreads, 1, 0);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelEncodingThreadsHint, 2, 0);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelEncodingArguments, 0, 3);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.textBoxArguments, 1, 3);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelEncodingSlices, 0, 1);
            this.tableLayoutPanelEncodingAdvanced.Controls.Add(this.labelEncodingSlicesHint, 2, 1);
            this.tableLayoutPanelEncodingAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncodingAdvanced.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelEncodingAdvanced.Name = "tableLayoutPanelEncodingAdvanced";
            this.tableLayoutPanelEncodingAdvanced.RowCount = 4;
            this.tableLayoutPanelEncodingAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelEncodingAdvanced.Size = new System.Drawing.Size(1035, 113);
            this.tableLayoutPanelEncodingAdvanced.TabIndex = 0;
            // 
            // tableLayoutPanelEncodingSlices
            // 
            this.tableLayoutPanelEncodingSlices.ColumnCount = 2;
            this.tableLayoutPanelEncodingSlices.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.18421F));
            this.tableLayoutPanelEncodingSlices.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.81579F));
            this.tableLayoutPanelEncodingSlices.Controls.Add(this.trackSlices, 0, 0);
            this.tableLayoutPanelEncodingSlices.Controls.Add(this.labelSlices, 1, 0);
            this.tableLayoutPanelEncodingSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncodingSlices.Location = new System.Drawing.Point(82, 31);
            this.tableLayoutPanelEncodingSlices.Name = "tableLayoutPanelEncodingSlices";
            this.tableLayoutPanelEncodingSlices.RowCount = 1;
            this.tableLayoutPanelEncodingSlices.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEncodingSlices.Size = new System.Drawing.Size(142, 22);
            this.tableLayoutPanelEncodingSlices.TabIndex = 31;
            // 
            // trackSlices
            // 
            this.trackSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackSlices.Location = new System.Drawing.Point(0, 0);
            this.trackSlices.Margin = new System.Windows.Forms.Padding(0);
            this.trackSlices.Maximum = 4;
            this.trackSlices.Minimum = 1;
            this.trackSlices.Name = "trackSlices";
            this.trackSlices.Size = new System.Drawing.Size(122, 22);
            this.trackSlices.TabIndex = 0;
            this.trackSlices.Value = 1;
            this.trackSlices.ValueChanged += new System.EventHandler(this.trackSlices_Scroll);
            // 
            // labelSlices
            // 
            this.labelSlices.AutoSize = true;
            this.labelSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSlices.Location = new System.Drawing.Point(122, 0);
            this.labelSlices.Margin = new System.Windows.Forms.Padding(0);
            this.labelSlices.Name = "labelSlices";
            this.labelSlices.Size = new System.Drawing.Size(20, 22);
            this.labelSlices.TabIndex = 1;
            this.labelSlices.Text = "1";
            this.labelSlices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingThreads
            // 
            this.labelEncodingThreads.AutoSize = true;
            this.labelEncodingThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEncodingThreads.Location = new System.Drawing.Point(3, 0);
            this.labelEncodingThreads.Name = "labelEncodingThreads";
            this.labelEncodingThreads.Size = new System.Drawing.Size(73, 28);
            this.labelEncodingThreads.TabIndex = 28;
            this.labelEncodingThreads.Text = "Threads:";
            this.labelEncodingThreads.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanelEncodingThreads
            // 
            this.tableLayoutPanelEncodingThreads.ColumnCount = 2;
            this.tableLayoutPanelEncodingThreads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.18421F));
            this.tableLayoutPanelEncodingThreads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.81579F));
            this.tableLayoutPanelEncodingThreads.Controls.Add(this.trackThreads, 0, 0);
            this.tableLayoutPanelEncodingThreads.Controls.Add(this.labelThreads, 1, 0);
            this.tableLayoutPanelEncodingThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEncodingThreads.Location = new System.Drawing.Point(82, 3);
            this.tableLayoutPanelEncodingThreads.Name = "tableLayoutPanelEncodingThreads";
            this.tableLayoutPanelEncodingThreads.RowCount = 1;
            this.tableLayoutPanelEncodingThreads.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEncodingThreads.Size = new System.Drawing.Size(142, 22);
            this.tableLayoutPanelEncodingThreads.TabIndex = 0;
            // 
            // trackThreads
            // 
            this.trackThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackThreads.Location = new System.Drawing.Point(0, 0);
            this.trackThreads.Margin = new System.Windows.Forms.Padding(0);
            this.trackThreads.Maximum = 16;
            this.trackThreads.Minimum = 1;
            this.trackThreads.Name = "trackThreads";
            this.trackThreads.Size = new System.Drawing.Size(122, 22);
            this.trackThreads.TabIndex = 0;
            this.trackThreads.Value = 1;
            this.trackThreads.ValueChanged += new System.EventHandler(this.trackThreads_Scroll);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelThreads.Location = new System.Drawing.Point(122, 0);
            this.labelThreads.Margin = new System.Windows.Forms.Padding(0);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(20, 22);
            this.labelThreads.TabIndex = 1;
            this.labelThreads.Text = "1";
            this.labelThreads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingThreadsHint
            // 
            this.labelEncodingThreadsHint.AutoSize = true;
            this.labelEncodingThreadsHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEncodingThreadsHint.Location = new System.Drawing.Point(230, 0);
            this.labelEncodingThreadsHint.Name = "labelEncodingThreadsHint";
            this.labelEncodingThreadsHint.Size = new System.Drawing.Size(802, 28);
            this.labelEncodingThreadsHint.TabIndex = 27;
            this.labelEncodingThreadsHint.Text = "Determines amount of threads ffmpeg uses. Try setting this to 1 if ffmpeg.exe cra" +
    "shes as soon as you click Convert.";
            this.labelEncodingThreadsHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEncodingArguments
            // 
            this.labelEncodingArguments.AutoSize = true;
            this.labelEncodingArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEncodingArguments.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelEncodingArguments.Location = new System.Drawing.Point(3, 84);
            this.labelEncodingArguments.Name = "labelEncodingArguments";
            this.labelEncodingArguments.Size = new System.Drawing.Size(73, 29);
            this.labelEncodingArguments.TabIndex = 29;
            this.labelEncodingArguments.Text = "Arguments:";
            this.labelEncodingArguments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxArguments
            // 
            this.textBoxArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelEncodingAdvanced.SetColumnSpan(this.textBoxArguments, 2);
            this.textBoxArguments.Location = new System.Drawing.Point(85, 88);
            this.textBoxArguments.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.textBoxArguments.Name = "textBoxArguments";
            this.textBoxArguments.Size = new System.Drawing.Size(944, 20);
            this.textBoxArguments.TabIndex = 1;
            // 
            // labelEncodingSlices
            // 
            this.labelEncodingSlices.AutoSize = true;
            this.labelEncodingSlices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEncodingSlices.Location = new System.Drawing.Point(3, 28);
            this.labelEncodingSlices.Name = "labelEncodingSlices";
            this.labelEncodingSlices.Size = new System.Drawing.Size(73, 28);
            this.labelEncodingSlices.TabIndex = 30;
            this.labelEncodingSlices.Text = "Slices:";
            this.labelEncodingSlices.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEncodingSlicesHint
            // 
            this.labelEncodingSlicesHint.AutoSize = true;
            this.labelEncodingSlicesHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEncodingSlicesHint.Location = new System.Drawing.Point(230, 28);
            this.labelEncodingSlicesHint.Name = "labelEncodingSlicesHint";
            this.labelEncodingSlicesHint.Size = new System.Drawing.Size(802, 28);
            this.labelEncodingSlicesHint.TabIndex = 32;
            this.labelEncodingSlicesHint.Text = "Split frames into slices before encoding them. Results in a higher quality per fr" +
    "ame. 4 slices is standard for 720p resolutions.";
            this.labelEncodingSlicesHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAdvancedWarning
            // 
            this.labelAdvancedWarning.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelAdvancedWarning, 3);
            this.labelAdvancedWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAdvancedWarning.Location = new System.Drawing.Point(3, 3);
            this.labelAdvancedWarning.Margin = new System.Windows.Forms.Padding(3);
            this.labelAdvancedWarning.Name = "labelAdvancedWarning";
            this.labelAdvancedWarning.Size = new System.Drawing.Size(1041, 22);
            this.labelAdvancedWarning.TabIndex = 32;
            this.labelAdvancedWarning.Text = "Do not modify these settings unless you are 100% sure you know what you\'re doing." +
    "";
            this.labelAdvancedWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxAdvancedProcessing
            // 
            this.groupBoxAdvancedProcessing.Controls.Add(this.tableLayoutPanelAdvancedProcessing);
            this.groupBoxAdvancedProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAdvancedProcessing.Location = new System.Drawing.Point(3, 31);
            this.groupBoxAdvancedProcessing.Name = "groupBoxAdvancedProcessing";
            this.groupBoxAdvancedProcessing.Size = new System.Drawing.Size(1041, 104);
            this.groupBoxAdvancedProcessing.TabIndex = 0;
            this.groupBoxAdvancedProcessing.TabStop = false;
            this.groupBoxAdvancedProcessing.Text = "Processing";
            // 
            // tableLayoutPanelAdvancedProcessing
            // 
            this.tableLayoutPanelAdvancedProcessing.ColumnCount = 3;
            this.tableLayoutPanelAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanelAdvancedProcessing.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.labelProcessingDenoiseHint, 2, 2);
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.boxDenoise, 0, 2);
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.labelProcessingLevelsHint, 2, 0);
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.labelProcessingDeinterlaceHint, 2, 1);
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.boxDeinterlace, 0, 1);
            this.tableLayoutPanelAdvancedProcessing.Controls.Add(this.boxLevels, 0, 0);
            this.tableLayoutPanelAdvancedProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAdvancedProcessing.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelAdvancedProcessing.Name = "tableLayoutPanelAdvancedProcessing";
            this.tableLayoutPanelAdvancedProcessing.RowCount = 3;
            this.tableLayoutPanelAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelAdvancedProcessing.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelAdvancedProcessing.Size = new System.Drawing.Size(1035, 85);
            this.tableLayoutPanelAdvancedProcessing.TabIndex = 1;
            // 
            // labelProcessingDenoiseHint
            // 
            this.labelProcessingDenoiseHint.AutoSize = true;
            this.labelProcessingDenoiseHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProcessingDenoiseHint.Location = new System.Drawing.Point(230, 56);
            this.labelProcessingDenoiseHint.Name = "labelProcessingDenoiseHint";
            this.labelProcessingDenoiseHint.Size = new System.Drawing.Size(802, 29);
            this.labelProcessingDenoiseHint.TabIndex = 36;
            this.labelProcessingDenoiseHint.Text = "Denoise the video, resulting in less detailed video but more bang for your buck w" +
    "hen it comes to bitrate.";
            this.labelProcessingDenoiseHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxDenoise
            // 
            this.boxDenoise.AutoSize = true;
            this.boxDenoise.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanelAdvancedProcessing.SetColumnSpan(this.boxDenoise, 2);
            this.boxDenoise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxDenoise.Location = new System.Drawing.Point(3, 59);
            this.boxDenoise.Name = "boxDenoise";
            this.boxDenoise.Size = new System.Drawing.Size(221, 23);
            this.boxDenoise.TabIndex = 35;
            this.boxDenoise.Text = "Denoise:";
            this.boxDenoise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDenoise.UseVisualStyleBackColor = true;
            this.boxDenoise.CheckedChanged += new System.EventHandler(this.boxDenoise_CheckedChanged);
            // 
            // labelProcessingLevelsHint
            // 
            this.labelProcessingLevelsHint.AutoSize = true;
            this.labelProcessingLevelsHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProcessingLevelsHint.Location = new System.Drawing.Point(230, 0);
            this.labelProcessingLevelsHint.Name = "labelProcessingLevelsHint";
            this.labelProcessingLevelsHint.Size = new System.Drawing.Size(802, 28);
            this.labelProcessingLevelsHint.TabIndex = 27;
            this.labelProcessingLevelsHint.Text = "Movies usually store colors in the 16->235 range, which can make colours look was" +
    "hed out in browsers. This option expands the color levels to the full range.";
            this.labelProcessingLevelsHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProcessingDeinterlaceHint
            // 
            this.labelProcessingDeinterlaceHint.AutoSize = true;
            this.labelProcessingDeinterlaceHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProcessingDeinterlaceHint.Location = new System.Drawing.Point(230, 28);
            this.labelProcessingDeinterlaceHint.Name = "labelProcessingDeinterlaceHint";
            this.labelProcessingDeinterlaceHint.Size = new System.Drawing.Size(802, 28);
            this.labelProcessingDeinterlaceHint.TabIndex = 32;
            this.labelProcessingDeinterlaceHint.Text = "Attempt to deinterlace an interlaced input video.";
            this.labelProcessingDeinterlaceHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxDeinterlace
            // 
            this.boxDeinterlace.AutoSize = true;
            this.boxDeinterlace.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanelAdvancedProcessing.SetColumnSpan(this.boxDeinterlace, 2);
            this.boxDeinterlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxDeinterlace.Location = new System.Drawing.Point(3, 31);
            this.boxDeinterlace.Name = "boxDeinterlace";
            this.boxDeinterlace.Size = new System.Drawing.Size(221, 22);
            this.boxDeinterlace.TabIndex = 34;
            this.boxDeinterlace.Text = "Deinterlace:";
            this.boxDeinterlace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxDeinterlace.UseVisualStyleBackColor = true;
            this.boxDeinterlace.CheckedChanged += new System.EventHandler(this.boxDeinterlace_CheckedChanged);
            // 
            // boxLevels
            // 
            this.boxLevels.AutoSize = true;
            this.boxLevels.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanelAdvancedProcessing.SetColumnSpan(this.boxLevels, 2);
            this.boxLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxLevels.Location = new System.Drawing.Point(3, 3);
            this.boxLevels.Name = "boxLevels";
            this.boxLevels.Size = new System.Drawing.Size(221, 22);
            this.boxLevels.TabIndex = 33;
            this.boxLevels.Text = "Expand color ranges:";
            this.boxLevels.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxLevels.UseVisualStyleBackColor = true;
            this.boxLevels.CheckedChanged += new System.EventHandler(this.boxLevels_CheckedChanged);
            // 
            // panelHideTheOptions
            // 
            this.panelHideTheOptions.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelHideTheOptions.Controls.Add(this.panelContainTheProgressBar);
            this.panelHideTheOptions.Location = new System.Drawing.Point(3, 88);
            this.panelHideTheOptions.Name = "panelHideTheOptions";
            this.panelHideTheOptions.Size = new System.Drawing.Size(1067, 356);
            this.panelHideTheOptions.TabIndex = 3;
            // 
            // panelContainTheProgressBar
            // 
            this.panelContainTheProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelContainTheProgressBar.AutoSize = true;
            this.panelContainTheProgressBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelContainTheProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.panelContainTheProgressBar.Controls.Add(this.labelIndexingProgress);
            this.panelContainTheProgressBar.Controls.Add(this.progressBarIndexing);
            this.panelContainTheProgressBar.Location = new System.Drawing.Point(299, 148);
            this.panelContainTheProgressBar.Name = "panelContainTheProgressBar";
            this.panelContainTheProgressBar.Size = new System.Drawing.Size(469, 61);
            this.panelContainTheProgressBar.TabIndex = 0;
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
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(3, 424);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1067, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(1052, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVideoCrf
            // 
            this.labelVideoCrf.AutoSize = true;
            this.labelVideoCrf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoCrf.Location = new System.Drawing.Point(3, 56);
            this.labelVideoCrf.Name = "labelVideoCrf";
            this.labelVideoCrf.Size = new System.Drawing.Size(73, 28);
            this.labelVideoCrf.TabIndex = 33;
            this.labelVideoCrf.Text = "CRF:";
            this.labelVideoCrf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericCrf
            // 
            this.numericCrf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericCrf.Location = new System.Drawing.Point(82, 59);
            this.numericCrf.Maximum = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.numericCrf.Name = "numericCrf";
            this.numericCrf.Size = new System.Drawing.Size(142, 20);
            this.numericCrf.TabIndex = 34;
            this.numericCrf.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericCrf.ValueChanged += new System.EventHandler(this.UpdateArguments);
            // 
            // labelVideoCrfHint
            // 
            this.labelVideoCrfHint.AutoSize = true;
            this.labelVideoCrfHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVideoCrfHint.Location = new System.Drawing.Point(230, 56);
            this.labelVideoCrfHint.Name = "labelVideoCrfHint";
            this.labelVideoCrfHint.Size = new System.Drawing.Size(802, 28);
            this.labelVideoCrfHint.TabIndex = 35;
            this.labelVideoCrfHint.Text = "Constant rate factor for the video; basically, the overall quality. A lower value" +
    " means higher quality and filesize.";
            this.labelVideoCrfHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonGo;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 446);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tableLayoutPanelMainForm);
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
            this.tableLayoutPanelMainForm.ResumeLayout(false);
            this.groupBoxMain.ResumeLayout(false);
            this.tableLayoutPanelGroupMain.ResumeLayout(false);
            this.tableLayoutPanelGroupMain.PerformLayout();
            this.tabControlOptions.ResumeLayout(false);
            this.tabPageProcessing.ResumeLayout(false);
            this.tableLayoutPanelProcessing.ResumeLayout(false);
            this.tableLayoutPanelProcessing.PerformLayout();
            this.toolStripProcessingScript.ResumeLayout(false);
            this.toolStripProcessingScript.PerformLayout();
            this.panelProcessingScriptInput.ResumeLayout(false);
            this.panelProcessingScriptInput.PerformLayout();
            this.tabPageEncoding.ResumeLayout(false);
            this.tableLayoutPanelEncoding.ResumeLayout(false);
            this.groupBoxEncodingGeneral.ResumeLayout(false);
            this.tableLayoutPanelEncodingGeneral.ResumeLayout(false);
            this.tableLayoutPanelEncodingGeneral.PerformLayout();
            this.tableLayoutPanelGeneralSizeLimit.ResumeLayout(false);
            this.tableLayoutPanelGeneralSizeLimit.PerformLayout();
            this.groupBoxEncodingVideo.ResumeLayout(false);
            this.tableLayoutPanelEncodingVideo.ResumeLayout(false);
            this.tableLayoutPanelEncodingVideo.PerformLayout();
            this.tableLayoutPanelVideoBitrate.ResumeLayout(false);
            this.tableLayoutPanelVideoBitrate.PerformLayout();
            this.groupBoxEncodingAudio.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanelAudioBitrate.ResumeLayout(false);
            this.tableLayoutPanelAudioBitrate.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxAdvancedEncoding.ResumeLayout(false);
            this.tableLayoutPanelEncodingAdvanced.ResumeLayout(false);
            this.tableLayoutPanelEncodingAdvanced.PerformLayout();
            this.tableLayoutPanelEncodingSlices.ResumeLayout(false);
            this.tableLayoutPanelEncodingSlices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackSlices)).EndInit();
            this.tableLayoutPanelEncodingThreads.ResumeLayout(false);
            this.tableLayoutPanelEncodingThreads.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackThreads)).EndInit();
            this.groupBoxAdvancedProcessing.ResumeLayout(false);
            this.tableLayoutPanelAdvancedProcessing.ResumeLayout(false);
            this.tableLayoutPanelAdvancedProcessing.PerformLayout();
            this.panelHideTheOptions.ResumeLayout(false);
            this.panelHideTheOptions.PerformLayout();
            this.panelContainTheProgressBar.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCrf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMainForm;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGroupMain;
        private System.Windows.Forms.Button buttonBrowseOut;
        private System.Windows.Forms.Label labelOutputFile;
        private System.Windows.Forms.Label labelInputFile;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonBrowseIn;
        public System.Windows.Forms.TextBox textBoxIn;
        public System.Windows.Forms.TextBox textBoxOut;
        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPageProcessing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProcessing;
        private System.Windows.Forms.ToolStrip toolStripProcessingScript;
        private System.Windows.Forms.ToolStripButton toolStripButtonCrop;
        private System.Windows.Forms.ToolStripButton toolStripButtonResize;
        private System.Windows.Forms.ToolStripButton toolStripButtonReverse;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdvancedScripting;
        private System.Windows.Forms.ToolStripButton toolStripButtonSubtitle;
        private System.Windows.Forms.ToolStripButton buttonPreview;
        private System.Windows.Forms.Panel panelProcessingScriptInput;
        private System.Windows.Forms.ListView listViewProcessingScript;
        private System.Windows.Forms.TextBox textBoxProcessingScript;
        private System.Windows.Forms.TabPage tabPageEncoding;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncoding;
        private System.Windows.Forms.GroupBox groupBoxEncodingGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingGeneral;
        private System.Windows.Forms.Label labelGeneralTitle;
        private System.Windows.Forms.TextBox boxMetadataTitle;
        private System.Windows.Forms.Label labelMetadataTitleHint;
        private System.Windows.Forms.GroupBox groupBoxEncodingVideo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingVideo;
        private System.Windows.Forms.Label labelVideoBitrate;
        private System.Windows.Forms.Label labelVideoBitrateHint;
        private System.Windows.Forms.CheckBox boxHQ;
        private System.Windows.Forms.Panel panelHideTheOptions;
        private System.Windows.Forms.Panel panelContainTheProgressBar;
        private System.Windows.Forms.ProgressBar progressBarIndexing;
        private System.Windows.Forms.Label labelIndexingProgress;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxAdvancedEncoding;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingAdvanced;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingSlices;
        private System.Windows.Forms.TrackBar trackSlices;
        private System.Windows.Forms.Label labelSlices;
        private System.Windows.Forms.Label labelEncodingThreads;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEncodingThreads;
        private System.Windows.Forms.TrackBar trackThreads;
        private System.Windows.Forms.Label labelThreads;
        private System.Windows.Forms.Label labelEncodingThreadsHint;
        private System.Windows.Forms.Label labelEncodingArguments;
        private System.Windows.Forms.TextBox textBoxArguments;
        private System.Windows.Forms.Label labelEncodingSlices;
        private System.Windows.Forms.Label labelAdvancedWarning;
        private System.Windows.Forms.GroupBox groupBoxAdvancedProcessing;
        private System.Windows.Forms.Label labelEncodingSlicesHint;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAdvancedProcessing;
        private System.Windows.Forms.Label labelProcessingLevelsHint;
        private System.Windows.Forms.Label labelProcessingDeinterlaceHint;
        private System.Windows.Forms.CheckBox boxDeinterlace;
        private System.Windows.Forms.CheckBox boxLevels;
        private System.Windows.Forms.ToolStripSplitButton toolStripButtonTrim;
        private System.Windows.Forms.ToolStripMenuItem toolStripButtonMultipleTrim;
        private System.Windows.Forms.ToolStripButton toolStripButtonCaption;
        private System.Windows.Forms.ToolStripButton toolStripButtonOverlay;
        private System.Windows.Forms.Label labelGeneralSizeLimitHint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneralSizeLimit;
        private System.Windows.Forms.TextBox boxLimit;
        private System.Windows.Forms.Label labelGeneralSizeLimitUnit;
        private System.Windows.Forms.Label labelGeneralSizeLimit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelVideoBitrate;
        private System.Windows.Forms.TextBox boxVideoBitrate;
        private System.Windows.Forms.Label labelVideoBitrateUnit;
        private System.Windows.Forms.Label labelVideoHQHint;
        private System.Windows.Forms.GroupBox groupBoxEncodingAudio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelGeneralAudioHint;
        private System.Windows.Forms.CheckBox boxAudio;
        private System.Windows.Forms.Label labelAudioBitrate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAudioBitrate;
        private System.Windows.Forms.TextBox boxAudioBitrate;
        private System.Windows.Forms.Label labelAudioBitrateUnit;
        private System.Windows.Forms.Label labelAudioBitrateHint;
        private System.Windows.Forms.Label labelProcessingDenoiseHint;
        private System.Windows.Forms.CheckBox boxDenoise;
        private System.Windows.Forms.Label labelVideoCrfHint;
        private System.Windows.Forms.NumericUpDown numericCrf;
        private System.Windows.Forms.Label labelVideoCrf;
    }
}

