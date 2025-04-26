using System;

namespace Neomaster.Demos.Apps.Threads.AffinityParameterized
{
    internal class Counter
    {
        private static bool _stop;
        private readonly string _name;

        public Counter(string name)
        {
            _name = name;
        }

        public void Run()
        {
            long i;

            for (i = 0; !_stop && i < 1e9; i++) ;

            _stop = true;

            Console.WriteLine("Thread {0,12}, i={1}", _name, i);
        }
    }
}
