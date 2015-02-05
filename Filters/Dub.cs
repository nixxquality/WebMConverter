using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFMSSharp;

namespace WebMConverter
{
    public partial class DubForm : Form
    {
        public DubFilter GeneratedFilter;

        private BackgroundWorker _worker;

        public DubForm()
        {
            InitializeComponent();
            comboDubMode.SelectedIndex = 0;
        }

        public DubForm(DubFilter dubFilter) : this()
        {
            SetFile(dubFilter.AudioFileName);
            if (!dubFilter.TrimAudio)
                comboDubMode.SelectedIndex = 1;
        }

        private void DubForm_DragEnter(object sender, DragEventArgs e)
        {
            var dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
            e.Effect = dataPresent ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void DubForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            SetFile(files[0]);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openAudioFile.ShowDialog(this);
        }

        private void openAudioFile_FileOk(object sender, CancelEventArgs e)
        {
            SetFile(((OpenFileDialog) sender).FileName);
        }

        private void SetFile(string audioFileName)
        {
            boxAudioFile.Text = audioFileName;
            openAudioFile.FileName = audioFileName;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            var audioFile = boxAudioFile.Text;
            string indexFile;

            try
            {
                if (!File.Exists(audioFile))
                    throw new FileNotFoundException();

                panelIndexingProgress.BringToFront();
                labelIndexingProgress.Text = "Hashing...";

                using (var md5 = MD5.Create())
                using (var stream = File.OpenRead(audioFile))
                {
                    var filename = new UTF8Encoding().GetBytes(Path.GetFileNameWithoutExtension(audioFile));
                    var buffer = new byte[4096];

                    filename.CopyTo(buffer, 0);
                    stream.Read(buffer, filename.Length, 4096 - filename.Length);

                    var hash = BitConverter.ToString(md5.ComputeHash(buffer));
                    indexFile = Path.Combine(Path.GetTempPath(), hash + ".ffindex");
                }

                if (File.Exists(indexFile))
                {
                    try
                    {
                        var index = new Index(indexFile);

                        if (index.BelongsToFile(audioFile))
                        {
                            DialogResult = DialogResult.OK;
                            GeneratedFilter = new DubFilter(audioFile, indexFile, comboDubMode.SelectedIndex == 0);
                            Close();
                            return;
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    File.Delete(indexFile);
                }

                labelIndexingProgress.Text = "Indexing...";
                progressIndexingProgress.Value = 0;
                progressIndexingProgress.Style = ProgressBarStyle.Continuous;
                _worker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };
                _worker.ProgressChanged += (o, args) =>
                {
                    progressIndexingProgress.Value = args.ProgressPercentage;
                };
                _worker.DoWork += (bw, workArgs) =>
                {
                    var indexer = new Indexer(audioFile, Source.Lavf);

                    indexer.UpdateIndexProgress += (ind, updateArgs) =>
                    {
                        _worker.ReportProgress((int) (((double) updateArgs.Current/updateArgs.Total)*100));
                        indexer.CancelIndexing = _worker.CancellationPending;
                    };

                    try
                    {
                        var index = indexer.Index();

                        try
                        {
                            index.GetFirstIndexedTrackOfType(TrackType.Audio);
                        }
                        catch (KeyNotFoundException)
                        {
                            throw new InvalidDataException("That file doesn't have any audio!");
                        }

                        index.WriteIndex(indexFile);

                    }
                    catch (OperationCanceledException)
                    {
                        workArgs.Cancel = true;
                    }
                };
                _worker.RunWorkerCompleted += (o, args) =>
                {
                    this.InvokeIfRequired(() =>
                    {
                        panelIndexingProgress.SendToBack();
                    });

                    if (args.Error != null)
                    {
                        this.InvokeIfRequired(() =>
                        {
                            SetFile(string.Empty);

                            if (
                                MessageBox.Show(args.Error.Message, "ERROR", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Error) == DialogResult.Cancel)
                                Close();
                        });
                        return;
                    }

                    if (args.Cancelled)
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                        GeneratedFilter = new DubFilter(audioFile, indexFile, comboDubMode.SelectedIndex == 0);
                    }

                    this.InvokeIfRequired(Close);
                };
                _worker.RunWorkerAsync();
            }
            catch (Exception err)
            {
                if (MessageBox.Show(err.Message, "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) ==
                    DialogResult.Cancel)
                    Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_worker != null && _worker.IsBusy)
                _worker.CancelAsync();
        }

        private void comboDubMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboDubMode.SelectedIndex)
            {
                case 0: // Trim audio
                    labelDubModeHint.Text = "When you want to dub a video.";
                    break;
                case 1: // Loop video
                    labelDubModeHint.Text = "When you want to dub a picture.";
                    break;
            }
        }
    }

    public class DubFilter
    {
        public readonly string AudioFileName;
        public readonly string IndexFileName;
        public readonly bool TrimAudio;

        public DubFilter(string audioFileName, string indexFileName, bool trimAudio)
        {
            AudioFileName = audioFileName;
            IndexFileName = indexFileName;
            TrimAudio = trimAudio;
        }

        public override string ToString()
        {
            // Left alone, AudioDub will potentially stretch the video out to fit the audio stream.
            // This is unwanted in most scenarios, but we can stop that by trimming any audio after the last's (AviSynth magic word) last frame.
            return string.Format(
                TrimAudio
                    ? @"Trim(AudioDub(FFAudioSource(""{0}"",cachefile=""{1}"")), 0, last.FrameCount)"
                    : @"dub = FFAudioSource(""{0}"",cachefile=""{1}""){2}Loop(-1).AudioDub(dub).Trim(0,dub.FrameCount)",
                AudioFileName, IndexFileName, Environment.NewLine);
        }
    }
}