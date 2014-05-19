using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
            FFMSSharp.Frame frame = Program.VideoSource.GetFrame((Filters.Trim == null) ? 0 : Filters.Trim.TrimStart); // the video may have different frame resolutions

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

            labelWidthIn.Text = inwidth.ToString();
            textWidthOut.Text = inwidth.ToString();
            labelHeightIn.Text = inheight.ToString();
            textHeightOut.Text = inheight.ToString();
        }

        public ResizeForm(ResizeFilter ResizeFilter) : this()
        {
            targetwidth = ResizeFilter.TargetWidth;
            targetheight = ResizeFilter.TargetHeight;

            textWidthOut.Text = targetwidth.ToString();
            textHeightOut.Text = targetheight.ToString();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            GeneratedFilter = new ResizeFilter(targetwidth, targetheight);
        }

        private void textWidthOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textWidthOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(textWidthOut.Text, CultureInfo.InvariantCulture);

                targetwidth = i;

                if (checkBoxProportions.Checked)
                {
                    textWidthOut.TextChanged -= textWidthOut_TextChanged;

                    targetheight = (int)((float)inheight / (float)inwidth * (float)i);
                    textHeightOut.Text = targetheight.ToString();

                    textWidthOut.TextChanged += textWidthOut_TextChanged;
                }

                try
                {
                    int.Parse(textHeightOut.Text, CultureInfo.InvariantCulture); // if the other one's a proper number too
                    buttonConfirm.Enabled = true;
                }
                catch { }
            }
            catch
            {
                buttonConfirm.Enabled = false;
            }
        }

        private void textHeightOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textHeightOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int i = int.Parse(textHeightOut.Text, CultureInfo.InvariantCulture);

                targetheight = i;

                if (checkBoxProportions.Checked)
                {
                    textHeightOut.TextChanged -= textHeightOut_TextChanged;

                    targetwidth = (int)((float)inwidth / (float)inheight * (float)i);
                    textWidthOut.Text = targetwidth.ToString();

                    textHeightOut.TextChanged -= textHeightOut_TextChanged;
                }

                try
                {
                    int.Parse(textWidthOut.Text, CultureInfo.InvariantCulture); // if the other one's a proper number too
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

    public class ResizeFilter
    {
        public readonly int TargetWidth;
        public readonly int TargetHeight;

        public ResizeFilter(int TargetWidth, int TargetHeight)
        {
            this.TargetWidth = TargetWidth;
            this.TargetHeight = TargetHeight;
        }

        public override string ToString()
        {
            return string.Format("LanczosResize({0}, {1})", (TargetWidth / 2) * 2, (TargetHeight / 2) * 2);
        }
    }
}
