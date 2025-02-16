using System;
using System.Linq;
using System.Threading;

namespace Neomaster.Demos.Apps.Threads.AffinityParameterized
{
    internal class Program
    {
        static void Main()
        {
            var threads = Enum.GetValues(typeof(ThreadPriority))
                .Cast<ThreadPriority>()
                .OrderByDescending(tp => tp)
                .Select(tp =>
                {
                    var counter = new Counter(tp.ToString());
                    var thread = new Thread(counter.Run)
                    {
                        Priority = tp
                    };

                    return thread;
                })
                .ToArray();

            foreach (var th in threads)
            {
                th.Start();
            }

            // lowest
            threads.Last().Join();
            Console.ReadKey();

            // Output:
            //
            // [run_af_0001.bat]
            //
            // Thread     Highest, i = 1000000000
            // Thread AboveNormal, i = 0
            // Thread      Normal, i = 0
            // Thread BelowNormal, i = 0
            // Thread      Lowest, i = 0
            //
            // [run_af_1111.bat]
            // 
            // Thread      Highest, i=988866452
            // Thread       Normal, i=1000000000
            // Thread  AboveNormal, i=996749408
            // Thread  BelowNormal, i=3569215
            // Thread       Lowest, i=0
        }
    }
}
