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

namespace Mandelbrot
{
    public class OpenCLCompute : IDisposable
    {

        private ComputeContext _context;
        private ComputeCommandQueue _queue;
        private ComputeDevice _device;
        private ComputeProgram _program;

        private ComputeBuffer<byte> _gpuPixels;
        private ComputeBuffer<clColor> _gpuPallet;

        private ComputeKernel _computePixels;

        private const int _threadsPerBlock = 32;
        private int _gpuIndex;
        private int2 _dims;

        public OpenCLCompute(int gpuIndex, int2 dims)
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
            _queue = new ComputeCommandQueue(_context, _device, ComputeCommandQueueFlags.None);

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

        private void InitBuffers(int2 dims)
        {
            int len = (dims.X * dims.Y) * 4;

            _gpuPixels = new ComputeBuffer<byte>(_context, ComputeMemoryFlags.ReadWrite, len);
        }

        private List<ComputeDevice> GetDevices()
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
                cvt[i] = new clColor() { R = c.R, G = c.G, B = c.B };
            }

            _gpuPallet?.Dispose();
            _gpuPallet = new ComputeBuffer<clColor>(_context, ComputeMemoryFlags.ReadOnly, cvt.Length);
            _queue.WriteToBuffer(cvt, _gpuPallet, true, null);
        }

        public void ComputePixels(ref IntPtr pixels, int maxIters, PointD xMinMax, PointD yMinMax, Point fieldSize, int colorScale, float cValue)
        {
            int len = (_dims.X * _dims.Y) * 4;
            int2 padDims = new int2() { X = PadSize(_dims.X), Y = PadSize(_dims.Y) };

            _computePixels.SetMemoryArgument(0, _gpuPixels);
            _computePixels.SetValueArgument(1, _dims);
            _computePixels.SetValueArgument(2, maxIters);
            _computePixels.SetValueArgument(3, xMinMax);
            _computePixels.SetValueArgument(4, yMinMax);
            _computePixels.SetValueArgument(5, fieldSize);
            _computePixels.SetValueArgument(6, colorScale);
            _computePixels.SetValueArgument(7, cValue);
            _computePixels.SetMemoryArgument(8, _gpuPallet);
            _computePixels.SetValueArgument(9, (int)_gpuPallet.Count);

            _queue.Execute(_computePixels, null, new long[] { padDims.X, padDims.Y }, new long[] { _threadsPerBlock, _threadsPerBlock }, null);

            _queue.Read(_gpuPixels, true, 0, len, pixels, null); 

            _queue.Finish();
        }

        private void ReadBuffer(ComputeBuffer<byte> source, ref IntPtr dest, int len)
        {
            Cloo.Bindings.CL10.EnqueueReadBuffer(
                  _queue.Handle,
                  source.Handle,
                  true,
                  new IntPtr(0 * 4),
                  new IntPtr(len * 4),
                  dest,
                  0,
                  null,
                  null);
        }

        public void Dispose()
        {
            _context?.Dispose();
            _queue?.Dispose();
            _program?.Dispose();
            _gpuPallet?.Dispose();
            _gpuPixels?.Dispose();
            _computePixels?.Dispose();
        }
    }
}
