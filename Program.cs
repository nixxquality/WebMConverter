using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebMConverter
{
    #region Filters

    static class Filters
    {
        internal static CaptionFilter Caption = null;
        internal static CropFilter Crop = null;
        internal static DeinterlaceFilter Deinterlace = null;
        internal static DenoiseFilter Denoise = null;
        internal static LevelsFilter Levels = null;
        internal static MultipleTrimFilter MultipleTrim = null;
        internal static OverlayFilter Overlay = null;
        internal static ResizeFilter Resize = null;
        internal static ReverseFilter Reverse = null;
        internal static SubtitleFilter Subtitle = null;
        internal static TrimFilter Trim = null;

        internal static void ResetFilters()
        {
            Caption = null;
            Crop = null;
            Deinterlace = null;
            Denoise = null;
            Levels = null;
            MultipleTrim = null;
            Overlay = null;
            Resize = null;
            Reverse = null;
            Subtitle = null;
            Trim = null;
        }
    }
    
    #endregion

    static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder shortPath, int shortPathLength);
    }

    static class Program
    {
        public static FFMSSharp.VideoSource VideoSource;
        public static FFMSSharp.ColorRange VideoColorRange;
        public static bool VideoInterlaced;
        public static string InputFile;
        public static FileType InputType;
        public static string FileMd5;
        public static string AttachmentDirectory;
        public static Dictionary<int, Tuple<string, SubtitleType>> SubtitleTracks; // stream id, <tag:title OR codec_name, textsub/vobsub>

        const double closeenough = 0.1;
        static int TimeToFrame(double time)
        {
            int frame = (int)((float)VideoSource.FPSNumerator / (float)VideoSource.FPSDenominator * time);
            double difference;
            bool? subtracted = null;

            FFMSSharp.Track VideoTrack = VideoSource.Track;
            FFMSSharp.FrameInfo frameinfo;
            while (true)
            {
                frameinfo = VideoTrack.GetFrameInfo(frame);

                try
                {
                    // To convert this to a timestamp in wallclock milliseconds, use the relation int64_t timestamp = (int64_t)((FFMS_FrameInfo->PTS * FFMS_TrackTimeBase->Num) / (double)FFMS_TrackTimeBase->Den).
                    difference = ((frameinfo.PTS * VideoTrack.TimeBaseNumerator) / VideoTrack.TimeBaseDenominator / 1000) - time;
                }
                catch (NullReferenceException) // We've seeked out of bounds -- the user likely requested a time longer than the video.
                {
                    frame = VideoTrack.NumberOfFrames - 1;
                    break;
                }

                if (Math.Abs(difference) < closeenough) break; // We've seeked close enough.

                if (difference < 0)
                {
                    if (subtracted == true) break; // This prevents us from flipping in an infinite loop, in the rare case that we can't get close enough.
                    frame += 1;
                    subtracted = false;
                }
                else
                {
                    if (subtracted == false) break;
                    frame -= 1;
                    subtracted = true;
                }

            }
            return frame;
        }
        internal static int TimeSpanToFrame(TimeSpan time)
        {
            return TimeToFrame(time.TotalSeconds);
        }

        static long FrameToTime(int frame)
        {
            var frameinfo = VideoSource.Track.GetFrameInfo(frame);
            return frameinfo.PTS * VideoSource.Track.TimeBaseNumerator / VideoSource.Track.TimeBaseDenominator;
        }
        internal static TimeSpan FrameToTimeSpan(int frame)
        {
            return new TimeSpan(Program.FrameToTime(frame) * 10000);
        }
        internal static string FrameToTimeStamp(int frame)
        {
            return FrameToTimeSpan(frame).ToString(@"hh\:mm\:ss");
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
                MessageBox.Show(string.Format("Failed to load AviSynth: {0}.{1}I'll open the download page, go ahead and install it.", new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Process.Start("http://avisynth.nl/index.php/Main_Page#Official_builds");
                return;
            }

            // Upgrade Application Settings if applicable
            if (Properties.Settings.Default.TryUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.TryUpgrade = false;
                Properties.Settings.Default.Save();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public enum FileType
    {
        Video,
        Avisynth
    }

    public enum SubtitleType
    {
        TextSub,
        VobSub
    }

    public static class Extensions
    {
        // http://stackoverflow.com/a/12179408/174466
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}
