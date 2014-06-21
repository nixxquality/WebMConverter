using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class OverlayForm : Form
    {
        public OverlayFilter GeneratedFilter;

        Point point;
        string filename;

        Size videoResolution;
        Point held;
        Point beforeheld;
        Bitmap picture;

        public OverlayForm(OverlayFilter filterToEdit = null)
        {
            InitializeComponent();

            if (filterToEdit == null)
            {
                if (filePicker.ShowDialog(this) == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                filename = filePicker.FileName;
                point = new Point(0, 0);
            }
            else
            {
                filename = filterToEdit.FileName;
                point = filterToEdit.Placement;
            }

            picture = new Bitmap(filename);

            if (Filters.Trim != null)
            {
                previewFrame.Frame = Filters.Trim.TrimStart;
            }
            if (Filters.MultipleTrim != null)
            {
                previewFrame.Frame = Filters.MultipleTrim.Trims[0].TrimStart;
            }

            previewFrame.Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPicture_Paint);
            previewFrame.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewPicture_MouseDown);
            previewFrame.Picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewPicture_MouseMove);
        }

        Point getBasePoint(PictureBox pictureBox)
        {
            int basex = pictureBox.Size.Width == pictureBox.ClientSize.Width ? 0 : pictureBox.Size.Width - pictureBox.ClientSize.Width / 2;
            int basey = pictureBox.Size.Height == pictureBox.ClientSize.Height ? 0 : pictureBox.Size.Height - pictureBox.ClientSize.Height / 2;

            return new Point(basex, basey);
        }

        float getPictureScale(PictureBox pictureBox)
        {
            return (float)pictureBox.ClientSize.Width / (float)videoResolution.Width;
        }

        void previewPicture_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            var pictureBox = sender as PictureBox;

            var basePoint = getBasePoint(pictureBox);
            var scale = getPictureScale(pictureBox);

            var scaledPoint = new Point(basePoint.X + (int)(point.X * scale), basePoint.Y + (int)(point.Y * scale));

            g.DrawImage(picture, scaledPoint.X, scaledPoint.Y, picture.Width * scale, picture.Height * scale);
        }

        void previewPicture_MouseDown(object sender, MouseEventArgs e)
        {
            held = e.Location;
            beforeheld = point;

            previewPicture_MouseMove(sender, e);
        }

        void previewPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.None)
                return;

            var scale = getPictureScale(previewFrame.Picture);

            point.X = beforeheld.X + (int)((e.X - held.X) / scale);
            point.Y = beforeheld.Y + (int)((e.Y - held.Y) / scale);

            previewFrame.Picture.Invalidate();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            var frame = Program.VideoSource.GetFrame(previewFrame.Frame);
            videoResolution = frame.EncodedResolution;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            GeneratedFilter = new OverlayFilter(point, filename);

            DialogResult = DialogResult.OK;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previewFrame.Frame = Filters.Trim.TrimStart;
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previewFrame.Frame = Filters.Trim.TrimEnd;
        }

        private void frameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog<int>("Frame", previewFrame.Frame))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    previewFrame.Frame = Math.Max(0, Math.Min(Program.VideoSource.NumberOfFrames - 1, dialog.Value)); // Make sure we don't go out of bounds.
                }
            }
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog<TimeSpan>("Time", Program.FrameToTimeSpan(previewFrame.Frame)))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int i = Program.TimeSpanToFrame(dialog.Value);
                    i = Math.Max(0, Math.Min(Program.VideoSource.NumberOfFrames - 1, i)); // Make sure we don't go out of bounds.
                    previewFrame.Frame = i;
                }
            }
        }
    }

    public class OverlayFilter
    {
        public readonly Point Placement;
        public readonly string FileName;

        public OverlayFilter(Point placement, string fileName)
        {
            Placement = placement;
            FileName = fileName;
        }

        public override string ToString()
        {
            return string.Format("Overlay(ImageSource(\"{0}\"), x={1}, y={2}, mask=ImageSource(\"{0}\",pixel_type=\"RGB32\").ShowAlpha(pixel_type=\"RGB32\"))", FileName, Placement.X, Placement.Y);
        }
    }
}
