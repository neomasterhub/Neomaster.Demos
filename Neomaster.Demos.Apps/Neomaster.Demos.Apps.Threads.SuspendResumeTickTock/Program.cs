#pragma warning disable CS0618
using System;
using System.Threading;

namespace Neomaster.Demos.Apps.Threads.SuspendResumeTickTock
{
    internal class Program
    {
        private static void Beep(int frequency, Thread next)
        {
            Thread.CurrentThread.Suspend();

            while (true)
            {
                Console.Beep(frequency, 500);
                Thread.Sleep(1000);
                next.Resume();
                Thread.CurrentThread.Suspend();
            }
        }

        static void Main()
        {
            Thread tick = null;
            Thread tock = null;

            tick = new Thread(() => Beep(500, tock));
            tock = new Thread(() => Beep(1000, tick));

            tick.Start();
            tock.Start();

            while (tick.ThreadState != ThreadState.Suspended) ;

            tick.Resume();
        }
    }
}
