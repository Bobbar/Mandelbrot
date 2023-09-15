using Mandelbrot.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mandelbrot.FractalRendering
{
    public class ParallelCPURenderer : IFractalRender
    {
        private Size fieldSize;
        private int numThreads = 2;

        public ParallelCPURenderer(int nThreads, Size fieldSize)
        {
            this.fieldSize = fieldSize;
            this.numThreads = nThreads;
        }

        public unsafe void UpdatePixels(nint pixels, int maxIterations, List<Complex> itVals, double radius, List<Color> pallet)
        {
            byte* pixB = (byte*)pixels;
            ParallelHelpers.ParallelForSlim(fieldSize.Width, numThreads, (start, len) =>
            {
                for (int x = start; x < len; x++)
                {
                    for (int y = 0; y < fieldSize.Height; y++)
                    {
                        int cellIdx = (y * fieldSize.Width + x);

                        int pidx = cellIdx * 4;

                        var color = GetPixel(x, y, itVals, radius, pallet);

                        pixB[pidx] = color.B;
                        pixB[pidx + 1] = color.G;
                        pixB[pidx + 2] = color.R;
                        pixB[pidx + 3] = (byte)255;
                    }
                }
            });
        }

        private Color GetPixel(int px, int py, List<Complex> xVals, double radius, List<Color> pallet)
        {
            var x0 = radius * (2.0 * (double)px - (double)fieldSize.Width) / (double)fieldSize.Width;
            var y0 = -radius * (2.0 * (double)py - (double)fieldSize.Height) / (double)fieldSize.Width;

            double x = 0.0;
            double y = 0.0;
            double zn_size = 0;

            var d0 = new Complex(x0, y0);
            var dn = d0;

            int iters = 0;
            int max = xVals.Count - 1;

            do
            {
                dn *= xVals[iters] + dn;
                dn += d0;
                iters++;
                zn_size = (xVals[iters] * 0.5 + dn).Norm();


            } while (zn_size < 256 && iters < max);


            if (iters == max)
                return Color.Black;


            var color = GetColor(zn_size, iters, pallet);
            return color;
        }

        private Color GetColor(double zn_size, int iters, List<Color> pallet)
        {
            double nu = iters - Math.Log2(Math.Log2(zn_size));
            int i = (int)(nu * 10.0) % pallet.Count;

            i = Math.Clamp(i, 0, pallet.Count);

            return pallet[i];
        }

        public void Dispose()
        {
           // :-(
        }
    }
}
