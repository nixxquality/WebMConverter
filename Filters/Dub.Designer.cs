namespace WebMConverter
{
    partial class DubForm
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
            System.Windows.Forms.Label labelAudioFile;
            System.Windows.Forms.TableLayoutPanel tableLayout;
            System.Windows.Forms.Label labelTrimAudioHint;
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.boxAudioFile = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.panelIndexingProgress = new System.Windows.Forms.Panel();
            this.progressIndexingProgress = new System.Windows.Forms.ProgressBar();
            this.labelIndexingProgress = new System.Windows.Forms.Label();
            this.openAudioFile = new System.Windows.Forms.OpenFileDialog();
            this.boxTrimAudio = new System.Windows.Forms.CheckBox();
            labelAudioFile = new System.Windows.Forms.Label();
            tableLayout = new System.Windows.Forms.TableLayoutPanel();
            labelTrimAudioHint = new System.Windows.Forms.Label();
            tableLayout.SuspendLayout();
            this.panelIndexingProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAudioFile
            // 
            labelAudioFile.AutoSize = true;
            labelAudioFile.Dock = System.Windows.Forms.DockStyle.Fill;
            labelAudioFile.Location = new System.Drawing.Point(3, 0);
            labelAudioFile.Name = "labelAudioFile";
            labelAudioFile.Size = new System.Drawing.Size(94, 28);
            labelAudioFile.TabIndex = 0;
            labelAudioFile.Text = "Audio file:";
            labelAudioFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 4;
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayout.Controls.Add(labelTrimAudioHint, 1, 1);
            tableLayout.Controls.Add(this.buttonConfirm, 3, 2);
            tableLayout.Controls.Add(this.buttonCancel, 2, 2);
            tableLayout.Controls.Add(labelAudioFile, 0, 0);
            tableLayout.Controls.Add(this.boxAudioFile, 1, 0);
            tableLayout.Controls.Add(this.buttonBrowse, 3, 0);
            tableLayout.Controls.Add(this.boxTrimAudio, 0, 1);
            tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayout.Location = new System.Drawing.Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableLayout.Size = new System.Drawing.Size(401, 85);
            tableLayout.TabIndex = 1;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConfirm.Location = new System.Drawing.Point(328, 59);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(70, 23);
            this.buttonConfirm.TabIndex = 3;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(253, 59);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(69, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // boxAudioFile
            // 
            this.boxAudioFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.boxAudioFile.BackColor = System.Drawing.SystemColors.ControlLightLight;
            tableLayout.SetColumnSpan(this.boxAudioFile, 2);
            this.boxAudioFile.Location = new System.Drawing.Point(103, 4);
            this.boxAudioFile.Name = "boxAudioFile";
            this.boxAudioFile.Size = new System.Drawing.Size(219, 20);
            this.boxAudioFile.TabIndex = 0;
            this.boxAudioFile.TabStop = false;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBrowse.Location = new System.Drawing.Point(328, 3);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(70, 22);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // panelIndexingProgress
            // 
            this.panelIndexingProgress.Controls.Add(this.progressIndexingProgress);
            this.panelIndexingProgress.Controls.Add(this.labelIndexingProgress);
            this.panelIndexingProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIndexingProgress.Location = new System.Drawing.Point(0, 0);
            this.panelIndexingProgress.Name = "panelIndexingProgress";
            this.panelIndexingProgress.Size = new System.Drawing.Size(401, 85);
            this.panelIndexingProgress.TabIndex = 0;
            // 
            // progressIndexingProgress
            // 
            this.progressIndexingProgress.Location = new System.Drawing.Point(3, 3);
            this.progressIndexingProgress.Name = "progressIndexingProgress";
            this.progressIndexingProgress.Size = new System.Drawing.Size(395, 23);
            this.progressIndexingProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressIndexingProgress.TabIndex = 0;
            this.progressIndexingProgress.Value = 30;
            // 
            // labelIndexingProgress
            // 
            this.labelIndexingProgress.Location = new System.Drawing.Point(3, 28);
            this.labelIndexingProgress.Name = "labelIndexingProgress";
            this.labelIndexingProgress.Size = new System.Drawing.Size(395, 25);
            this.labelIndexingProgress.TabIndex = 1;
            this.labelIndexingProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openAudioFile
            // 
            this.openAudioFile.Title = "Select Audio file";
            this.openAudioFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openAudioFile_FileOk);
            // 
            // boxTrimAudio
            // 
            this.boxTrimAudio.AutoSize = true;
            this.boxTrimAudio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxTrimAudio.Checked = true;
            this.boxTrimAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boxTrimAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxTrimAudio.Location = new System.Drawing.Point(3, 31);
            this.boxTrimAudio.Name = "boxTrimAudio";
            this.boxTrimAudio.Size = new System.Drawing.Size(94, 22);
            this.boxTrimAudio.TabIndex = 4;
            this.boxTrimAudio.Text = "Trim audio:";
            this.boxTrimAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boxTrimAudio.UseVisualStyleBackColor = true;
            // 
            // labelTrimAudioHint
            // 
            labelTrimAudioHint.AutoSize = true;
            tableLayout.SetColumnSpan(labelTrimAudioHint, 3);
            labelTrimAudioHint.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTrimAudioHint.Location = new System.Drawing.Point(103, 28);
            labelTrimAudioHint.Name = "labelTrimAudioHint";
            labelTrimAudioHint.Size = new System.Drawing.Size(295, 28);
            labelTrimAudioHint.TabIndex = 5;
            labelTrimAudioHint.Text = "Uncheck this if you want the audio to continue past your video boundaries, or if " +
    "you\'re dubbing an image file.";
            labelTrimAudioHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DubForm
            // 
            this.AcceptButton = this.buttonConfirm;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(401, 85);
            this.ControlBox = false;
            this.Controls.Add(tableLayout);
            this.Controls.Add(this.panelIndexingProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DubForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dub";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DubForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DubForm_DragEnter);
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            this.panelIndexingProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIndexingProgress;
        private System.Windows.Forms.Label labelIndexingProgress;
        private System.Windows.Forms.ProgressBar progressIndexingProgress;
        private System.Windows.Forms.TextBox boxAudioFile;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openAudioFile;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox boxTrimAudio;
    }
}