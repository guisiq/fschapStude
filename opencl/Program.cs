using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloo;

namespace OpenCL_C_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get OpenCL platforms
            ComputePlatform[] platforms = ComputePlatform.GetPlatforms();
            // Create OpenCL context
            ComputeContextPropertyList properties = new ComputeContextPropertyList(platforms[0]);
            ComputeContext context = new ComputeContext(ComputeDeviceTypes.Gpu, properties, null, IntPtr.Zero);
            // Get OpenCL devices
            ComputeDevice[] devices = context.Devices;
            // Create the OpenCL program
            ComputeProgram program = new ComputeProgram(context, @"
            __kernel void vectorAdd(__global const float *a, 
                                    __global const float *b,
                                    __global float *c)
            {
                int i = get_global_id(0);
                c[i] = a[i] + b[i];
            }
            ");
            // Compile the OpenCL program
            program.Build(null, null, null, IntPtr.Zero);
            // Create OpenCL kernel
            ComputeKernel kernel = program.CreateKernel("vectorAdd");
            // Create OpenCL command queue
            ComputeCommandQueue queue = new ComputeCommandQueue(context, devices[0], ComputeCommandQueueFlags.None);
            // Create OpenCL buffers
            int size = 1024;
            ComputeBuffer<float> a = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly, size);
            ComputeBuffer<float> b = new ComputeBuffer<float>(context, ComputeMemoryFlags.ReadOnly, size);
            ComputeBuffer<float> c = new ComputeBuffer<float>(context, ComputeMemoryFlags.WriteOnly, size);
            // Set OpenCL kernel parameters
            kernel.SetMemoryArgument(0, a);
            kernel.SetMemoryArgument(1, b);
            kernel.SetMemoryArgument(2, c);
            // Create host arrays
            float[] aData = new float[size];
            float[] bData = new float[size];
            // Initialize host arrays
            for (int i = 0; i < size; i++)
            {
                aData[i] = i;
                bData[i] = i;
            }
            // Write host data to OpenCL buffers
            queue.WriteToBuffer(aData, a, true, null);
            queue.WriteToBuffer(bData, b, true, null);
            // Execute OpenCL kernel
            queue.Execute(kernel, null, new long[] { size }, null, null);
            // Read data from OpenCL buffers
            float[] cData = new float[size];
            queue.ReadFromBuffer(c, ref cData, true, null);
            // Print result
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(cData[i]);
            }
            // Dispose OpenCL objects
            queue.Dispose();
            kernel.Dispose();
            program.Dispose();
            a.Dispose();
            b.Dispose();
            c.Dispose();
            context.Dispose();
        }
    }
}