using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Cloo;
using System.Drawing;
using System.Security.Cryptography.Xml;
using System.Numerics;
using Cloo.Bindings;

namespace Mandelbrot
{
	public class OpenCLCompute : IDisposable
	{
		private ComputeContext _context;
		private ComputeCommandQueue _queue;
		private ComputeDevice _device;
		private ComputeProgram _program;

		private ComputeBuffer<clColor> _gpuPixels;
		private ComputeBuffer<clColor> _gpuPallet;
        private ComputeBuffer<Complex> _gpuHPPoints;

        private ComputeKernel _computePixels;
		private ComputeEventList _evts = null;

		private const int _threadsPerBlock = 8;//32;
		private int _gpuIndex;
        private Size _dims;

        private bool _profile = false;

        public OpenCLCompute(int gpuIndex, Size dims)
        {
            _gpuIndex = gpuIndex;
            _dims = dims;

            Init();
            InitBuffers(dims);
        }

        private void Init()
		{
			var devices = GetDevices();

			_device = devices[_gpuIndex];

			var platform = _device.Platform;

			_context = new ComputeContext(new[] { _device }, new ComputeContextPropertyList(platform), null, IntPtr.Zero);
			var flags = _profile ? ComputeCommandQueueFlags.Profiling : ComputeCommandQueueFlags.None;
			_queue = new ComputeCommandQueue(_context, _device, flags);

			if (_profile)
				_evts = new ComputeEventList();

			var kernelPath = $@"{Environment.CurrentDirectory}\OpenCL\OCLKernels.cl";
			string clSource;

			using (StreamReader streamReader = new StreamReader(kernelPath))
			{
				clSource = streamReader.ReadToEnd();
			}

			_program = new ComputeProgram(_context, clSource);

			try
			{
				string options = $@"-cl-std=CL2.0";
				_program.Build(new[] { _device }, options, null, IntPtr.Zero);
			}
			catch (BuildProgramFailureComputeException ex)
			{
				string buildLog = _program.GetBuildLog(_device);
				File.WriteAllText("build_error.txt", buildLog);
				Debug.WriteLine(buildLog);
				throw;
			}

			_computePixels = _program.CreateKernel("ComputePixels");
		}

        private void InitBuffers(Size dims)
        {
            int len = (dims.Width * dims.Height);

            _gpuPixels = new ComputeBuffer<clColor>(_context, ComputeMemoryFlags.ReadWrite, len);
            _gpuPallet = new ComputeBuffer<clColor>(_context, ComputeMemoryFlags.ReadOnly, 1);
            _gpuHPPoints = new ComputeBuffer<Complex>(_context, ComputeMemoryFlags.ReadOnly, 1);
        }

        public static List<ComputeDevice> GetDevices()
		{
			var devices = new List<ComputeDevice>();

			foreach (var platform in ComputePlatform.Platforms)
			{
				foreach (var device in platform.Devices)
				{
					devices.Add(device);
				}
			}

			return devices;
		}

		private int PadSize(int len)
		{
			if (len < _threadsPerBlock)
				return _threadsPerBlock;

			int mod = len % _threadsPerBlock;
			int padLen = len - mod + _threadsPerBlock;
			return padLen;
		}

		public void UpdatePallet(List<Color> pallet)
		{
			var cvt = new clColor[pallet.Count];
			for (int i = 0; i < cvt.Length; i++)
			{
				var c = pallet[i];
				cvt[i] = new clColor(c);
			}

			if (_gpuPallet.Count != cvt.Length)
			{
				_gpuPallet?.Dispose();
				_gpuPallet = new ComputeBuffer<clColor>(_context, ComputeMemoryFlags.ReadOnly, cvt.Length);
			}

			_queue.WriteToBuffer(cvt, _gpuPallet, true, null);
		}

        private void UpdateHPPoints(List<Complex> pnts)
        {
            if (_gpuHPPoints.Count != pnts.Count)
            {
                _gpuHPPoints?.Dispose();
                _gpuHPPoints = new ComputeBuffer<Complex>(_context, ComputeMemoryFlags.ReadOnly, pnts.Count);
            }

            _queue.WriteToBuffer(pnts.ToArray(), _gpuHPPoints, true, _evts);
        }

        public void ComputePixels(ref IntPtr pixels, List<Complex> itVals, double radius, List<Color> pallet)
		{
			if (_profile)
				_evts = new ComputeEventList();

			UpdatePallet(pallet);
			UpdateHPPoints(itVals);

            int len = (_dims.Width * _dims.Height);

            int argI = 0;
			_computePixels.SetMemoryArgument(argI++, _gpuPixels);
			_computePixels.SetValueArgument(argI++, _dims);
			_computePixels.SetMemoryArgument(argI++, _gpuPallet);
			_computePixels.SetValueArgument(argI++, (int)_gpuPallet.Count);
			_computePixels.SetMemoryArgument(argI++, _gpuHPPoints);
			_computePixels.SetValueArgument(argI++, (int)_gpuHPPoints.Count);
			_computePixels.SetValueArgument(argI++, radius);

            _queue.Execute(_computePixels, null, new long[] { _dims.Width, _dims.Height }, new long[] { _threadsPerBlock, _threadsPerBlock }, _evts);

            _queue.Read(_gpuPixels, true, 0, len, pixels, _evts);

			if (_profile)
			{
				_queue.Finish();

				ulong tot = 0;
				for (int i = 0; i < _evts.Count; i++)
				{
					var evt = _evts[i];
					var elap = evt.FinishTime - evt.StartTime;
					tot += elap;

					var elapMS = (float)elap / 1000000.0f;
					var idxStr = i.ToString("00");
					Debug.WriteLine($"[{idxStr}] ({evt.Type.ToString()}) - Elap: {elap} ns  {elapMS} ms");
					evt.Dispose();
				}

				Debug.WriteLine("Tot: {0} ns  {1} ms", tot, (tot / 1000000.0f));
				Debug.WriteLine("---");

			}

			_queue.Finish();
		}

		private T[] ReadBuffer<T>(ComputeBuffer<T> buffer, bool blocking = false) where T : struct
		{
			T[] buf = new T[buffer.Count];

			_queue.ReadFromBuffer(buffer, ref buf, blocking, null);
			if (blocking) _queue.Finish(); // This is probably redundant...

			return buf;
		}

		public void Dispose()
		{
			_context?.Dispose();
			_queue?.Dispose();
			_program?.Dispose();
			_gpuPallet?.Dispose();
			_gpuPixels?.Dispose();
			_gpuHPPoints?.Dispose();
			_computePixels?.Dispose();
			_evts?.Clear();
		}
	}
}
