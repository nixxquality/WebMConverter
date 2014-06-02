namespace WebMConverter
{
    partial class TextInputDialog
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
            this.boxText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // boxText
            // 
            this.boxText.AcceptsReturn = true;
            this.boxText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxText.Location = new System.Drawing.Point(0, 0);
            this.boxText.Multiline = true;
            this.boxText.Name = "boxText";
            this.boxText.Size = new System.Drawing.Size(218, 167);
            this.boxText.TabIndex = 0;
            // 
            // TextInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 167);
            this.Controls.Add(this.boxText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextInputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox boxText;

    }
}