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
    public partial class ResizeForm : Form
    {
        private int targetwidth;
        private int targetheight;

        private int inwidth;
        private int inheight;

        public ResizeFilter GeneratedFilter;

        public ResizeForm()
        {
            FFMSsharp.Frame frame = FFMS2.VideoSource.GetFrame((Filters.Trim == null) ? 0 : Filters.Trim.TrimStart); // the video may have different frame resolutions

            if (Filters.Crop != null)
            {
                inwidth = frame.EncodedResolution.Width - Filters.Crop.Left + Filters.Crop.Right;
                inheight = frame.EncodedResolution.Height - Filters.Crop.Top + Filters.Crop.Bottom;
            }
            else
            {
                inwidth = frame.EncodedResolution.Width;
                inheight = frame.EncodedResolution.Height;
            }

            InitializeComponent();

            labelWidthIn.Text = "" + inwidth;
            textWidthOut.Text = "" + inwidth;
            labelHeightIn.Text = "" + inheight;
            textHeightOut.Text = "" + inheight;
        }

        public ResizeForm(ResizeFilter ResizeFilter) : this()
        {
            targetwidth = ResizeFilter.TargetWidth;
            targetheight = ResizeFilter.TargetHeight;

            textWidthOut.Text = "" + targetwidth;
            textHeightOut.Text = "" + targetheight;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            GeneratedFilter = new ResizeFilter(targetwidth, targetheight);
        }

        private void textWidthOut_KeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                int i = int.Parse(textWidthOut.Text);

                targetwidth = i;

                if (checkBoxProportions.Checked)
                {
                    targetheight = (int)((float)inheight / (float)inwidth * (float)i);
                    textHeightOut.Text = "" + targetheight;
                }

                try
                {
                    int.Parse(textHeightOut.Text); // if the other one's a proper number too
                    buttonConfirm.Enabled = true;
                }
                catch { }
            }
            catch
            {
                buttonConfirm.Enabled = false;
            }
        }

        private void textHeightOut_KeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                int i = int.Parse(textHeightOut.Text);

                targetheight = i;

                if (checkBoxProportions.Checked)
                {
                    targetwidth = (int)((float)inwidth / (float)inheight / (float)i);
                    textWidthOut.Text = "" + targetwidth;
                }

                try
                {
                    int.Parse(textWidthOut.Text); // if the other one's a proper number too
                    buttonConfirm.Enabled = true;
                }
                catch { }
            }
            catch
            {
                buttonConfirm.Enabled = false;
            }
        }
    }

    public class ResizeFilter : IFilter
    {
        public readonly int TargetWidth;
        public readonly int TargetHeight;

        public ResizeFilter(int TargetWidth, int TargetHeight)
        {
            this.TargetWidth = TargetWidth;
            this.TargetHeight = TargetHeight;
        }

        public string GetAvisynthCommand()
        {
            return string.Format("LanczosResize({0}, {1})", TargetWidth, TargetHeight);
        }
    }
}
