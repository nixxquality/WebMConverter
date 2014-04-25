using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace WebMConverter
{
    public partial class PreviewFrame : UserControl
    {
        private uint frame;

        public int Frame
        {
            get { return (int)frame; }
            set { frame = (uint)value; GeneratePreview(); }
        }
        public PictureBox Picture
        { get { return pictureBoxFrame; } }

        public PreviewFrame()
        {
            InitializeComponent();
        }

        public void GeneratePreview()
        {
            if (FFMS2.VideoSource == null)
                return;

            // Prepare our "list" of accepted pixel formats
            List<int> pixelformat = new List<int>();
            pixelformat.Add(FFMSsharp.FFMS2.GetPixFmt("bgra"));

            // Calculate width and height
            int w, h;
            float s;
            FFMSsharp.Frame frame;
            frame = FFMS2.VideoSource.GetFrame((int)this.frame);
            s = Math.Min((float)this.Size.Width / (float)frame.EncodedResolution.Width, (float)this.Size.Height / (float)frame.EncodedResolution.Height);
            w = (int)(frame.EncodedResolution.Width * s);
            h = (int)(frame.EncodedResolution.Height * s);

            // Do all the work
            FFMS2.VideoSource.SetOutputFormat(pixelformat, w, h, FFMSsharp.Resizers.Bilinear);
            frame = FFMS2.VideoSource.GetFrame((int)this.frame);

            pictureBoxFrame.BackgroundImage = frame.GetBitmap();
            pictureBoxFrame.ClientSize = new Size(w, h);

            // Center the pictureBox in our control
            if (w == Width || w - 1 == Width || w + 1 == Width) // this looks weird but keep in mind we're dealing with an ex float here
            {
                Padding = new Padding(0, (Height - h) / 2, 0, 0);
            }
            else
            {
                Padding = new Padding((Width - w) / 2, 0, 0, 0);
            }
        }

        private void pictureBoxFrame_SizeChanged(object sender, EventArgs e)
        {
            GeneratePreview();
        }
    }
}
