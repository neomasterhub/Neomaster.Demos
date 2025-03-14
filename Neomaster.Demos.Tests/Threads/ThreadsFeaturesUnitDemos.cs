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

  [Fact]
  public void SynchronizedInstanceMethodOfDifferentObjects()
  {
    var obj1 = new SynchronizedMethodClass();
    var obj2 = new SynchronizedMethodClass();
    var obj1HashCode = 0;
    var obj2HashCode = 0;
    var chars = new ConcurrentQueue<char>();
    const string expectedChars1String = "AAAAABBBBBCCCCCDDDDD";
    const string expectedChars2String = "11111222223333344444";
    const int charNumber = 5;
    var th1 = new Thread(() =>
    {
      for (char c = 'A'; c <= 'D'; c++)
      {
        obj1.Count(chars, charNumber, c, out obj1HashCode);
      }
    });
    var th2 = new Thread(() =>
    {
      for (char c = '1'; c <= '4'; c++)
      {
        obj2.Count(chars, charNumber, c, out obj2HashCode);
      }
    });

    th1.Start();
    th2.Start();
    th1.Join();
    th2.Join();

    var charsString = string.Concat(chars);
    var uniqueChars = chars.Distinct();

    // Demonstrates that the object being locked is "this".
    foreach (var uc in uniqueChars)
    {
      var ucSeq = new string(uc, charNumber);
      Assert.DoesNotContain(charsString, ucSeq);
    }

    var chars1String = string.Concat(chars.Where(c => c >= 'A'));
    var chars2String = string.Concat(chars.Where(c => c < 'A'));
    Assert.Equal(expectedChars1String, chars1String);
    Assert.Equal(expectedChars2String, chars2String);
    Assert.NotEqual(obj1HashCode, obj2HashCode);
  }

  [Fact]
  public void SynchronizedStaticMethodOfDifferentObjects()
  {
    var chars = new ConcurrentQueue<char>();
    const string expectedChars1String = "AAAAABBBBBCCCCCDDDDD";
    const string expectedChars2String = "11111222223333344444";
    const int charNumber = 5;
    var th1 = new Thread(() =>
    {
      for (char c = 'A'; c <= 'D'; c++)
      {
        SynchronizedMethodClass.StaticCount(chars, charNumber, c);
      }
    });
    var th2 = new Thread(() =>
    {
      for (char c = '1'; c <= '4'; c++)
      {
        SynchronizedMethodClass.StaticCount(chars, charNumber, c);
      }
    });

    th1.Start();
    th2.Start();
    th1.Join();
    th2.Join();

    var charsString = string.Concat(chars);
    var uniqueChars = chars.Distinct();

    // Demonstrates that the object being locked is an instance of "typeof(SynchronizedMethodClass)".
    foreach (var uc in uniqueChars)
    {
      var ucSeq = new string(uc, charNumber);
      Assert.DoesNotContain(charsString, ucSeq);
    }

    var chars1String = string.Concat(chars.Where(c => c >= 'A'));
    var chars2String = string.Concat(chars.Where(c => c < 'A'));
    Assert.Equal(expectedChars1String, chars1String);
    Assert.Equal(expectedChars2String, chars2String);
  }

  public class SynchronizedMethodClass
  {
    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void StaticCount(ConcurrentQueue<char> chars, int number, char c)
    {
      for (var i = 0; i < number; i++)
      {
        Thread.Sleep(50);
        chars.Enqueue(c);
      }
    }

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
