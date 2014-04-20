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
    public partial class previewFrame : UserControl
    {
        // Internal things for drawing and the likes
        private FFmpeg ffmpegProcess;
        private bool generating = false;
        private string message;

        // The info for our preview operation
        private Image image;
        private uint frame;
        private string inputFile;
        private string previewFile = Path.GetTempFileName();

        public int Frame
        {
            get { return (int)frame; }
            set { frame = (uint)value; GeneratePreview(); }
        }
        public string InputFile
        {
            get { return inputFile; }
            set { inputFile = value; GeneratePreview(); }
        }
        public bool Generating
        {
            get { return generating; }
        }
        public string Message
        {
            get { return message; }
        }

        public previewFrame()
        {
            InitializeComponent();
        }

        private void GeneratePreview()
        {
            string argument = ConstructArguments();

            if (string.IsNullOrWhiteSpace(argument))
                return;

            if (generating)
                return; // Drop the frame

            generating = true;
            ffmpegProcess = new FFmpeg(argument);

            ffmpegProcess.Process.Exited += (o, args) => pictureBoxFrame.Invoke((Action)(() =>
            {
                generating = false;

                int exitCode = ffmpegProcess.Process.ExitCode;

                if (exitCode != 0)
                {
                    message = string.Format("ffmpeg.exe exited with exit code {0}. That's usually bad.", exitCode);
                    return;
                }

                // We can't do a File.Exists check here. Downside to using TempFile.

                try
                {
                    using (FileStream stream = new FileStream(previewFile, FileMode.Open, FileAccess.Read))
                        image = Image.FromStream(stream);

                    pictureBoxFrame.BackgroundImage = image;

                    float aspectRatio = image.Width / image.Height;
                    ClientSize = new Size((int)(ClientSize.Width * aspectRatio), ClientSize.Height);
                }
                catch (Exception e)
                {
                    message = e.ToString();
                }
            }));
            ffmpegProcess.Start();
        }

        private string ConstructArguments()
        {
            string template = "-i \"{0}\" -f image2 -c:v bmp -vf \"select=gte(n\\,{1})\" -vframes 1 -y \"{2}\"";
            // {0} is input file
            // {1} is the frame number
            // {2} is output image

            if (string.IsNullOrWhiteSpace(inputFile))
            {
                message = "No input file.";
                return null;
            }
            if (!File.Exists(inputFile))
            {
                message = "Input file doesn't exist.";
                return null;
            }

            message = "Previewing frame #" + frame;

            return string.Format(template, inputFile, frame, previewFile);
        }
    }
}
