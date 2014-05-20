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

        public TrimForm(TrimFilter FilterToEdit = null)
        {
            InitializeComponent();

            trackVideoTimeline.Maximum = Program.VideoSource.NumberOfFrames - 1;
            trackVideoTimeline.TickFrequency = trackVideoTimeline.Maximum / 60;
            labelTimeStamp.Text = Program.FrameToTimeStamp(trackVideoTimeline.Value);

            trackVideoTimeline.Focus();

            if (FilterToEdit == null)
            {
                trimStart = 0;
                trimEnd = Program.VideoSource.NumberOfFrames - 1;
            }
            else
            {
                trimStart = FilterToEdit.TrimStart;
                trimEnd = FilterToEdit.TrimEnd;
            }

            labelTrimStart.Text = string.Format("{0} ({1})", Program.FrameToTimeStamp(trimStart), trimStart.ToString());
            labelTrimEnd.Text = string.Format("{0} ({1})", Program.FrameToTimeStamp(trimEnd), trimEnd.ToString());

            trackVideoTimeline.Value = trimStart;
            previewFrame.Frame = trimStart;
            labelTimeStamp.Text = Program.FrameToTimeStamp(trimStart);

            checktrims();
        }

        private void trackBarVideoTimeline_Scroll(object sender, EventArgs e)
        {
            previewFrame.Frame = trackVideoTimeline.Value;
            labelTimeStamp.Text = Program.FrameToTimeStamp(trackVideoTimeline.Value);
        }

        private void buttonTrimStart_Click(object sender, EventArgs e)
        {
            trimStart = trackVideoTimeline.Value;
            labelTrimStart.Text = string.Format("{0} ({1})", Program.FrameToTimeStamp(trimStart), trimStart.ToString());
            checktrims();
        }

        private void buttonTrimEnd_Click(object sender, EventArgs e)
        {
            trimEnd = trackVideoTimeline.Value;
            labelTrimEnd.Text = string.Format("{0} ({1})", Program.FrameToTimeStamp(trimEnd), trimEnd.ToString());
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

        private void toolStripMenuGoToFrame_Click(object sender, EventArgs e)
        {
            using (var dialog = new GoToDialog("Frame", trackVideoTimeline.Value.ToString()))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int i = 0;
                    int.TryParse(dialog.Result, out i);
                    i = Math.Max(0, Math.Min(trackVideoTimeline.Maximum - 1, i)); // Make sure we don't go out of bounds.
                    previewFrame.Frame = i;
                    trackVideoTimeline.Value = i;
                    labelTimeStamp.Text = Program.FrameToTimeStamp(trackVideoTimeline.Value);
                }
            }
        }

        private void ToolStripMenuGoToTime_Click(object sender, EventArgs e)
        {
            using (var dialog = new GoToDialog("Time", Program.FrameToTimeStamp(trackVideoTimeline.Value)))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    double time;
                    try
                    {
                        time = TimeSpan.Parse(dialog.Result).TotalSeconds;
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("That's not a valid timestamp.");
                        return;
                    }
                    int i = Program.TimeToFrame(time);
                    i = Math.Max(0, Math.Min(trackVideoTimeline.Maximum - 1, i)); // Make sure we don't go out of bounds.
                    previewFrame.Frame = i;
                    trackVideoTimeline.Value = i;
                    labelTimeStamp.Text = Program.FrameToTimeStamp(trackVideoTimeline.Value);
                }
            }
        }

        private void trackVideoTimeline_Leave(object sender, EventArgs e)
        {
            trackVideoTimeline.Focus(); // You're not going anywhere~
        }
    }

    public class TrimFilter
    {
        public readonly int TrimStart;
        public readonly int TrimEnd;

        public TrimFilter(int TrimStart, int TrimEnd)
        {
            this.TrimStart = TrimStart;
            this.TrimEnd = TrimEnd;
        }

        public override string ToString()
        {
            return string.Format("Trim({0}, {1})", TrimStart, TrimEnd);
        }

        internal double GetDuration()
        {
            double firsttime, lasttime;
            var track = Program.VideoSource.Track;

            firsttime = Program.FrameToTime(TrimStart);
            lasttime = Program.FrameToTime(TrimEnd);

            return lasttime - firsttime;
        }
    }
}
