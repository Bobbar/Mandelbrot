using Mandelbrot.Rendering;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        private Bitmap fieldImg;
        private PanZoomRenderer renderer;
        private Size fieldSize = new Size(500, 500);
        private const int NUM_THREADS = 16;
        private int colorScale = 100;
        private int maxIterations = 1000;
        private float cValue = 2.0f;
        private float zoomFact = 0.3f;
        private bool useOCL = true;
        private PointD xTMinMax = new PointD(-2.0f, 0.47f);
        private PointD yTMinMax = new PointD(-1.12f, 1.12f);

        private OpenCLCompute oclCompute;

        private List<Color> pallet = new List<Color>();

        public Form1()
        {
            InitializeComponent();

            Init();
            InitPallet();

            iterationsNumeric.Value = maxIterations;
            cValueNumeric.Value = (decimal)cValue;

            xtMinNumeric.Value = (decimal)xTMinMax.X;
            xtMaxNumeric.Value = (decimal)xTMinMax.Y;
            ytMinNumeric.Value = (decimal)yTMinMax.X;
            ytMaxNumeric.Value = (decimal)yTMinMax.Y;

            resXTextBox.Text = fieldSize.Width.ToString();
            resYTextBox.Text = fieldSize.Height.ToString();

            useOCLCheckBox.Checked = useOCL;
        }

        private void Init()
        {
            fieldImg?.Dispose();
            fieldImg = new Bitmap(fieldSize.Width, fieldSize.Height);

            renderer?.Dispose();
            renderer = new PanZoomRenderer(fieldImg, pictureBox);
            renderer.MouseDown -= Renderer_MouseDown;
            renderer.MouseDown += Renderer_MouseDown;

            oclCompute?.Dispose();
            oclCompute = new OpenCLCompute(0, new int2() { X = fieldSize.Width, Y = fieldSize.Height });
            InitPallet();
        }

        private void InitPallet()
        {
            int size = 250;
            int range = size / 6;

            pallet.Clear();

            for (int k = 0; k < size; k++)
            {
                Color c;
                if (k <= range)
                    c = Color.FromArgb(255, Lagrange(new Point(), new Point(range, 255), k), 0);
                else if (k <= range * 2)
                    c = Color.FromArgb(Lagrange(new Point(range + 1, 255), new Point(range * 2, 0), k), 255, 0);
                else if (k <= range * 3)
                    c = Color.FromArgb(0, 255, Lagrange(new Point(range * 2 + 1, 0), new Point(range * 3, 255), k));
                else if (k <= range * 4)
                    c = Color.FromArgb(0, Lagrange(new Point(range * 3 + 1, 255), new Point(range * 4, 0), k), 255);
                else if (k <= range * 5)
                    c = Color.FromArgb(Lagrange(new Point(range * 4 + 1, 0), new Point(range * 5, 255), k), 0, 255);
                else
                    c = Color.FromArgb(255, 0, Lagrange(new Point(range * 5 + 1, 255), new Point(size - 1, 0), k));

                pallet.Add(c);
            }

            oclCompute.UpdatePallet(pallet);
        }

        private int Lagrange(Point p1, Point p2, int x)
        {
            float ret = (((p1.Y * (x - p2.X)) / (float)(p1.X - p2.X)) + ((p2.Y * (x - p1.X)) / (float)(p2.X - p1.X)));
            return (int)ret;
        }

        private void Reset()
        {
            xTMinMax = new PointD(-2.0f, 0.47f);
            yTMinMax = new PointD(-1.12f, 1.12f);
            Refresh();
        }

        private unsafe void Refresh()
        {
            const int alphaOffset = 3;

            // Write the cells directly to the bitmap.
            var data = renderer.Image.LockBits(new Rectangle(new Point(), fieldSize), ImageLockMode.ReadWrite, renderer.Image.PixelFormat);

            if (useOCL)
            {
                IntPtr pxls = data.Scan0;
                oclCompute.ComputePixels(ref pxls, maxIterations, xTMinMax, yTMinMax, new Point(fieldSize.Width, fieldSize.Height), colorScale, cValue);
            }
            else
            {
                byte* pixels = (byte*)data.Scan0;
                ParallelHelpers.ParallelForSlim(fieldSize.Width, NUM_THREADS, (start, len) =>
                {
                    for (int x = start; x < len; x++)
                    {
                        for (int y = 0; y < fieldSize.Height; y++)
                        {
                            int cellIdx = (y * fieldSize.Width + x);
                            int pidx = cellIdx * 4;

                            pixels[pidx] = 0; // B
                            pixels[pidx + 1] = 0; // G
                            pixels[pidx + 2] = 0; // R
                            pixels[pidx + alphaOffset] = 0; // A

                            var it = GetPixel(x, y, maxIterations);
                            var color = GetColor(maxIterations, it);

                            pixels[pidx] = color.B;
                            pixels[pidx + 1] = color.G;
                            pixels[pidx + 2] = color.R;
                            pixels[pidx + alphaOffset] = (byte)255;

                        }
                    }
                });
            }

            renderer.Image.UnlockBits(data);
            renderer.Refresh();
        }


        private int GetPixel(int px, int py, int maxIters)
        {
            var x0 = Scale((double)px, xTMinMax.X, xTMinMax.Y, (double)0.0, fieldSize.Width);
            var y0 = Scale((double)py, yTMinMax.X, yTMinMax.Y, (double)0.0, fieldSize.Height);

            double x = 0.0;
            double y = 0.0;

            int iters = 0;

            double cSqrd = Math.Pow((double)cValue, 2.0);

            while (x * x + y * y <= cSqrd && iters < maxIters)
            {
                var xtemp = x * x - y * y + x0;
                y = (double)cValue * x * y + y0;
                x = xtemp;
                iters++;
            }

            return iters;
        }

        private double Scale(double m, double Tmin, double Tmax, double Rmin, double Rmax)
        {
            var res = ((m - Rmin) / (Rmax - Rmin)) * (Tmax - Tmin) + Tmin;
            return res;
        }

        private double GetRelPoint(double pixel, float length, PointD set)
        {
            return set.X + (pixel / (double)length) * (set.Y - set.X);
        }

        private void UpdateSets(Point loc)
        {
            var zfw = fieldSize.Width * zoomFact;
            var zfh = fieldSize.Height * zoomFact;

            var tmpX = new PointD(xTMinMax.X, xTMinMax.Y);
            var tmpY = new PointD(yTMinMax.X, yTMinMax.Y);

            xTMinMax.X = GetRelPoint((loc.X - zfw), fieldSize.Width, tmpX);
            xTMinMax.Y = GetRelPoint((loc.X + zfw), fieldSize.Width, tmpX);

            yTMinMax.X = GetRelPoint((loc.Y - zfh), fieldSize.Height, tmpY);
            yTMinMax.Y = GetRelPoint((loc.Y + zfh), fieldSize.Height, tmpY);

            Refresh();
        }

        private Color GetColor(float maxValue, float currentValue)
        {
            if (currentValue >= maxValue)
                return Color.Black;
            else
                return pallet[(int)(currentValue % (pallet.Count - 1))];
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void iterationsNumeric_ValueChanged(object sender, EventArgs e)
        {
            maxIterations = (int)iterationsNumeric.Value;
            Refresh();
        }

        private void cValueNumeric_ValueChanged(object sender, EventArgs e)
        {
            cValue = (float)cValueNumeric.Value;
            Refresh();
        }

        private void xtMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            xTMinMax.X = (float)xtMinNumeric.Value;
            Refresh();
        }

        private void xtMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            xTMinMax.Y = (float)xtMaxNumeric.Value;
            Refresh();
        }

        private void ytMinNumeric_ValueChanged(object sender, EventArgs e)
        {
            yTMinMax.X = (float)ytMinNumeric.Value;
            Refresh();
        }

        private void ytMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            yTMinMax.Y = (float)ytMaxNumeric.Value;
            //Refresh();
        }

        private void applyResButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(resXTextBox.Text.Trim(), out int xRes) && int.TryParse(resYTextBox.Text.Trim(), out int yRes))
            {
                fieldSize = new Size(xRes, yRes);
                Init();
                Refresh();
            }
        }

        private void zoomFactNumeric_ValueChanged(object sender, EventArgs e)
        {
            zoomFact = (float)zoomFactNumeric.Value;
            //Refresh();
        }

        private void Renderer_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                UpdateSets(e.Location);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void useOCLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useOCL = useOCLCheckBox.Checked;
        }
    }
}