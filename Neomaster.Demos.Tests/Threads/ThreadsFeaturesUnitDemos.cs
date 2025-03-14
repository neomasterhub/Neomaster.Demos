using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsFeaturesUnitDemos
{
  [Fact(DisplayName = "Synchronized method: instance")]
  public void SynchronizedMethodInstance()
  {
    var obj = new SynchronizedMethodClass();
    var chars = new ConcurrentQueue<char>();
    const string expectedCharsString = "AAAAABBBBBCCCCCDDDDD";

    for (char c = 'A'; c <= 'D'; c++)
    {
      obj.Count(chars, 5, c);
    }

    var charsString = string.Concat(chars);
    Assert.Equal(expectedCharsString, charsString);
  }

  [Fact(DisplayName = "Synchronized method: static")]
  public void SynchronizedMethodStatic()
  {
    var chars = new ConcurrentQueue<char>();
    const string expectedCharsString = "AAAAABBBBBCCCCCDDDDD";

    for (char c = 'A'; c <= 'D'; c++)
    {
      SynchronizedMethodClass.CountStatic(chars, 5, c);
    }

    var charsString = string.Concat(chars);
    Assert.Equal(expectedCharsString, charsString);
  }

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
