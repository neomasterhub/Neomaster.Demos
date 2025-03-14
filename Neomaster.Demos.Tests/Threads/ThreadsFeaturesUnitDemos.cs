using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsFeaturesUnitDemos
{
  public class SynchronizedMethodClass
  {
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void CountStatic(ConcurrentQueue<char> chars, int number, char c)
    {
      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);
        chars.Enqueue(c);
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Count(ConcurrentQueue<char> chars, int number, char c)
    {
      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);
        chars.Enqueue(c);
      }
    }
  }
}
