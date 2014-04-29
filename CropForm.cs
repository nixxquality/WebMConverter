using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebMConverter
{
    public partial class CropForm : Form
    {
        private Corner heldCorner = Corner.None;
        private bool held = false;

        private bool insideForm;
        private bool insideRectangle;
        private Point mousePos;
        private Point mouseOffset;
        private const int maxDistance = 6;

        private RectangleF cropPercent;
        private enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            None
        }

        public CropFilter GeneratedFilter;

        public CropForm()
        {
            cropPercent = new RectangleF(0.25f, 0.25f, 0.5f, 0.5f);

            InitializeComponent();

            if (Filters.Trim != null)
                previewFrame.Frame = Filters.Trim.TrimStart;

            this.previewFrame.Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPicture_Paint);
            this.previewFrame.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewPicture_MouseDown);
            this.previewFrame.Picture.MouseEnter += new System.EventHandler(this.previewPicture_MouseEnter);
            this.previewFrame.Picture.MouseLeave += new System.EventHandler(this.previewPicture_MouseLeave);
            this.previewFrame.Picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewPicture_MouseMove);
            this.previewFrame.Picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.previewPicture_MouseUp);
        }

        public CropForm(CropFilter CropPixels) : this()
        {
            FFMSsharp.Frame frame = Program.VideoSource.GetFrame(previewFrame.Frame);

            cropPercent = new RectangleF(
                (float)CropPixels.Left / (float)frame.EncodedResolution.Width,
                (float)CropPixels.Top / (float)frame.EncodedResolution.Height,
                (float)(frame.EncodedResolution.Width - CropPixels.Left + CropPixels.Right) / (float)frame.EncodedResolution.Width,
                (float)(frame.EncodedResolution.Height - CropPixels.Top + CropPixels.Bottom) / (float)frame.EncodedResolution.Height
            );
        }

        private void textBoxFrame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int i = int.Parse(textBoxFrame.Text);
                    previewFrame.Frame = i;
                    textBoxFrame.Text = "" + i;
                }
                catch
                { }
            }
        }

        private void previewPicture_MouseDown(object sender, MouseEventArgs e)
        {
            //This checks the distance from the rectangle corner point to the mouse, and then selects the one with the smallest distance
            //That one will be dragged along with the mouse

            var closest = GetClosestPointDistance(new Point(e.X, e.Y));

            if (closest.Value < maxDistance * maxDistance) //Comparing squared distance
            {
                heldCorner = closest.Key;
                held = true;

            }
            else if (insideRectangle) //Or, if there's no closest dot and the mouse is inside the cropping rectangle, drag the entire rectangle
            {
                mouseOffset = new Point((int)(cropPercent.X * previewFrame.Picture.Width - e.X), (int)(cropPercent.Y * previewFrame.Picture.Height - e.Y));
                heldCorner = Corner.None;
                held = true;
            }


            previewFrame.Invalidate();
        }

        private KeyValuePair<Corner, float> GetClosestPointDistance(Point e)
        {
            var distances = new Dictionary<Corner, float>();
            distances[Corner.TopLeft] = (float)(Math.Pow(e.X - cropPercent.Left * previewFrame.Picture.Width, 2) + Math.Pow(e.Y - cropPercent.Top * previewFrame.Picture.Height, 2));
            distances[Corner.TopRight] = (float)(Math.Pow(e.X - cropPercent.Right * previewFrame.Picture.Width, 2) + Math.Pow(e.Y - cropPercent.Top * previewFrame.Picture.Height, 2));
            distances[Corner.BottomLeft] = (float)(Math.Pow(e.X - cropPercent.Left * previewFrame.Picture.Width, 2) + Math.Pow(e.Y - cropPercent.Bottom * previewFrame.Picture.Height, 2));
            distances[Corner.BottomRight] = (float)(Math.Pow(e.X - cropPercent.Right * previewFrame.Picture.Width, 2) + Math.Pow(e.Y - cropPercent.Bottom * previewFrame.Picture.Height, 2));

            return distances.OrderBy(a => a.Value).First();

        }

        private void previewPicture_MouseUp(object sender, MouseEventArgs e)
        {
            held = false;
            heldCorner = Corner.None;
            previewFrame.Picture .Invalidate();
        }

        private void previewPicture_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = new Point(e.X, e.Y);
            insideRectangle = cropPercent.Contains(e.X / (float)previewFrame.Picture.Width, e.Y / (float)previewFrame.Picture.Height);

            if (held)
            {
                //Here we change the size of the rectangle if the mouse is actually held down

                //Clamp mouse pos to picture box, that way you shouldn't be able to move the cropping rectangle out of bounds
                Point min = new Point(0, 0);
                Point max = new Point(previewFrame.Picture.Size);
                float clampedMouseX = Math.Max(min.X, Math.Min(max.X, e.X));
                float clampedMouseY = Math.Max(min.Y, Math.Min(max.Y, e.Y));

                float newWidth = 0;
                float newHeight = 0;
                switch (heldCorner)
                {
                    case Corner.TopLeft:
                        newWidth = cropPercent.Width - (clampedMouseX / (float)previewFrame.Picture.Width - cropPercent.X);
                        newHeight = cropPercent.Height - (clampedMouseY / (float)previewFrame.Picture.Height - cropPercent.Y);
                        cropPercent.X = clampedMouseX / (float)previewFrame.Picture.Width;
                        cropPercent.Y = clampedMouseY / (float)previewFrame.Picture.Height;
                        break;

                    case Corner.TopRight:
                        newWidth = cropPercent.Width + (clampedMouseX / (float)previewFrame.Picture.Width - cropPercent.Right);
                        newHeight = cropPercent.Height - (clampedMouseY / (float)previewFrame.Picture.Height - cropPercent.Y);
                        cropPercent.Y = clampedMouseY / (float)previewFrame.Picture.Height;
                        break;

                    case Corner.BottomLeft:
                        newWidth = cropPercent.Width - (clampedMouseX / (float)previewFrame.Picture.Width - cropPercent.X);
                        newHeight = cropPercent.Height + (clampedMouseY / (float)previewFrame.Picture.Height - cropPercent.Bottom);
                        cropPercent.X = clampedMouseX / (float)previewFrame.Picture.Width;
                        break;

                    case Corner.BottomRight:
                        newWidth = cropPercent.Width + (clampedMouseX / (float)previewFrame.Picture.Width - cropPercent.Right);
                        newHeight = cropPercent.Height + (clampedMouseY / (float)previewFrame.Picture.Height - cropPercent.Bottom);
                        break;

                    case Corner.None: //Drag entire rectangle
                        //This is a special case, because the mouse needs to be clamped according to rectangle size too!
                        float actualRectW = cropPercent.Width * previewFrame.Picture.Width;
                        float actualRectH = cropPercent.Height * previewFrame.Picture.Height;
                        clampedMouseX = Math.Max(min.X - mouseOffset.X, Math.Min(max.X - mouseOffset.X - actualRectW, e.X));
                        clampedMouseY = Math.Max(min.Y - mouseOffset.Y, Math.Min(max.Y - mouseOffset.Y - actualRectH, e.Y));
                        cropPercent.X = (clampedMouseX + mouseOffset.X) / (float)previewFrame.Picture.Width;
                        cropPercent.Y = (clampedMouseY + mouseOffset.Y) / (float)previewFrame.Picture.Height;
                        break;
                }

                if (newWidth != 0)
                    cropPercent.Width = newWidth;
                if (newHeight != 0)
                    cropPercent.Height = newHeight;
            }

            previewFrame.Picture.Invalidate();
        }

        private void previewPicture_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            //g.SmoothingMode = SmoothingMode.HighQuality;
            //TODO: this is really slow for some reason. Investigate using profiling or something.

            var edgePen = new Pen(Color.White, 1f);
            var dotBrush = new SolidBrush(Color.White);
            var outsideBrush = new HatchBrush(HatchStyle.Percent50, Color.Transparent);

            var maxW = previewFrame.Picture.Width;
            var maxH = previewFrame.Picture.Height;
            var x = cropPercent.X * previewFrame.Picture.Width;
            var y = cropPercent.Y * previewFrame.Picture.Height;
            var w = cropPercent.Width * maxW;
            var h = cropPercent.Height * maxH;

            //Darken background
            g.FillRectangle(outsideBrush, 0, 0, maxW, y);
            g.FillRectangle(outsideBrush, 0, y, x, h);
            g.FillRectangle(outsideBrush, x + w, y, maxW - (x + w), h);
            g.FillRectangle(outsideBrush, 0, y + h, maxW, maxH);

            //Edge
            g.DrawRectangle(edgePen, x, y, w, h);

            if (insideForm) //Draw corner dots if mouse is inside the picture box
            {
                float diameter = 6;
                float diameterEdge = diameter * 2;

                g.FillEllipse(dotBrush, x - diameter / 2, y - diameter / 2, diameter, diameter);
                g.FillEllipse(dotBrush, x + w - diameter / 2, y - diameter / 2, diameter, diameter);
                g.FillEllipse(dotBrush, x - diameter / 2, y + h - diameter / 2, diameter, diameter);
                g.FillEllipse(dotBrush, x + w - diameter / 2, y + h - diameter / 2, diameter, diameter);

                var closest = GetClosestPointDistance(mousePos);
                if (closest.Value < maxDistance * maxDistance)  //Comparing squared distance to avoid worthless square roots
                {
                    Cursor = Cursors.Hand;
                    //Draw outlines on the dots to indicate they can be selected and moved
                    if (closest.Key == Corner.TopLeft) g.DrawEllipse(edgePen, x - diameterEdge / 2, y - diameterEdge / 2, diameterEdge, diameterEdge);
                    if (closest.Key == Corner.TopRight) g.DrawEllipse(edgePen, x + w - diameterEdge / 2, y - diameterEdge / 2, diameterEdge, diameterEdge);
                    if (closest.Key == Corner.BottomLeft) g.DrawEllipse(edgePen, x - diameterEdge / 2, y + h - diameterEdge / 2, diameterEdge, diameterEdge);
                    if (closest.Key == Corner.BottomRight) g.DrawEllipse(edgePen, x + w - diameterEdge / 2, y + h - diameterEdge / 2, diameterEdge, diameterEdge);
                }
                else if (insideRectangle)
                    Cursor = Cursors.SizeAll;
                else if (Cursor != Cursors.Default) //Reduntant???
                    Cursor = Cursors.Default;
            }
        }

        private void previewPicture_MouseEnter(object sender, EventArgs e)
        {
            insideForm = true;
            previewFrame.Picture.Invalidate();
        }

        private void previewPicture_MouseLeave(object sender, EventArgs e)
        {
            insideForm = false;
            previewFrame.Picture.Invalidate();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (cropPercent.Left >= cropPercent.Right || cropPercent.Top >= cropPercent.Bottom)
            {
                MessageBox.Show("You messed up your crop! Please try again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cropPercent = new RectangleF(0.25f, 0.25f, 0.5f, 0.5f);
                return;
            }

            float tolerance = 0.1f; //Account for float inprecision

            if (cropPercent.Left < 0 - tolerance || cropPercent.Top < 0 - tolerance || cropPercent.Right > 1 + tolerance || cropPercent.Bottom > 1 + tolerance)
            {
                MessageBox.Show("Your crop is outside the valid range! Please try again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cropPercent = new RectangleF(0.25f, 0.25f, 0.5f, 0.5f);
                return;
            }

            cropPercent.X = Math.Max(0, cropPercent.X);
            cropPercent.Y = Math.Max(0, cropPercent.Y);
            if (cropPercent.Right > 1)
                cropPercent.Width = 1 - cropPercent.X;
            if (cropPercent.Bottom > 1)
                cropPercent.Height = 1 - cropPercent.Y;

            FFMSsharp.Frame frame = Program.VideoSource.GetFrame(previewFrame.Frame);
            GeneratedFilter = new CropFilter(
                (int)(frame.EncodedResolution.Width * cropPercent.Left),
                (int)(frame.EncodedResolution.Height * cropPercent.Top),
                -(int)(frame.EncodedResolution.Width - frame.EncodedResolution.Width * cropPercent.Right),
                -(int)(frame.EncodedResolution.Height - frame.EncodedResolution.Height * cropPercent.Bottom)
            );

            DialogResult = DialogResult.OK;

            Close();
        }
    }

    public class CropFilter : IFilter
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;

        public CropFilter(int Left, int Top, int Right, int Bottom)
        {
            this.Left = (Left / 2) * 2; // Make it even
            this.Top = (Top / 2) * 2;
            this.Right = (Right / 2) * 2;
            this.Bottom = (Bottom / 2) * 2;
        }

        public string GetAvisynthCommand()
        {
            return string.Format("Crop({0}, {1}, {2}, {3})", Left, Top, Right, Bottom);
        }
    }
}
