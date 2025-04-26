using System;
using System.Diagnostics;
using System.Threading;

namespace Neomaster.Demos.Apps.Threads.AffinityProgrammed
{
    internal class Program
    {
        static void Main()
        {
            Thread.Sleep(TimeSpan.FromSeconds(15)); // to see the peak in the CPU core graph

            // CPU3 | CPU2 | CPU1 | CPU0
            // off  | on   | on   | off   <-> 0110 = 6(dec)
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)6;

            while (true) ;
        }
    }
}
