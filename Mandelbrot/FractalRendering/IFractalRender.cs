using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Mandelbrot.FractalRendering
{
    internal interface IFractalRender : IDisposable
    {
        void UpdatePixels(IntPtr pixels, int maxIterations, List<Complex> itVals, double radius, List<Color> pallet);
    }
}
