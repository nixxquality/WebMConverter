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

            trackVideoTimeline.Maximum = FFMS2.VideoSource.NumFrames;
            trackVideoTimeline.TickFrequency = trackVideoTimeline.Maximum / 60;
            textBoxSelectedFrame.Text = "" + trackVideoTimeline.Value;
        }

        public TrimForm(int TrimStart, int TrimEnd) : this()
        {
            trimStart = TrimStart;
            trimEnd = TrimEnd;

            textBoxTrimStart.Text = "" + TrimStart;
            textBoxTrimEnd.Text = "" + TrimEnd;

            trackVideoTimeline.Value = TrimStart;
            previewFrame.Frame = TrimStart;
            textBoxSelectedFrame.Text = "" + TrimStart;

            checktrims();
        }

        private void trackBarVideoTimeline_Scroll(object sender, EventArgs e)
        {
            previewFrame.Frame = trackVideoTimeline.Value;
            textBoxSelectedFrame.Text = "" + trackVideoTimeline.Value;
        }

        private void buttonTrimStart_Click(object sender, EventArgs e)
        {
            trimStart = trackVideoTimeline.Value;
            textBoxTrimStart.Text = "" + trackVideoTimeline.Value;
            checktrims();
        }

        private void buttonTrimEnd_Click(object sender, EventArgs e)
        {
            trimEnd = trackVideoTimeline.Value;
            textBoxTrimEnd.Text = "" + trackVideoTimeline.Value;
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

        private void textBoxSelectedFrame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int i = int.Parse(textBoxSelectedFrame.Text);
                    previewFrame.Frame = i;
                    trackVideoTimeline.Value = i;
                    textBoxSelectedFrame.Text = "" + i;
                }
                catch
                { }
            }
            e.Handled = true; // Don't close the TrimForm!
        }
    }
}
