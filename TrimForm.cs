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

        public TrimFilter GeneratedFilter;

        public TrimForm()
        {
            InitializeComponent();

            trackVideoTimeline.Maximum = Program.VideoSource.NumFrames - 1;
            trackVideoTimeline.TickFrequency = trackVideoTimeline.Maximum / 60;
            textBoxSelectedFrame.Text = "" + trackVideoTimeline.Value;
        }

        public TrimForm(TrimFilter FilterToEdit) : this()
        {
            trimStart = FilterToEdit.TrimStart;
            trimEnd = FilterToEdit.TrimEnd;

            textBoxTrimStart.Text = "" + trimStart;
            textBoxTrimEnd.Text = "" + trimEnd;

            trackVideoTimeline.Value = trimStart;
            previewFrame.Frame = trimStart;
            textBoxSelectedFrame.Text = "" + trimStart;

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
            GeneratedFilter = new TrimFilter(trimStart, trimEnd);

            Close();
        }

        private void textBoxSelectedFrame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int i = int.Parse(textBoxSelectedFrame.Text);
                    i = Math.Min(trackVideoTimeline.Maximum - 1, i); // Make sure we don't go out of bounds.
                    previewFrame.Frame = i;
                    trackVideoTimeline.Value = i;
                    textBoxSelectedFrame.Text = "" + i;
                }
                catch
                { }
            }
        }
    }

    public class TrimFilter : IFilter
    {
        public readonly int TrimStart;
        public readonly int TrimEnd;

        public TrimFilter(int TrimStart, int TrimEnd)
        {
            this.TrimStart = TrimStart;
            this.TrimEnd = TrimEnd;
        }

        public string GetAvisynthCommand()
        {
            return string.Format("Trim({0}, {1})", TrimStart, TrimEnd);
        }
    }
}
