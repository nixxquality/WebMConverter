using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
    static class FFMS2
    {
        public static FFMSsharp.VideoSource VideoSource;
    }

    static class Filters
    {
        public static TrimFilter Trim = null;
        public static CropFilter Crop = null;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
