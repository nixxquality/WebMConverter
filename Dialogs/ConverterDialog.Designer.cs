namespace WebMConverter
{
    partial class ConverterDialog
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
            System.Windows.Forms.TableLayoutPanel table;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConverterDialog));
            this.buttonPlay = new System.Windows.Forms.Button();
            this.boxOutput = new System.Windows.Forms.RichTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureStatus = new System.Windows.Forms.PictureBox();
            this.StatusImages = new System.Windows.Forms.ImageList(this.components);
            table = new System.Windows.Forms.TableLayoutPanel();
            table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            table.ColumnCount = 3;
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            table.Controls.Add(this.boxOutput, 0, 0);
            table.Controls.Add(this.pictureStatus, 0, 1);
            table.Controls.Add(this.buttonPlay, 1, 1);
            table.Controls.Add(this.buttonCancel, 2, 1);
            table.Dock = System.Windows.Forms.DockStyle.Fill;
            table.Location = new System.Drawing.Point(3, 3);
            table.Name = "table";
            table.RowCount = 2;
            table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            table.Size = new System.Drawing.Size(678, 435);
            table.TabIndex = 0;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(34, 407);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(317, 25);
            this.buttonPlay.TabIndex = 5;
            this.buttonPlay.Text = "Play result";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // boxOutput
            // 
            this.boxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            table.SetColumnSpan(this.boxOutput, 3);
            this.boxOutput.DetectUrls = false;
            this.boxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxOutput.Location = new System.Drawing.Point(3, 3);
            this.boxOutput.Name = "boxOutput";
            this.boxOutput.ReadOnly = true;
            this.boxOutput.Size = new System.Drawing.Size(672, 398);
            this.boxOutput.TabIndex = 0;
            this.boxOutput.Text = "";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.Location = new System.Drawing.Point(357, 407);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(318, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // pictureStatus
            // 
            this.pictureStatus.BackColor = System.Drawing.SystemColors.Control;
            this.pictureStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureStatus.Location = new System.Drawing.Point(3, 407);
            this.pictureStatus.Name = "pictureStatus";
            this.pictureStatus.Size = new System.Drawing.Size(25, 25);
            this.pictureStatus.TabIndex = 4;
            this.pictureStatus.TabStop = false;
            // 
            // StatusImages
            // 
            this.StatusImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("StatusImages.ImageStream")));
            this.StatusImages.TransparentColor = System.Drawing.Color.Transparent;
            this.StatusImages.Images.SetKeyName(0, "Happening");
            this.StatusImages.Images.SetKeyName(1, "Failure");
            this.StatusImages.Images.SetKeyName(2, "Success");
            // 
            // ConverterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 441);
            this.ControlBox = false;
            this.Controls.Add(table);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ConverterDialog";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowInTaskbar = false;
            this.Text = "Conversion Progress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConverterForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConverterForm_FormClosed);
            this.Load += new System.EventHandler(this.ConverterForm_Load);
            table.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureStatus;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.ImageList StatusImages;
        private System.Windows.Forms.RichTextBox boxOutput;


    }
}