namespace WebMConverter
{
    partial class TrimForm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowTrimButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonTrimStart = new System.Windows.Forms.Button();
            this.textBoxTrimStart = new System.Windows.Forms.TextBox();
            this.buttonTrimEnd = new System.Windows.Forms.Button();
            this.textBoxTrimEnd = new System.Windows.Forms.TextBox();
            this.previewFrame = new WebMConverter.PreviewFrame();
            this.trackVideoTimeline = new System.Windows.Forms.TrackBar();
            this.flowDialogButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuGoTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuGoToFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuGoToTime = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTimeStamp = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.flowTrimButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVideoTimeline)).BeginInit();
            this.flowDialogButtons.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel.Controls.Add(this.flowTrimButtons, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.previewFrame, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.trackVideoTimeline, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.flowDialogButtons, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelTimeStamp, 2, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(744, 520);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // flowTrimButtons
            // 
            this.flowTrimButtons.Controls.Add(this.buttonTrimStart);
            this.flowTrimButtons.Controls.Add(this.textBoxTrimStart);
            this.flowTrimButtons.Controls.Add(this.buttonTrimEnd);
            this.flowTrimButtons.Controls.Add(this.textBoxTrimEnd);
            this.flowTrimButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTrimButtons.Location = new System.Drawing.Point(0, 490);
            this.flowTrimButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowTrimButtons.Name = "flowTrimButtons";
            this.flowTrimButtons.Size = new System.Drawing.Size(564, 30);
            this.flowTrimButtons.TabIndex = 4;
            // 
            // buttonTrimStart
            // 
            this.buttonTrimStart.Location = new System.Drawing.Point(3, 3);
            this.buttonTrimStart.Name = "buttonTrimStart";
            this.buttonTrimStart.Size = new System.Drawing.Size(54, 23);
            this.buttonTrimStart.TabIndex = 0;
            this.buttonTrimStart.Text = "Start";
            this.buttonTrimStart.UseVisualStyleBackColor = true;
            this.buttonTrimStart.Click += new System.EventHandler(this.buttonTrimStart_Click);
            // 
            // textBoxTrimStart
            // 
            this.textBoxTrimStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTrimStart.Enabled = false;
            this.textBoxTrimStart.Location = new System.Drawing.Point(63, 4);
            this.textBoxTrimStart.Name = "textBoxTrimStart";
            this.textBoxTrimStart.Size = new System.Drawing.Size(59, 20);
            this.textBoxTrimStart.TabIndex = 2;
            // 
            // buttonTrimEnd
            // 
            this.buttonTrimEnd.Location = new System.Drawing.Point(128, 3);
            this.buttonTrimEnd.Name = "buttonTrimEnd";
            this.buttonTrimEnd.Size = new System.Drawing.Size(54, 23);
            this.buttonTrimEnd.TabIndex = 1;
            this.buttonTrimEnd.Text = "End";
            this.buttonTrimEnd.UseVisualStyleBackColor = true;
            this.buttonTrimEnd.Click += new System.EventHandler(this.buttonTrimEnd_Click);
            // 
            // textBoxTrimEnd
            // 
            this.textBoxTrimEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTrimEnd.Enabled = false;
            this.textBoxTrimEnd.Location = new System.Drawing.Point(188, 4);
            this.textBoxTrimEnd.Name = "textBoxTrimEnd";
            this.textBoxTrimEnd.Size = new System.Drawing.Size(59, 20);
            this.textBoxTrimEnd.TabIndex = 3;
            // 
            // previewFrame
            // 
            this.previewFrame.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel.SetColumnSpan(this.previewFrame, 3);
            this.previewFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewFrame.Frame = 0;
            this.previewFrame.Location = new System.Drawing.Point(0, 0);
            this.previewFrame.Margin = new System.Windows.Forms.Padding(0);
            this.previewFrame.Name = "previewFrame";
            this.previewFrame.Size = new System.Drawing.Size(744, 458);
            this.previewFrame.TabIndex = 0;
            // 
            // trackVideoTimeline
            // 
            this.tableLayoutPanel.SetColumnSpan(this.trackVideoTimeline, 2);
            this.trackVideoTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackVideoTimeline.Location = new System.Drawing.Point(3, 461);
            this.trackVideoTimeline.Name = "trackVideoTimeline";
            this.trackVideoTimeline.Size = new System.Drawing.Size(658, 26);
            this.trackVideoTimeline.TabIndex = 1;
            this.trackVideoTimeline.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackVideoTimeline.Scroll += new System.EventHandler(this.trackBarVideoTimeline_Scroll);
            this.trackVideoTimeline.Leave += new System.EventHandler(this.trackVideoTimeline_Leave);
            // 
            // flowDialogButtons
            // 
            this.tableLayoutPanel.SetColumnSpan(this.flowDialogButtons, 2);
            this.flowDialogButtons.Controls.Add(this.buttonConfirm);
            this.flowDialogButtons.Controls.Add(this.buttonCancel);
            this.flowDialogButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDialogButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowDialogButtons.Location = new System.Drawing.Point(564, 490);
            this.flowDialogButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowDialogButtons.Name = "flowDialogButtons";
            this.flowDialogButtons.Size = new System.Drawing.Size(180, 30);
            this.flowDialogButtons.TabIndex = 5;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConfirm.Enabled = false;
            this.buttonConfirm.Location = new System.Drawing.Point(102, 3);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 0;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(21, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuGoTo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(744, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuGoTo
            // 
            this.toolStripMenuGoTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuGoToFrame,
            this.ToolStripMenuGoToTime});
            this.toolStripMenuGoTo.Name = "toolStripMenuGoTo";
            this.toolStripMenuGoTo.Size = new System.Drawing.Size(57, 20);
            this.toolStripMenuGoTo.Text = "Go to...";
            // 
            // toolStripMenuGoToFrame
            // 
            this.toolStripMenuGoToFrame.Name = "toolStripMenuGoToFrame";
            this.toolStripMenuGoToFrame.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuGoToFrame.Text = "Frame";
            this.toolStripMenuGoToFrame.Click += new System.EventHandler(this.toolStripMenuGoToFrame_Click);
            // 
            // ToolStripMenuGoToTime
            // 
            this.ToolStripMenuGoToTime.Name = "ToolStripMenuGoToTime";
            this.ToolStripMenuGoToTime.Size = new System.Drawing.Size(107, 22);
            this.ToolStripMenuGoToTime.Text = "Time";
            this.ToolStripMenuGoToTime.Click += new System.EventHandler(this.ToolStripMenuGoToTime_Click);
            // 
            // labelTimeStamp
            // 
            this.labelTimeStamp.AutoSize = true;
            this.labelTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimeStamp.Location = new System.Drawing.Point(667, 458);
            this.labelTimeStamp.Name = "labelTimeStamp";
            this.labelTimeStamp.Size = new System.Drawing.Size(74, 32);
            this.labelTimeStamp.TabIndex = 6;
            this.labelTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TrimForm
            // 
            this.AcceptButton = this.buttonConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(744, 544);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(446, 299);
            this.Name = "TrimForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trim";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowTrimButtons.ResumeLayout(false);
            this.flowTrimButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackVideoTimeline)).EndInit();
            this.flowDialogButtons.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private PreviewFrame previewFrame;
        private System.Windows.Forms.TrackBar trackVideoTimeline;
        private System.Windows.Forms.FlowLayoutPanel flowTrimButtons;
        private System.Windows.Forms.Button buttonTrimStart;
        private System.Windows.Forms.Button buttonTrimEnd;
        private System.Windows.Forms.FlowLayoutPanel flowDialogButtons;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxTrimStart;
        private System.Windows.Forms.TextBox textBoxTrimEnd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuGoTo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuGoToFrame;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuGoToTime;
        private System.Windows.Forms.Label labelTimeStamp;
    }
}