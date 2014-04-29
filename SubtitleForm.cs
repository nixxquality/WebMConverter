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
        }

        public SubtitleForm(SubtitleFilter SubtitleFilter) : this()
        {
            if (SubtitleFilter.FileName != FFMS2.InputFile)
            {
                checkBoxInternalSubs.Checked = false;
                textBoxSubtitleFile.Text = SubtitleFilter.FileName;
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (checkBoxInternalSubs.Checked)
            {
                GeneratedFilter = new SubtitleFilter(FFMS2.InputFile);
            }
            else
            {
                GeneratedFilter = new SubtitleFilter(textBoxSubtitleFile.Text);
            }
        }

        private void checkBoxInternalSubs_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSubtitleFile.Enabled = !checkBoxInternalSubs.Checked;
            buttonSelectSubtitleFile.Enabled = !checkBoxInternalSubs.Checked;
        }

        private void buttonSelectSubtitleFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Path.GetDirectoryName(FFMS2.InputFile);
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
        public readonly string FileName;

        public SubtitleFilter(string FileName)
        {
            this.FileName = FileName;
        }

        public string GetAvisynthCommand()
        {
            return string.Format("assrender(\"{0}\")", FileName);
            // Todo: Something about fonts
        }
    }
}
