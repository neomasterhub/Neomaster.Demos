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
      obj.Count(ref chars, 5, c, out _);
    }

    Assert.Equal(expectedChars, chars);
  }

  [Fact]
  public void SynchronizedObjects()
  {
    var obj1 = new SynchronizedMethodClass();
    var obj2 = new SynchronizedMethodClass();
    var obj1HashCode = 0;
    var obj2HashCode = 0;
    var chars = string.Empty;
    const string expectedChars1 = "AAAAABBBBBCCCCCDDDDD";
    const string expectedChars2 = "11111222223333344444";

    var d = 'A' - '1';
    for (char c = 'A'; c <= 'D'; c++)
    {
      obj1.Count(ref chars, 5, c, out obj1HashCode);
      obj2.Count(ref chars, 5, (char)(c - d), out obj2HashCode);
    }

    var chars1 = string.Concat(chars.Where(c => c >= 'A'));
    var chars2 = string.Concat(chars.Where(c => c < 'A'));
    Assert.Equal(expectedChars1, chars1);
    Assert.Equal(expectedChars2, chars2);
    Assert.NotEqual(obj1HashCode, obj2HashCode);
  }

  public class SynchronizedMethodClass
  {
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Count(ref string chars, int number, char c, out int thisHashCode)
    {
      thisHashCode = this.GetHashCode();

      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);

        chars += c;
      }
    }
  }
}
