using Mandelbrot.Rendering;
using Microsoft.VisualBasic.Logging;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mandelbrot
{
	public partial class Form1 : Form
	{
		private Bitmap fieldImg;
		private PanZoomRendererD2D renderer;

		private Size fieldSize = new Size(1000, 1000);
		private int NUM_THREADS = 4;
		private int maxIterations = 1000;
		private float zoomFact = 2.0f;
		private bool useOCL = false;

		private PointD center = new PointD();
		private double radius = 2.0;

		private OpenCLCompute oclCompute;
		private int oclDeviceIdx = -1;

		private List<Color> pallet = new List<Color>();
		private List<Color> palletSource = new List<Color>();

		private readonly List<Color> defaultPalletSource = new List<Color>()
		{
			Color.FromArgb(0,7,100),
			Color.FromArgb(32,107,203),
			Color.FromArgb(237,255,255),
			Color.FromArgb(255,170,0),
			Color.FromArgb(0,2,0)
		};

		private Point lastMouseDownLoc = new Point();
		private List<Set> _prevSets = new List<Set>();
		private Set _lastSet = new Set();

		private System.Windows.Forms.Timer _refreshTimer = new System.Windows.Forms.Timer();

		public Form1()
		{
			InitializeComponent();

			palletSource = new List<Color>(defaultPalletSource);
			NUM_THREADS = Environment.ProcessorCount;

			_refreshTimer.Interval = 250;
			_refreshTimer.Tick += RefreshTimer_Tick;

			EnumOclDevices();

			Init();

			iterationsNumeric.Value = maxIterations;
			zoomFactNumeric.Value = (decimal)zoomFact;

			centerXTextBox.Text = center.X.ToString();
			centerYTextBox.Text = center.Y.ToString();
			radiusTextBox.Text = radius.ToString();

			resXTextBox.Text = fieldSize.Width.ToString();
			resYTextBox.Text = fieldSize.Height.ToString();

			useOCLCheckBox.Checked = useOCL;

			Reset();

		}

		private void RefreshTimer_Tick(object? sender, EventArgs e)
		{
			renderer?.Refresh();
			_refreshTimer.Stop();
		}

		private void EnumOclDevices()
		{
			oclDeviceCombo.DataSource = null;
			oclDeviceCombo.Items.Clear();

			var devices = OpenCLCompute.GetDevices();
			if (devices.Count == 0)
			{
				//throw new Exception("No OpenCL devices found.");
				MessageBox.Show("No OpenCL devices found.");
				useOCL = false;
				return;
			}
			else
				oclDeviceIdx = 0;

			oclDeviceCombo.ValueMember = "Item1";
			oclDeviceCombo.DisplayMember = "Item2";

			var cmbDevs = new List<Tuple<int, string>>();
			devices.ForEach(d => cmbDevs.Add(new Tuple<int, string>(devices.IndexOf(d), d.Name)));
			oclDeviceCombo.DataSource = cmbDevs;
		}

		private void Init()
		{
			fieldImg?.Dispose();
			fieldImg = new Bitmap(fieldSize.Width, fieldSize.Height);

			renderer?.Dispose();
			renderer = new PanZoomRendererD2D(fieldImg, pictureBox);

			renderer.MouseDown -= Renderer_MouseDown;
			renderer.MouseDown += Renderer_MouseDown;

			try
			{
				oclCompute?.Dispose();
				oclCompute = new OpenCLCompute(oclDeviceIdx, new int2() { X = fieldSize.Width, Y = fieldSize.Height });
				useOCL = true;
			}
			catch
			{
				useOCL = false;
				MessageBox.Show("Error initializing OpenCL device.");

			}

			InitPallet();
		}

		private void InitPallet()
		{
			pallet.Clear();

			var cols = palletSource;

			for (int i = 0; i < cols.Count; i++)
			{
				var cur = cols[i];
				var next = (i + 1 == cols.Count) ? cols.First() : cols[i + 1];
				for (double d = 0.0; d < 1.0; d += 0.01)
				{
					pallet.Add(Interp(cur, next, d));
				}
			}

			oclCompute.UpdatePallet(pallet);
		}

		private Color Interp(Color color1, Color color2, double mid)
		{
			int r = (int)(color1.R + (color2.R - color1.R) * mid);
			int g = (int)(color1.G + (color2.G - color1.G) * mid);
			int b = (int)(color1.B + (color2.B - color1.B) * mid);
			return Color.FromArgb(r, g, b);
		}

		private void Reset()
		{
			center = new PointD();
			radius = 2.0;

			centerXTextBox.Text = center.X.ToString();
			centerYTextBox.Text = center.Y.ToString();
			radiusTextBox.Text = radius.ToString();

			_prevSets.Clear();
			_lastSet = new Set(center, radius);

			RefreshImage();
		}

		private unsafe void RefreshImage()
		{
			const int alphaOffset = 3;
			var itVals = ItVals();

			this.Text = "Mandelbrot (Processing...)";
			
			// Write directly to the bitmap.
			using (var bmpLock = new BitmapLock(renderer.Image))
			{
				var data = bmpLock.Data;

				if (useOCL)
				{
					IntPtr pxls = data.Scan0;
					oclCompute.ComputePixels(ref pxls, maxIterations, new Point(fieldSize.Width, fieldSize.Height), itVals, radius);
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

								var color = GetPixel(x, y, maxIterations, itVals);

								pixels[pidx] = color.B;
								pixels[pidx + 1] = color.G;
								pixels[pidx + 2] = color.R;
								pixels[pidx + alphaOffset] = (byte)255;
							}
						}
					});
				}

			}

			renderer.Refresh();

			this.Text = "Mandelbrot";
		}


		private Color GetPixel(int px, int py, int maxIters, List<Complex> xVals)
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


			} while (zn_size < 500 && iters < max);


			if (iters == max)
				return Color.Black;


			var color = GetColor(zn_size, iters);
			return color;
		}

		private Color GetColor(double zn_size, int iters)
		{
			//if (iters >= maxIterations)
			//    return Color.Black;
			//else
			//{
			double nu = iters - Math.Log2(Math.Log2(zn_size));
			int i = (int)(nu * 10.0) % pallet.Count;

			if (i < 0)
				return Color.Black;

			return pallet[i];
			//}

		}

		private List<Complex> ItVals()
		{
			var v = new List<Complex>();

			double xn_r = center.X;
			double xn_i = center.Y;

			for (int i = 0; i != maxIterations; i++)
			{
				double re = xn_r + xn_r;
				double im = xn_i + xn_i;

				var c = new Complex(re, im);

				v.Add(c);

				if (re > 1024 || im > 1024 || re < -1024 || im < -1024)
					return v;

				xn_r = xn_r * xn_r - xn_i * xn_i + center.X;
				xn_i = re * xn_i + center.Y;
			}

			return v;
		}


		private double GetRelPoint(double pixel, float length, PointD set)
		{
			return set.X + (pixel / (double)length) * (set.Y - set.X);
		}

		private void UpdateSets(Point loc, bool findNearest = true)
		{
			Point nearest = loc;

			if (findNearest)
				nearest = FindNearestValidPoint(loc);

			lastMouseDownLoc = nearest;

			var zfw = fieldSize.Width * 1f;
			var zfh = fieldSize.Height * 1f;

			center.X += radius * (2.0 * nearest.X - fieldSize.Width) / fieldSize.Width;
			center.Y += -radius * (2.0 * nearest.Y - fieldSize.Height) / fieldSize.Width;

			//radius /= 2.0;

			radius *= (1f - (zoomFact / 10f));

			_prevSets.Add(_lastSet);
			_lastSet = new Set(center, radius);

			centerXTextBox.Text = center.X.ToString();
			centerYTextBox.Text = center.Y.ToString();
			radiusTextBox.Text = radius.ToString();

			RefreshImage();
		}


		private unsafe Point FindNearestValidPoint(Point loc)
		{
			const int range = 50;
			const int BLACK_PIXEL = 0;

			int startX = loc.X - range;
			int endX = loc.X + range;
			int startY = loc.Y - range;
			int endY = loc.Y + range;

			startX = Math.Clamp(startX, 0, renderer.Image.Width);
			endX = Math.Clamp(endX, 0, renderer.Image.Width);
			startY = Math.Clamp(startY, 0, renderer.Image.Height);
			endY = Math.Clamp(endY, 0, renderer.Image.Height);

			Point minPnt = loc;
			float minDist = float.MaxValue;

			using (var bmpLock = new BitmapLock(renderer.Image))
			{
				var data = bmpLock.Data;
				byte* pixels = (byte*)data.Scan0;
				int cellIdx = (loc.Y * fieldSize.Width + loc.X);
				int pidx = cellIdx * 4;

				// Don't proceed if we are already on a valid pixel.
				var selectedColor = pixels[pidx] + pixels[pidx + 1] + pixels[pidx + 2];

				if (selectedColor == BLACK_PIXEL)
					return loc;

				for (int x = startX; x < endX; x++)
					for (int y = startY; y < endY; y++)
					{
						cellIdx = (y * fieldSize.Width + x);
						pidx = cellIdx * 4;

						var color = pixels[pidx] + pixels[pidx + 1] + pixels[pidx + 2];

						if (color == BLACK_PIXEL)
						{
							var dist = (float)(Math.Pow((x - loc.X), 2) + Math.Pow(y - loc.Y, 2));
							minDist = Math.Min(minDist, dist);

							if (minDist == dist)
								minPnt = new Point(x, y);
						}

					}

				return minPnt;
			}
		}


		private void SaveImage()
		{
			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "Images|*.png;*.bmp;*.jpg;*.tiff";

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					var format = ImageFormat.Bmp;
					var ext = Path.GetExtension(dlg.FileName);

					switch (ext)
					{
						case ".jpg":
						case ".jpeg":
							format = ImageFormat.Jpeg;
							break;

						case ".png":
							format = ImageFormat.Png;
							break;

						case ".tiff":
							format = ImageFormat.Tiff;
							break;

					}

					renderer.Image.Save(dlg.FileName, format);
				}
			}
		}

		private void SaveSet()
		{
			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "Set|*.set;";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					using (var stream = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write))
					{
						var serializer = new XmlSerializer(typeof(Set));
						serializer.Serialize(stream, _lastSet);
					}
				}
			}
		}

		private void LoadSet()
		{
			using (var dlg = new OpenFileDialog())
			{
				dlg.Filter = "Set|*.set;";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					using (var stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
					{
						var serializer = new XmlSerializer(typeof(Set));
						var set = serializer.Deserialize(stream) as Set;

						center = set.Center;
						radius = set.Radius;

						_prevSets.Clear();
						_lastSet = set;

						RefreshImage();
					}
				}
			}
		}

		private void ChoosePallet()
		{
			using (var palletFrm = new ChoosePalletForm(palletSource, defaultPalletSource))
			{
				if (palletFrm.ShowDialog() == DialogResult.OK)
				{
					palletSource = palletFrm.PalletSource;
				}
			}

			InitPallet();
			RefreshImage();
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			RefreshImage();
		}

		private void iterationsNumeric_ValueChanged(object sender, EventArgs e)
		{
			maxIterations = (int)iterationsNumeric.Value;
			RefreshImage();
		}

		private void zoomFactNumeric_ValueChanged(object sender, EventArgs e)
		{
			zoomFact = (float)zoomFactNumeric.Value;
			//Refresh();
		}

		private void applyResButton_Click(object sender, EventArgs e)
		{
			if (int.TryParse(resXTextBox.Text.Trim(), out int xRes) && int.TryParse(resYTextBox.Text.Trim(), out int yRes))
			{
				fieldSize = new Size(xRes, yRes);

				resXTextBox.Text = fieldSize.Width.ToString();
				resYTextBox.Text = fieldSize.Height.ToString();

				Init();
				RefreshImage();
			}
		}

		private void Renderer_MouseDown(object? sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				UpdateSets(e.Location);
			}
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			Reset();
		}

		private void useOCLCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			useOCL = useOCLCheckBox.Checked;

			oclDeviceCombo.Enabled = useOCL;
		}


		private void prevButton_Click(object sender, EventArgs e)
		{
			if (_prevSets.Count == 0)
				return;

			var prevSet = _prevSets.Last();
			_prevSets.RemoveAt(_prevSets.Count - 1);

			center = prevSet.Center;
			radius = prevSet.Radius;

			_lastSet = prevSet;

			RefreshImage();
		}

		private void pictureBox_Paint(object sender, PaintEventArgs e)
		{
			_refreshTimer.Start();
		}

		private void oclDeviceCombo_SelectionChangeCommitted(object sender, EventArgs e)
		{
			var selected = oclDeviceCombo.SelectedItem as Tuple<int, string>;

			if (selected != null)
			{
				oclDeviceIdx = selected.Item1;
				Init();
				RefreshImage();
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			SaveImage();
		}

		private void PalletButton_Click(object sender, EventArgs e)
		{
			ChoosePallet();
		}

		private void smoothingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			renderer.Smoothing = smoothingCheckBox.Checked;
			RefreshImage();
		}

		private void loadSetButton_Click(object sender, EventArgs e)
		{
			LoadSet();
		}

		private void saveSetButton_Click(object sender, EventArgs e)
		{
			SaveSet();
		}

		private void centerXTextBox_Leave(object sender, EventArgs e)
		{
			if (double.TryParse(centerXTextBox.Text.Trim(), out var x))
				center.X = x;
		}

		private void centerYTextBox_Leave(object sender, EventArgs e)
		{
			if (double.TryParse(centerYTextBox.Text.Trim(), out var y))
				center.Y = y;
		}

		private void radiusTextBox_Leave(object sender, EventArgs e)
		{
			if (double.TryParse(radiusTextBox.Text.Trim(), out var r))
				radius = r;
		}
	}
}