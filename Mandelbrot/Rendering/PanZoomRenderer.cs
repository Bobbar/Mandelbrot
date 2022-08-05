using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mandelbrot.Rendering
{
    public sealed class PanZoomRenderer : IDisposable
    {
        public Bitmap Image => targetImage;
        public event EventHandler<MouseEventArgs> MouseDown;

        private Bitmap targetImage;
        private Bitmap viewPortImg;
        private Graphics viewPortGfx;
        private Graphics viewGfx;
        private BufferedGraphicsContext currentContext;
        private BufferedGraphics buffer;

        private float currentScale = 1f;
        private const float scaleChangeMulti = 0.1f;
        private PointF viewPortOffset = new PointF();
        private PointF scaleOffset = new PointF();
        private PointF mouseMoveDownLoc = new PointF();
        private PointF windowCenter = new PointF();
        private Size targetSize = new Size();
        private Size windowSize = new Size();
        private bool mouseDown = false;
        private PictureBox pictureBox;

        public PanZoomRenderer(Bitmap image, PictureBox target)
        {
            targetImage = image;
            pictureBox = target;

            pictureBox.MouseDown -= PictureBox_MouseDown;
            pictureBox.MouseUp -= PictureBox_MouseUp;
            pictureBox.MouseMove -= PictureBox_MouseMove;
            pictureBox.MouseWheel -= PictureBox_MouseWheel;
            pictureBox.FindForm().Resize -= PanZoomRenderer_Resize;

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseWheel += PictureBox_MouseWheel;
            pictureBox.FindForm().Resize += PanZoomRenderer_Resize;

            SetTargetSize(pictureBox.Width, pictureBox.Height);
        }



        private void InitGraphics()
        {
            currentContext = BufferedGraphicsManager.Current;

            viewPortImg?.Dispose();
            viewPortImg = new Bitmap(pictureBox.Width, pictureBox.Height);

            viewGfx?.Dispose();
            viewGfx = Graphics.FromImage(viewPortImg);

            var destRect = new Rectangle(new Point(), pictureBox.Size);

            buffer?.Dispose();
            buffer = currentContext.Allocate(viewGfx, destRect);

            viewPortGfx = buffer.Graphics;
            viewPortGfx.SmoothingMode = SmoothingMode.AntiAlias;
            viewPortGfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            viewPortGfx.InterpolationMode = InterpolationMode.NearestNeighbor;
            viewPortGfx.CompositingQuality = CompositingQuality.HighSpeed;
            viewPortGfx.PixelOffsetMode = PixelOffsetMode.Half;

            viewPortGfx.ResetTransform();
            viewPortGfx.ScaleTransform(currentScale, currentScale);

            pictureBox.Image = viewPortImg;
        }

        public void Refresh()
        {
            viewPortGfx.Clear(pictureBox.BackColor);



            var panZoomOffset = viewPortOffset.Add(scaleOffset);

            viewPortGfx.DrawImage(targetImage, new RectangleF(panZoomOffset, targetSize));

            buffer.Render();

            pictureBox.Refresh();
        }

        private PointF PointToScreen(PointF point)
        {
            return new PointF(point.X / currentScale, point.Y / currentScale);
        }

        private PointF ScreenToPoint(Point point)
        {
            return new PointF(point.X / currentScale - viewPortOffset.X - scaleOffset.X, point.Y / currentScale - viewPortOffset.Y - scaleOffset.Y);
        }

        public void SetTargetSize(int width, int height)
        {
            // Don't proceed with very small dimentions. 
            if (width < 10 || height < 10)
                return;

            var newSize = new Size(width, height);
            windowSize = newSize;
            targetSize = Helpers.GetScaledImageSize(targetImage.Size, newSize, true);
            windowCenter = new PointF(windowSize.Width / 2f, windowSize.Height / 2f);

            SetScaleOffset();

            InitGraphics();

            Refresh();
        }

        private void Zoom(float newScale, Point location)
        {
            //// Clamp the new scale to min/max.
            //if (newScale > _maxScale || newScale < _minScale)
            //    return;

            // Don't proceed if there is no change.
            if (currentScale == newScale)
                return;

            // Determine displacement direction.
            bool zoomIn = true;
            if (currentScale < newScale)
                zoomIn = false;

            // Set the new scale.
            currentScale = newScale;
            SetScaleOffset();

            // Determine scaled distance from screen center.
            var targetCenter = new PointF(targetSize.Width / 2f, targetSize.Height / 2f);
            var center = windowCenter.Subtract(targetCenter);
            var d = PointToScreen(new PointF(location.X - center.X, location.Y - center.Y));

            // Apply the distplacement factor.
            var dx = d.X * scaleChangeMulti;
            var dy = d.Y * scaleChangeMulti;

            // Flip the displacment direction as needed.
            if (zoomIn)
            {
                dx *= -1;
                dy *= -1;
            }

            // Apply the final displacement to the viewport offset.
            viewPortOffset = new PointF(viewPortOffset.X - dx, viewPortOffset.Y - dy);

            // Apply the scale transform to the graphics object.
            viewPortGfx.ResetTransform();
            viewPortGfx.ScaleTransform(currentScale, currentScale);

            Refresh();
        }

        private void SetScaleOffset()
        {
            var targetCenter = new PointF(targetSize.Width / 2f, targetSize.Height / 2f);
            scaleOffset = PointToScreen(windowCenter.Subtract(targetCenter));
        }

        private void OnMouseDown(MouseEventArgs args, Point scaledLocation)
        {
            MouseDown?.Invoke(this, new MouseEventArgs(args.Button, args.Clicks, scaledLocation.X, scaledLocation.Y, args.Delta));
        }

        private void PanZoomRenderer_Resize(object? sender, EventArgs e)
        {
            SetTargetSize(pictureBox.Width, pictureBox.Height);
        }

        private void PictureBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            var scaleChange = scaleChangeMulti * currentScale;
            float newScale = currentScale;

            // Compute the new scale and perform a zoom operation accordingly.
            if (e.Delta > 0)
            {
                newScale += scaleChange;
                Zoom(newScale, e.Location);
            }
            else
            {
                newScale -= scaleChange;
                Zoom(newScale, e.Location);
            }
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                var moveDiff = e.Location.Subtract(mouseMoveDownLoc);
                viewPortOffset = viewPortOffset.Add(PointToScreen(moveDiff));
                mouseMoveDownLoc = e.Location;
                Refresh();
            }
        }

        private void PictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void PictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseMoveDownLoc = e.Location;
                mouseDown = true;
            }

            var imgLoc = ScreenToPoint(e.Location);
            var scalePoint = Helpers.ScalePoint(ScaleDirection.ToImage, imgLoc, targetImage.Size, targetSize);
            var roundedPoint = new Point((int)Math.Floor(scalePoint.X), (int)Math.Floor(scalePoint.Y));

            OnMouseDown(e, roundedPoint);
        }

        public void Dispose()
        {
            pictureBox.MouseDown -= PictureBox_MouseDown;
            pictureBox.MouseUp -= PictureBox_MouseUp;
            pictureBox.MouseMove -= PictureBox_MouseMove;
            pictureBox.MouseWheel -= PictureBox_MouseWheel;
            pictureBox.FindForm().Resize -= PanZoomRenderer_Resize;

            viewPortGfx?.Dispose();
            viewPortImg?.Dispose();
            buffer?.Dispose();
        }
    }
}
