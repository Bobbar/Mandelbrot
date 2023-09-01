using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    public struct int2
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    public struct clColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    public struct Rule
    {
        public int B;
        public int S;
        public int C;
    }

    public struct PointD
    {
        public double X;
        public double Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

		public override string ToString()
		{
            return $"({X}, {Y})";
		}
	}
}
