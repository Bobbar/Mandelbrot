using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Mandelbrot.Rendering
{
	/// <summary>
	/// Provides a disposable handle for a <see cref="Bitmap"/> image.
	/// 
	/// 
	/// </summary>
	/// <remarks>Wraps the <see cref="Bitmap.GetHbitmap"/> method.</remarks>
	public class BitmapHandle : IDisposable
	{
		public IntPtr Handle { get; private set; }

		public BitmapHandle(Bitmap bmp)
		{
			Handle = bmp.GetHbitmap();
		}

		public void Dispose()
		{
			DeleteObject(Handle);
		}

		[DllImport("gdi32.dll")]
		private static extern bool DeleteObject(IntPtr hObject);
	}
}
