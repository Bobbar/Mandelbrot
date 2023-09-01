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
		public PointD xTMinMax = new PointD();
		public PointD yTMinMax = new PointD();
		public PointD Center = new PointD();
		public double Radius = 0;

		public Set() { }
		public Set(PointD xT, PointD yT, PointD center, double radius) 
		{
			xTMinMax = xT;
			yTMinMax = yT;
			Center = center;
			Radius = radius;
		}

		public override string ToString()
		{
			return $"xT: {xTMinMax}  yT: {yTMinMax}  C: {Center}  R: {Radius}";
		}

	}
}
