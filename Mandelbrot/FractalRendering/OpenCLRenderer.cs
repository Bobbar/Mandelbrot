using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using unvell.D2DLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mandelbrot.FractalRendering
{
    public class OpenCLRenderer : IFractalRender
    {
        private OpenCLCompute _compute;

        public OpenCLRenderer(int deviceIdx, Size imageSize)
        {
            _compute = new OpenCLCompute(deviceIdx, imageSize);
        }

        public void UpdatePixels(nint pixels, int maxIterations, List<Complex> itVals, double radius, List<Color> pallet)
        {
            IntPtr pxls = pixels;
            _compute.ComputePixels(ref pxls, itVals, radius, pallet);
        }

        public void Dispose()
        {
           _compute?.Dispose();
        }
    }
}
