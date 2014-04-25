using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class TrimForm : Form
    {
        private int trimStart = -1;
        private int trimEnd = -1;

        public int TrimStart
        { get { return trimStart; } }
        public int TrimEnd
        { get { return trimEnd; } }

        public TrimForm()
        {
            InitializeComponent();

            trackBar1.Maximum = FFMS2.VideoSource.NumFrames;
            trackBar1.TickFrequency = trackBar1.Maximum / 60;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            previewFrame1.Frame = trackBar1.Value;
        }

        private void buttonTrimStart_Click(object sender, EventArgs e)
        {
            trimStart = trackBar1.Value;
            checktrims();
        }

        private void buttonTrimEnd_Click(object sender, EventArgs e)
        {
            trimEnd = trackBar1.Value;
            checktrims();
        }

        private void checktrims()
        {
            if (trimStart == -1)
                return;
            if (trimEnd == -1)
                return;
            if (trimEnd < trimStart)
            {
                buttonConfirm.Enabled = false;
                return;
            }
            buttonConfirm.Enabled = true;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
