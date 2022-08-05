using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot.Rendering
{
    public static class PointExtensions
    {
        public static PointF SnapToGrid(this PointF point, int gridSize)
        {
            return new PointF((int)point.X >> gridSize << gridSize, (int)point.Y >> gridSize << gridSize);
        }

        public static Point SnapToGrid(this Point point, int gridSize)
        {
            return new Point(point.X >> gridSize << gridSize, point.Y >> gridSize << gridSize);
        }

        public static GraphicsPath ToGraphicsPath(this PointF[] points)
        {
            var path = new GraphicsPath();
            path.AddLines(points);
            return path;
        }

        public static PointF[] Add(this PointF[] points, PointF offset)
        {
            var pnts = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                pnts[i] = points[i].Add(offset);
            }

            return pnts;
        }

        public static PointF Multi(this PointF pointA, float value)
        {
            return new PointF(pointA.X * value, pointA.Y * value);
        }

        public static PointF Multi(this PointF pointA, PointF pointB)
        {
            return new PointF(pointA.X * pointB.X, pointA.Y * pointB.Y);
        }
        public static PointF Divide(this PointF pointA, float value)
        {
            return new PointF(pointA.X / value, pointA.Y / value);
        }

        public static PointF Add(this PointF pointA, PointF pointB)
        {
            return new PointF(pointA.X + pointB.X, pointA.Y + pointB.Y);
        }

        public static PointF Add(this PointF pointA, float value)
        {
            return new PointF(pointA.X + value, pointA.Y + value);
        }

        public static Point Add(this Point pointA, PointF pointB)
        {
            return new Point(pointA.X + (int)pointB.X, pointA.Y + (int)pointB.Y);
        }

        public static PointF Subtract(this PointF pointA, PointF pointB)
        {
            var diffX = pointA.X - pointB.X;
            var diffY = pointA.Y - pointB.Y;

            return new PointF(diffX, diffY);
        }

        public static PointF Subtract(this Point pointA, PointF pointB)
        {
            var diffX = pointA.X - pointB.X;
            var diffY = pointA.Y - pointB.Y;

            return new PointF(diffX, diffY);
        }

        public static float DistanceSqrt(this PointF pointA, PointF pointB)
        {
            return (float)Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2));
        }

        public static float DistanceSqrt(this Point pointA, PointF pointB)
        {
            return (float)Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2));
        }

        public static Point ToPoint(this PointF point)
        {
            return new Point((int)point.X, (int)point.Y);
        }
    }
}
