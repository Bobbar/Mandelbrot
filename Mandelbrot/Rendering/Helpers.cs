using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mandelbrot.Rendering
{
    public static class Helpers
    {
        /// <summary>
        /// Returns the scaled size for an image display in a window of the specified size.
        /// </summary>
        /// <param name="imageSize">The unscaled 'true' size of the image.</param>
        /// <param name="windowSize">The size of the target window or control.</param>
        /// <param name="keepAspectRatio">Optional. True if the scaled result should honor the original aspect ratio. </param>
        /// <returns></returns>
        public static Size GetScaledImageSize(Size imageSize, Size windowSize, bool keepAspectRatio = true)
        {
            var scaleFactor = ScaleFactor(imageSize, windowSize, keepAspectRatio);
            var scaledSize = new Size((int)(imageSize.Width / scaleFactor.X), (int)(imageSize.Height / scaleFactor.Y));

            return scaledSize;
        }

        public static PointF ScaleFactor(Size imageSize, Size windowSize, bool keepAspectRatio = true)
        {
            var wfactor = (float)imageSize.Width / windowSize.Width;
            var hfactor = (float)imageSize.Height / windowSize.Height;

            var resizeFactor = Math.Max(wfactor, hfactor);
            if (keepAspectRatio)
            {
                wfactor = resizeFactor;
                hfactor = resizeFactor;
            }

            return new PointF(wfactor, hfactor);
        }

        public static PointF[] ScalePointsToWindow(PointF[] points, Size imageSize, Size windowSize)
        {
            return ScalePoints(ScaleDirection.ToWindow, points, imageSize, windowSize);
        }

        public static PointF[] ScalePointsToImage(PointF[] points, Size imageSize, Size windowSize)
        {
            return ScalePoints(ScaleDirection.ToImage, points, imageSize, windowSize);
        }

        public static PointF[] ScalePoints(ScaleDirection dir, PointF[] points, Size imageSize, Size windowSize)
        {
            // Compute the ratio between the actual and scaled image sizes.
            float xRatio = imageSize.Width / (float)windowSize.Width;
            float yRatio = imageSize.Height / (float)windowSize.Height;
            var ratio = new PointF(xRatio, yRatio);

            var scalePts = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                scalePts[i] = ScalePoint(dir, points[i], ratio);
            }

            return scalePts;
        }

        public static PointF ScalePoint(ScaleDirection dir, PointF point, Size imageSize, Size windowSize)
        {
            var pts = new PointF[] { point };
            var scaled = ScalePoints(dir, pts, imageSize, windowSize);
            return scaled[0];
        }

        public static PointF ScalePoint(ScaleDirection direction, PointF point, PointF ratio)
        {
            if (direction == ScaleDirection.ToImage)
                return new PointF(point.X * ratio.X, point.Y * ratio.Y);
            else
                return new PointF(point.X / ratio.X, point.Y / ratio.Y);

        }

		
	}
}
