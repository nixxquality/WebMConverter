namespace WebMConverter
{
    partial class SubtitleForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxInternalSubs = new System.Windows.Forms.CheckBox();
            this.textBoxSubtitleFile = new System.Windows.Forms.TextBox();
            this.buttonSelectSubtitleFile = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.69909F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.96353F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.66869F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.36474F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxInternalSubs, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxSubtitleFile, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSelectSubtitleFile, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonConfirm, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonCancel, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(329, 87);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use internal subs:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subtitle file:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxInternalSubs
            // 
            this.checkBoxInternalSubs.AutoSize = true;
            this.checkBoxInternalSubs.Checked = true;
            this.checkBoxInternalSubs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInternalSubs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxInternalSubs.Location = new System.Drawing.Point(104, 3);
            this.checkBoxInternalSubs.Name = "checkBoxInternalSubs";
            this.checkBoxInternalSubs.Size = new System.Drawing.Size(86, 20);
            this.checkBoxInternalSubs.TabIndex = 1;
            this.checkBoxInternalSubs.Text = "Yes";
            this.checkBoxInternalSubs.UseVisualStyleBackColor = true;
            this.checkBoxInternalSubs.CheckedChanged += new System.EventHandler(this.checkBoxInternalSubs_CheckedChanged);
            // 
            // textBoxSubtitleFile
            // 
            this.textBoxSubtitleFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxSubtitleFile, 2);
            this.textBoxSubtitleFile.Enabled = false;
            this.textBoxSubtitleFile.Location = new System.Drawing.Point(104, 29);
            this.textBoxSubtitleFile.Name = "textBoxSubtitleFile";
            this.textBoxSubtitleFile.Size = new System.Drawing.Size(154, 20);
            this.textBoxSubtitleFile.TabIndex = 2;
            // 
            // buttonSelectSubtitleFile
            // 
            this.buttonSelectSubtitleFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSelectSubtitleFile.Enabled = false;
            this.buttonSelectSubtitleFile.Location = new System.Drawing.Point(264, 29);
            this.buttonSelectSubtitleFile.Name = "buttonSelectSubtitleFile";
            this.buttonSelectSubtitleFile.Size = new System.Drawing.Size(62, 20);
            this.buttonSelectSubtitleFile.TabIndex = 3;
            this.buttonSelectSubtitleFile.Text = "Browse...";
            this.buttonSelectSubtitleFile.UseVisualStyleBackColor = true;
            this.buttonSelectSubtitleFile.Click += new System.EventHandler(this.buttonSelectSubtitleFile_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConfirm.Location = new System.Drawing.Point(264, 55);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(62, 29);
            this.buttonConfirm.TabIndex = 5;
            this.buttonConfirm.TabStop = false;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(196, 55);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(62, 29);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // SubtitleForm
            // 
            this.AcceptButton = this.buttonConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(329, 87);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SubtitleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subtitles";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxInternalSubs;
        private System.Windows.Forms.TextBox textBoxSubtitleFile;
        private System.Windows.Forms.Button buttonSelectSubtitleFile;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
    }
}