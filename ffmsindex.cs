using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace WebMConverter
{
    class ffmsindex
    {
        private string ffmsindexPath = Path.Combine(Environment.CurrentDirectory, "Binaries/ffmsindex.exe");
        private Process Process;

        public ffmsindex(string filename)
        {
            Process = new Process();
            string outputfile = "";
            string arguments = "";

            // Prepare the preparation
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    outputfile = Path.Combine(Path.GetTempPath(), BitConverter.ToString(md5.ComputeHash(stream)) + ".ffindex");
                }
            }
            arguments = string.Format("\"{0}\" \"{1}\"", filename, outputfile);

            // Make sure we're not wasting time here
            if (File.Exists(outputfile))
                return;

            // Prepare the process
            ProcessStartInfo info = new ProcessStartInfo(ffmsindexPath);
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            info.Arguments = arguments;
            Process.StartInfo = info;
            Process.EnableRaisingEvents = true;

            // We don't need anything fancy for ffmsindex, just start it
            Process.Start();
        }
    }
}
