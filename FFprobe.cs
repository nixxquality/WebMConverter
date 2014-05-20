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
        public string FFmpegPath = Path.Combine(Environment.CurrentDirectory, "Binaries", "ffprobe.exe");
        const string templateArguments = "-f {0} \"{1}\" -of xml{2}";
        // {0} is the format of the input file
        // {1} is the input file
        // {2} is automatically constructed list of things to output

        public FFprobe(string avsInputFile, string format = "avisynth", List<string> dataToProbe = null, string argument = null)
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

                argument = string.Format(templateArguments, format, avsInputFile, dataToProbeAsString);
            }

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
            base.Start();
            this.BeginErrorReadLine();
            this.BeginOutputReadLine();
        }

        public XmlReader Probe()
        {
            var output = new StringBuilder();
            OutputDataReceived += (sender, args) => output.AppendLine(args.Data);

            Start();
            WaitForExit();

            return XmlReader.Create(new StringReader(output.ToString()));
        }
    }
}
