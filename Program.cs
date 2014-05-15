using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
    static class Filters
    {
        internal static CropFilter Crop = null;
        internal static DeinterlaceFilter Deinterlace = null;
        internal static ResizeFilter Resize = null;
        internal static ReverseFilter Reverse = null;
        internal static SubtitleFilter Subtitle = null;
        internal static TrimFilter Trim = null;

        internal static void ResetFilters()
        {
            Crop = null;
            Deinterlace = null;
            Resize = null;
            Reverse = null;
            Subtitle = null;
            Trim = null;
        }
    }

    public class DeinterlaceFilter
    {
        public override string ToString()
        {
            return "tdeint()";
        }
    }
    public class ReverseFilter
    {
        public override string ToString()
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

        internal static int TimeToFrame(double time)
        {
            return (int)((float)VideoSource.FPSNumerator / (float)VideoSource.FPSDenominator * time);
        }

        internal static double FrameToTime(int frame)
        {
            var frameinfo = VideoSource.Track.GetFrameInfo(frame);
            return frameinfo.PTS * (double)VideoSource.Track.TimeBaseNumerator / (double)VideoSource.Track.TimeBaseDenominator / 1000.0f;
        }
        internal static string FrameToTimeStamp(int frame)
        {
            double t = Program.FrameToTime(frame);
            int h = (int)(t % 3600);
            int m = (int)((t - h * 3600) % 60);
            int s = (int)(t - h * 3600 - m * 60);
            return new TimeSpan(h, m, s).ToString("c");
        }

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
