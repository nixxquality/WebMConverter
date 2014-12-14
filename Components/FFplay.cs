using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebMConverter
{
    class FFplay : Process
    {
        public string FFplayPath = Path.Combine(Environment.CurrentDirectory, "Binaries", "Win32", "ffplay.exe");

        StringBuilder errorLog;
        public string ErrorLog
        {
            get { return errorLog.ToString().Trim(); }
        }

        public FFplay(string argument)
        {
            this.StartInfo.FileName = FFplayPath;
            this.StartInfo.Arguments = argument;
            this.StartInfo.RedirectStandardInput = true;
            this.StartInfo.RedirectStandardOutput = true;
            this.StartInfo.RedirectStandardError = true;
            this.StartInfo.UseShellExecute = false; //Required to redirect IO streams
            this.StartInfo.CreateNoWindow = true; //Hide console
            this.EnableRaisingEvents = true;

            errorLog = new StringBuilder();
            ErrorDataReceived += (sender, args) => errorLog.AppendLine(args.Data);

#if DEBUG
            OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);
#endif
        }

        new public void Start()
        {
            base.Start();
            this.BeginErrorReadLine();
            this.BeginOutputReadLine();
        }
    }
}
