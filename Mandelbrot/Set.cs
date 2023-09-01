using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
	[Serializable]
	public class Set
	{
		public PointD Center = new PointD();
		public double Radius = 0;

		public Set() { }

		public Set(PointD center, double radius)
		{
			Center = center;
			Radius = radius;
		}

		public override string ToString()
		{
			return $"C: {Center}  R: {Radius}";
		}
	}
}
