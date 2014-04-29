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
        public static string InputFile;
    }

    interface IFilter
    {
        string GetAvisynthCommand();
    }
    static class Filters
    {
        internal static CropFilter Crop = null;
        internal static ResizeFilter Resize = null;
        internal static ReverseFilter Reverse = null;
        internal static SubtitleFilter Subtitle = null;
        internal static TrimFilter Trim = null;

        internal static void ResetFilters()
        {
            Crop = null;
            Resize = null;
            Reverse = null;
            Subtitle = null;
            Trim = null;
        }
    }

    public class ReverseFilter : IFilter
    {
        public string GetAvisynthCommand()
        {
            return "Reverse()";
        }
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
