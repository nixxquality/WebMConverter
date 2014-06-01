using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WebMConverter
{
    public partial class ConverterForm : Form
    {
        private string[] _arguments;
        //private Process _process;
        private FFmpeg _ffmpegProcess;

        private Timer _timer;
        private bool _ended;
        private bool _panic;

        int currentPass = 0;
        private bool twopass;
        private bool cancelTwopass;

        string infile;
        bool needToPipe;
        FFmpeg pipeFFmpeg;

        public ConverterForm(MainForm mainForm, string input, string[] args)
        {
            InitializeComponent();

            infile = input;
            _arguments = args;
            needToPipe = Environment.Is64BitOperatingSystem;

            for (int i = 0; i < args.Length; i++)
            {
                AddInputFileToArguments(ref args[i]);
            }
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs args)
        {
            if (args.Data != null)
                textBoxOutput.Invoke((Action)(() => textBoxOutput.AppendText("\n" + args.Data)));
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs args)
        {
            if (args.Data != null)
                textBoxOutput.Invoke((Action)(() => textBoxOutput.AppendText("\n" + args.Data)));
        }

        private void ConverterForm_Load(object sender, EventArgs e)
        {
            string argument = null;
            twopass = true;
            if (_arguments.Length == 1)
            {
                twopass = false;
                argument = _arguments[0];
            }

            if (twopass)
            {
                textBoxOutput.AppendText(string.Format("{0}Arguments for pass 1: {1}", Environment.NewLine, _arguments[0]));
                textBoxOutput.AppendText(string.Format("{0}Arguments for pass 2: {1}", Environment.NewLine, _arguments[1]));
            }
            else
                textBoxOutput.AppendText("\nArguments: " + argument);

            if (twopass)
                MultiPass(_arguments);
            else
                SinglePass(argument);
        }

        void AddInputFileToArguments(ref string argument)
        {
            if (needToPipe)
            {
                argument = string.Format("-f nut -i pipe:0 {0}", argument);
            }
            else
            {
                argument = string.Format("-f avisynth -i \"{0}\" {1}", infile, argument);
            }
        }

        void StartPipe(FFmpeg ffmpeg)
        {
            if (!needToPipe)
                return;

            string proxyargs = string.Format("-f avisynth -i \"{0}\" -f nut -c copy pipe:1", infile);
            textBoxOutput.AppendText("\n--- CREATING AVISYNTH PROXY --- ");

            pipeFFmpeg = new FFmpeg(proxyargs, true);

            pipeFFmpeg.ErrorDataReceived += (o, args) => Console.WriteLine("Proxy: " + args.Data);
            pipeFFmpeg.Start(false);
            var bw = new BackgroundWorker();
            bw.DoWork += delegate(object o, DoWorkEventArgs args)
            {
                try
                {
                    pipeFFmpeg.StandardOutput.BaseStream.CopyTo(ffmpeg.StandardInput.BaseStream);
                }
                catch (IOException)
                {
                    return;
                }
            };
            pipeFFmpeg.Exited += (o, args) => ffmpeg.StandardInput.Close();
            bw.RunWorkerAsync();
        }

        private void SinglePass(string argument)
        {
            _ffmpegProcess = new FFmpeg(argument);

            _ffmpegProcess.ErrorDataReceived += ProcessOnErrorDataReceived;
            _ffmpegProcess.OutputDataReceived += ProcessOnOutputDataReceived;
            _ffmpegProcess.Exited += (o, args) => textBoxOutput.Invoke((Action)(() =>
            {
                if (_panic) return; //This should stop that one exception when closing the converter
                textBoxOutput.AppendText("\n--- FFMPEG HAS EXITED ---");
                buttonCancel.Enabled = false;

                _timer = new Timer();
                _timer.Interval = 500;
                _timer.Tick += Exited;
                _timer.Start();
            }));

            _ffmpegProcess.Start();
            StartPipe(_ffmpegProcess);
        }

        private void MultiPass(string[] arguments)
        {
            int passes = arguments.Length;

            _ffmpegProcess = new FFmpeg(arguments[currentPass]);

            _ffmpegProcess.ErrorDataReceived += ProcessOnErrorDataReceived;
            _ffmpegProcess.OutputDataReceived += ProcessOnOutputDataReceived;
            _ffmpegProcess.Exited += (o, args) => textBoxOutput.Invoke((Action)(() =>
            {
                if (_panic) return; //This should stop that one exception when closing the converter
                textBoxOutput.AppendText("\n--- FFMPEG HAS EXITED ---");

                currentPass++;
                if (currentPass < passes && !cancelTwopass)
                {
                    textBoxOutput.AppendText(string.Format("\n--- ENTERING PASS {0} ---", currentPass + 1));

                    MultiPass(arguments); //Sort of recursion going on here, be careful with stack overflows and shit
                    return;
                }

                buttonCancel.Enabled = false;

                _timer = new Timer();
                _timer.Interval = 500;
                _timer.Tick += Exited;
                _timer.Start();
            }));

            _ffmpegProcess.Start();
            StartPipe(_ffmpegProcess);
        }

        private void Exited(object sender, EventArgs eventArgs)
        {
            _timer.Stop();

            var process = _ffmpegProcess;

            if (process.ExitCode != 0)
            {
                if (cancelTwopass)
                    textBoxOutput.AppendText("\n\nConversion cancelled.");
                else
                {
                    textBoxOutput.AppendText(string.Format("\n\nffmpeg.exe exited with exit code {0}. That's usually bad.", process.ExitCode));
                    textBoxOutput.AppendText("\nIf you have no idea what went wrong, open an issue on GitHub and copy paste the output of this window there.");
                }
                pictureBox.BackgroundImage = Properties.Resources.cross;

                if (process.ExitCode == -1073741819) //This error keeps happening for me if I set threads to anything above 1, might happen for other people too
                    MessageBox.Show("It appears ffmpeg.exe crashed because of a thread error. Set the amount of threads to 1 in the advanced tab and try again.", "FYI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                textBoxOutput.AppendText("\n\nVideo converted succesfully!");
                pictureBox.BackgroundImage = Properties.Resources.tick;

                buttonPlay.Enabled = true;
            }

            buttonCancel.Text = "Close";
            buttonCancel.Enabled = true;
            _ended = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            cancelTwopass = true;

            if (!_ended || _panic) //Prevent stack overflow
            {
                if (!_ffmpegProcess.HasExited)
                    _ffmpegProcess.Kill();
            }
            else
                Close();
        }

        private void ConverterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _panic = true; //Shut down while avoiding exceptions
            buttonCancel_Click(sender, e);
        }

        private void ConverterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ffmpegProcess.Dispose();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Process.Start((Owner as MainForm).textBoxOut.Text); //Play result video
        }
    }
}
