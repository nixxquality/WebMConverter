using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class SubtitleForm : Form
    {
        public SubtitleFilter GeneratedFilter;

        public SubtitleForm()
        {
            InitializeComponent();

            Dictionary<int, string> subtitleTracks = new Dictionary<int, string>();
            foreach (int Track in Program.SubtitleTracks)
            {
                subtitleTracks.Add(Track, string.Format("Track #{0}", Track)); // Todo: Add more meaningful information
            }
            comboBoxVideoTracks.DataSource = new BindingSource(subtitleTracks, null);
            comboBoxVideoTracks.ValueMember = "Key";
            comboBoxVideoTracks.DisplayMember = "Value";
        }

        public SubtitleForm(SubtitleFilter SubtitleFilter) : this()
        {
            if (SubtitleFilter.FileName == Program.InputFile)
            {
                comboBoxVideoTracks.SelectedValue = SubtitleFilter.Track;
            }
            else
            {
                checkBoxInternalSubs.Checked = false;
                textBoxSubtitleFile.Text = SubtitleFilter.FileName;
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (checkBoxInternalSubs.Checked)
            {
                GeneratedFilter = new SubtitleFilter(Program.InputFile, (int)comboBoxVideoTracks.SelectedValue);
            }
            else
            {
                GeneratedFilter = new SubtitleFilter(textBoxSubtitleFile.Text, 0);
            }
        }

        private void checkBoxInternalSubs_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanelSubtitleFileSelector.Visible = !checkBoxInternalSubs.Checked;
            comboBoxVideoTracks.Visible = checkBoxInternalSubs.Checked;
            label2.Text = checkBoxInternalSubs.Checked ? "Subtitle track:" : "Subtitle file:";
        }

        private void buttonSelectSubtitleFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Path.GetDirectoryName(Program.InputFile);
                dialog.Filter = "Subtitle files (*.ass, *.srt, *.ssa)|*.ass;*.srt;*.ssa|All files|*.*";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxSubtitleFile.Text = dialog.FileName;
                }
            }
        }
    }

    public class SubtitleFilter : IFilter
    {
        private string actualFileName;
        public readonly string FileName;
        public readonly int Track;

        public SubtitleFilter(string FileName, int Track)
        {
            this.FileName = FileName;
            this.Track = Track;
        }

        public void BeforeEncode()
        {
            if (FileName == Program.InputFile)
            {
                actualFileName = Path.Combine(Program.AttachmentDirectory, "sub.ass"); // stub
                var ffmpeg = new FFmpeg(string.Format("-i \"{0}\" -map 0:{1} {2} -y", Program.InputFile, Track, actualFileName));
                ffmpeg.Start();
                ffmpeg.WaitForExit();
            }
            else
                actualFileName = FileName;
        }

        public string GetAvisynthCommand()
        {
            return string.Format("assrender(\"{0}\", fontdir=\"{1}\")", actualFileName, Program.AttachmentDirectory);
        }
    }
}
