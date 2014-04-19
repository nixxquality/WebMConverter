using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class MainForm : Form
    {
        private string _template;
        private string _templateArguments;

        private string _autoOutput;
        private string _autoTitle;
        private string _autoArguments;
        private bool _argumentError;

        public Size AssumedInputSize; //This will get set as soon as the crop form generates an input file. It's assumed because the user could've changed the video after cropping.
        //Might want to get a definite, reliable way to get the size of the input video.

        public RectangleF CroppingRectangle  //This is in the [0-1] region, multiply it by the resolution to get the crop coordinates in pixels
        {
            get { return _croppingRectangle; }
            set
            {
                _croppingRectangle = value;/*
                if (_croppingRectangle == CropForm.FullCrop)
                    labelCrop.Text = "Don't crop";
                else
                    labelCrop.Text = string.Format(CultureInfo.InvariantCulture, "X:{0:0%} Y:{1:0%} W:{2:0%} H:{3:0%}",
                                                   _croppingRectangle.X,
                                                   _croppingRectangle.Y,
                                                   _croppingRectangle.Width,
                                                   _croppingRectangle.Height);*/
            }
        }
        private RectangleF _croppingRectangle; //Using a backing field so we can update the label as soon as something changed it!

        //public RectangleF CroppingRectangle; //This is in the [0-1] region, multiply it by the resolution to get the crop coordinates in pixels

        public MainForm()
        {
            InitializeComponent();

            CroppingRectangle = new RectangleF(0, 0, 1, 1); //Crop nothing by default

            AllowDrop = true;
            DragEnter += HandleDragEnter;
            DragDrop += HandleDragDrop;

            _templateArguments = "{0} -c:v libvpx -crf 32 -b:v {1}K -threads {2} {3} {4} {5}";
            //{0} is '-an' if no audio, otherwise blank
            //{1} is bitrate in kb/s
            //{2} is amount of threads to use
            //{3} is '-fs XM' if X MB limit enabled otherwise blank
            //{4} is '-metadata title="TITLE"' when specifying a title, otherwise blank
            //{5} is '-quality best -lag-in-frames 16' when using HQ mode, otherwise blank

            //TODO: add an option for subtitles. It's either '-vf "ass=subtitle.ass"' or '-vf subtitles=subtitle.srt'

            _template = "-f avisynth -i \"{0}\" {2} {3} {4} -f webm -y \"{1}\"";
            //{0} is input file
            //{1} is output file
            //{2} is extra arguments
            //{3} is '-pass X' if 2-pass enabled, otherwise blank
            //{4} is '-auto-alt-ref 1' is 2-pass enabled AND HQ mode enabled, otherwise blank

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Keeping this disabled for now because threads are crashy

            //int threads = Environment.ProcessorCount;  //Set thread slider to default of 4
            //trackThreads.Value = Math.Min(trackThreads.Maximum, Math.Max(trackThreads.Minimum, threads));

            trackThreads_Scroll(sender, e); //Update label
        }

        private void buttonBrowseIn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    SetFile(dialog.FileName);
            }
        }

        private void SetFile(string path)
        {
            textBoxIn.Text = path;
            string fullPath = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            if (boxMetadataTitle.Text == _autoTitle || boxMetadataTitle.Text == "")
                boxMetadataTitle.Text = _autoTitle = name;
            if (textBoxOut.Text == _autoOutput || textBoxOut.Text == "")
                textBoxOut.Text = _autoOutput = Path.Combine(fullPath, name + ".webm");
            // Generate ffindex file for ffms2
            new ffmsindex(path);
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            // show copy cursor for files
            bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = dataPresent ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) SetFile(file);
        }

        private void buttonBrowseOut_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.OverwritePrompt = true;
                dialog.ValidateNames = true;
                dialog.Filter = "WebM files|*.webm";

                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    textBoxOut.Text = dialog.FileName;
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string result = Convert();
            if (!string.IsNullOrWhiteSpace(result))
                MessageBox.Show(result, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        char[] invalidChars = Path.GetInvalidPathChars();

        private string Convert()
        {
            string input = textBoxIn.Text;
            string output = textBoxOut.Text;

            if (string.IsNullOrWhiteSpace(input))
                return "No input file!";
            if (string.IsNullOrWhiteSpace(output))
                return "No output file!";

            if (invalidChars.Any(input.Contains))
                return "Input path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars);
            if (invalidChars.Any(output.Contains))
                return "Output path contains invalid characters!\nInvalid characters: " + string.Join(" ", invalidChars);

            if (!File.Exists(input))
                return "Input file doesn't exist!";

            if (input == output)
                return "Input and output files are the same!";

            string options = textBoxArguments.Text;
            try
            {
                if (options.Trim() == "" || _argumentError)
                    options = GenerateArguments();
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }

            // Make our temporary file for the AviSynth script
            string avsFileName = Path.GetTempFileName();
            using (StreamWriter avscript = new StreamWriter(avsFileName, false))
            {
                avscript.WriteLine("PluginPath = \"" + Environment.CurrentDirectory + "/Binaries/\"");
                avscript.WriteLine("LoadPlugin(PluginPath+\"ffms2.dll\")");
                avscript.WriteLine("LoadPlugin(PluginPath+\"vsfilter.dll\")");
                avscript.WriteLine("FFVideoSource(\"" + input + "\")");
                avscript.Write(textBoxProcessingScript.Text);
            }

            string[] arguments;
            if (!checkBox2Pass.Checked)
                arguments = new[] { string.Format(_template, avsFileName, output, options, "", "") };
            else
            {
                int passes = 2; //Can you even use more than 2 passes?

                string HQ = "";
                if (boxHQ.Checked)
                    HQ = "-auto-alt-ref 1"; //This should improve quality, only in 2-pass mode

                arguments = new string[passes];
                for (int i = 0; i < passes; i++)
                    arguments[i] = string.Format(_template, input, output, options, "-pass " + (i + 1), HQ);
            }

            var form = new ConverterForm(this, arguments);
            form.ShowDialog();

            return null;
        }

        private void UpdateArguments(object sender, EventArgs e)
        {
            try
            {
                string arguments = GenerateArguments();
                if (arguments != _autoArguments || _argumentError)
                {
                    textBoxArguments.Text = _autoArguments = arguments;
                    _argumentError = false;
                }
            }
            catch (ArgumentException argExc)
            {
                textBoxArguments.Text = "ERROR: " + argExc.Message;
                _argumentError = true;
            }
        }

        private string GenerateArguments()
        {
            float limit = 0;
            string limitTo = "";
            if (!string.IsNullOrWhiteSpace(boxLimit.Text))
            {
                if (!float.TryParse(boxLimit.Text, out limit))
                    throw new ArgumentException("Invalid size limit!");
                limitTo = string.Format("-fs {0}M", limit.ToString(CultureInfo.InvariantCulture)); //Should turn comma into dot
            }

            int bitrate = 900; // STUB

            int threads = trackThreads.Value;

            string metadataTitle = "";
            if (!string.IsNullOrWhiteSpace(boxMetadataTitle.Text))
                metadataTitle = string.Format("-metadata title=\"{0}\"", boxMetadataTitle.Text.Replace("\"", "\\\""));

            string HQ = "";
            if (boxHQ.Checked)
                HQ = "-quality best -lag-in-frames 16";

            string audioEnabled = boxAudio.Checked ? "" : "-an"; //-an if no audio
            return string.Format(_templateArguments, audioEnabled, bitrate, threads, limitTo, metadataTitle, HQ);
        }

        private static string MakeParseFriendly(string text)
        {
            //This method adds "00:" in front of text, if the text format is in either 00:00 or 00:00.00 format.
            //This pattern should work.

            string pattern = @"^[0-5][0-9]:[0-5][0-9](\.[0-9]+)?$";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            if (regex.IsMatch(text))
                return "00:" + text;
            return text;
        }

        private void trackThreads_Scroll(object sender, EventArgs e)
        {
            labelThreads.Text = trackThreads.Value.ToString();
            UpdateArguments(sender, e);
        }

        private void toolStripButtonTrim_Click(object sender, EventArgs e)
        {
            textBoxProcessingScript.AppendText(Environment.NewLine + "Trim(start_frame, end_frame)"); // STUB
        }

        private void toolStripButtonCrop_Click(object sender, EventArgs e)
        {
            textBoxProcessingScript.AppendText(Environment.NewLine + "Crop(left, top, -right, -bottom)"); // STUB
        }

        private void toolStripButtonResize_Click(object sender, EventArgs e)
        {
            textBoxProcessingScript.AppendText(Environment.NewLine + "LanczosResize(width, height)"); // STUB
        }
    }
}
