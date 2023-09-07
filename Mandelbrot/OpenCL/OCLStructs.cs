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
		public byte B;
		public byte G;
		public byte R;
		public byte A;

        public clColor(Color color)
        {
            this.B = color.B;
            this.G = color.G;
            this.R = color.R;
            this.A = color.A;
        }

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
