using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
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
        public static FFMSSharp.VideoSource VideoSource;
        public static string InputFile;
        public static string FileMd5;
        public static string AttachmentDirectory;
        public static List<int> SubtitleTracks;

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
