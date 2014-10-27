using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class PreviewFrame : UserControl
    {
        uint framenumber;
        FFMSSharp.Frame frame;
        int cachedframenumber = -1;

        [DefaultValue(0)]
        public int Frame
        {
            get { return (int)framenumber; }
            set { framenumber = (uint)value; GeneratePreview(); }
        }

        public PreviewFrame()
        {
            InitializeComponent();
        }

        public void GeneratePreview()
        {
            if (Program.VideoSource == null)
                return;

            // Prepare our "list" of accepted pixel formats
            List<int> pixelformat = new List<int>();
            pixelformat.Add(FFMSSharp.FFMS2.GetPixelFormat("bgra"));

            // Load the frame, if we haven't already
            if (cachedframenumber != framenumber)
            {
                cachedframenumber = (int)framenumber;
                frame = Program.VideoSource.GetFrame(cachedframenumber);
            }

            // Calculate width and height
            int w, h;
            float s;
            s = Math.Min((float)this.Size.Width / (float)frame.EncodedResolution.Width, (float)this.Size.Height / (float)frame.EncodedResolution.Height);
            w = (int)(frame.EncodedResolution.Width * s);
            h = (int)(frame.EncodedResolution.Height * s);

            // Do all the work
            Program.VideoSource.SetOutputFormat(pixelformat, w, h, FFMSSharp.Resizer.Bilinear);
            frame = Program.VideoSource.GetFrame((int)this.framenumber);

            Picture.BackgroundImage = frame.Bitmap;
            Picture.ClientSize = new Size(w, h);

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
