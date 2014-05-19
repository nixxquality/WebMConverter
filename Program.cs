using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
    #region Filters

    static class Filters
    {
        internal static CropFilter Crop = null;
        internal static DeinterlaceFilter Deinterlace = null;
        internal static LevelsFilter Levels = null;
        internal static ResizeFilter Resize = null;
        internal static ReverseFilter Reverse = null;
        internal static SubtitleFilter Subtitle = null;
        internal static TrimFilter Trim = null;

        internal static void ResetFilters()
        {
            Crop = null;
            Deinterlace = null;
            Levels = null;
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
    public class LevelsFilter
    {
        public override string ToString()
        {
            return "ColorYUV(levels=\"TV->PC\")";
        }
    }

    #endregion

    static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);
    }

    static class Program
    {
        public static FFMSSharp.VideoSource VideoSource;
        public static FFMSSharp.ColorRange VideoColorRange;
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
            // Check for AviSynth
            if (NativeMethods.LoadLibrary("avisynth") == IntPtr.Zero)
            {
                MessageBox.Show(string.Format("Failed to load AviSynth: {0}.{1}I'll open the download page, go ahead and install it.", new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message, Environment.NewLine) , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.Start("http://avisynth.nl/index.php/Main_Page#Official_builds");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


    }
}
