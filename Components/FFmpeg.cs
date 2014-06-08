using System;
using System.Diagnostics;
using System.IO;

namespace WebMConverter
{
    class FFmpeg : Process //Refactoring, faggots.
    {
        public string FFmpegPath;

        public FFmpeg(string argument, bool win32 = false)
        {
            string folder;
            if (win32)
                folder = "Win32";
            else
                if (Environment.Is64BitOperatingSystem)
                    folder = "Win64";
                else
                    folder = "Win32";

            FFmpegPath = Path.Combine(Environment.CurrentDirectory, "Binaries", folder, "ffmpeg.exe");

            this.StartInfo.FileName = FFmpegPath;
            this.StartInfo.Arguments = argument;
            this.StartInfo.RedirectStandardInput = true;
            this.StartInfo.RedirectStandardOutput = true;
            this.StartInfo.RedirectStandardError = true;
            this.StartInfo.UseShellExecute = false; //Required to redirect IO streams
            this.StartInfo.CreateNoWindow = true; //Hide console
            this.EnableRaisingEvents = true; 
        }

        new public void Start()
        {
            Start(true);
        }

        public void Start(bool OutputReadLine)
        {
            base.Start();
            this.BeginErrorReadLine();
            if (OutputReadLine)
                this.BeginOutputReadLine();
        }
    }
}
