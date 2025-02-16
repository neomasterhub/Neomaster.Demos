#pragma warning disable CS0618
using System;
using System.Threading;

namespace Neomaster.Demos.Apps.Threads.SuspendResume
{
    internal class Program
    {
        private static int _i = 0;
        private static readonly int _max = 3;
        private static readonly Thread _th1 = new Thread(() => Write('-', _th2));
        private static readonly Thread _th2 = new Thread(() => Write('|', _th1));

        private static void Write(char symbol, Thread next)
        {
            Thread.CurrentThread.Suspend();

            while (true)
            {
                Console.Write(symbol);
                Thread.Sleep(500);

                if (++_i == _max)
                {
                    _i = 0;
                    next.Resume();
                    Thread.CurrentThread.Suspend();
                }
            }
        }

        static void Main()
        {
            _th1.Start();
            _th2.Start();

            while (_th1.ThreadState != ThreadState.Suspended) ;

            _th1.Resume();

            // Output:
            // ---|||---|||---|||
        }
    }
}
