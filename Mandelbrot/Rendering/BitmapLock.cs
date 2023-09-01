using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot.Rendering
{
	/// <summary>
	/// Provides a disposable lock for a <see cref="System.Drawing.Bitmap"/> image.
	/// 
	/// </summary>
	/// <remarks>Wraps the <see cref="Bitmap.LockBits(Rectangle, ImageLockMode, PixelFormat)"/> method.</remarks>
	public class BitmapLock : IDisposable
	{
		public Bitmap Bitmap { get; private set; }
		public BitmapData Data { get; private set; }

		public BitmapLock(Bitmap bmp)
		{
			Bitmap = bmp;
			Data = bmp.LockBits(new Rectangle(new Point(), bmp.Size), ImageLockMode.ReadWrite, bmp.PixelFormat);
		}

		public void Dispose()
		{
			Bitmap.UnlockBits(Data);
		}
	}
}
