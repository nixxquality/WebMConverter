using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace WebMConverter
{
    class FFprobe : Process
    {
        public string FFmpegPath = Path.Combine(Environment.CurrentDirectory, "Binaries", "Win32", "ffprobe.exe");
        const string templateArguments = "{0} \"{1}\" -of xml{2}";
        // {0} is the format of the input file
        // {1} is the input file
        // {2} is automatically constructed list of things to output OR argument

        public FFprobe(string inputFile, string format = "-f avisynth", List<string> dataToProbe = null, string argument = null)
        {
            if (argument == null) // No override arguments, time to construct this bad boy
            {
                if (dataToProbe == null)
                {
                    dataToProbe = new List<string>();
                    dataToProbe.Add("streams");
                }

                string dataToProbeAsString = "";
                foreach(string Type in dataToProbe)
                {
                    dataToProbeAsString += " -show_";
                    dataToProbeAsString += Type;
                }
                this.StartInfo.Arguments = string.Format(templateArguments, format, inputFile, dataToProbeAsString);
            }
            else
            {
                this.StartInfo.Arguments = string.Format(templateArguments, format, inputFile, " " + argument);
            }

            this.StartInfo.FileName = FFmpegPath;
            this.StartInfo.RedirectStandardInput = true;
            this.StartInfo.RedirectStandardOutput = true;
            this.StartInfo.RedirectStandardError = true;
            this.StartInfo.UseShellExecute = false; //Required to redirect IO streams
            this.StartInfo.CreateNoWindow = true; //Hide console
            this.EnableRaisingEvents = true;
        }

        new public void Start()
        {
            base.Start();
            this.BeginErrorReadLine();
            this.BeginOutputReadLine();
        }

        public string Probe()
        {
            var output = new StringBuilder();
            OutputDataReceived += (sender, args) => output.AppendLine(args.Data);

            Start();
            WaitForExit();

            return output.ToString();
        }
    }
}
