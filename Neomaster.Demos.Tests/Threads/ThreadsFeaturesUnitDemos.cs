using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsFeaturesUnitDemos
{
  [Fact]
  public void SynchronizedMethod()
  {
    var obj = new SynchronizedMethodClass();
    var chars = new ConcurrentQueue<char>();
    const string expectedCharsString = "AAAAABBBBBCCCCCDDDDD";

    for (char c = 'A'; c <= 'D'; c++)
    {
      obj.Count(chars, 5, c, out _);
    }

    var charsString = string.Concat(chars);
    Assert.Equal(expectedCharsString, charsString);
  }

  public class SynchronizedMethodClass
  {
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Count(ConcurrentQueue<char> chars, int number, char c, out int thisHashCode)
    {
      thisHashCode = this.GetHashCode();

      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);
        chars.Enqueue(c);
      }
    }
  }
}
