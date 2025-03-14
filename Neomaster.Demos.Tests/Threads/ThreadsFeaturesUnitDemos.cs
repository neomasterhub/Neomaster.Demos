using System.Runtime.CompilerServices;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsFeaturesUnitDemos
{
  [Fact]
  public void SynchronizedMethod()
  {
    var obj = new SynchronizedMethodClass();
    var chars = string.Empty;
    const string expectedChars = "AAAAABBBBBCCCCCDDDDD";

    for (char c = 'A'; c <= 'D'; c++)
    {
      obj.Count(ref chars, 5, c);
    }

    Assert.Equal(expectedChars, chars);
  }

  public class SynchronizedMethodClass
  {
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Count(ref string chars, int number, char c)
    {
      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);

        chars += c;
      }
    }
  }
}
