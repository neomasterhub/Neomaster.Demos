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

  [Fact(DisplayName = "Synchronized method: instance in different threads")]
  public void SynchronizedMethodInstanceInDifferentThreads()
  {
    var obj1 = new SynchronizedMethodClass();
    var obj2 = new SynchronizedMethodClass();
    var chars = new ConcurrentQueue<char>();
    const string expectedChars1String = "AAAAABBBBBCCCCCDDDDD";
    const string expectedChars2String = "11111222223333344444";
    const int charNumber = 5;
    var th1 = new Thread(() =>
    {
      for (char c = 'A'; c <= 'D'; c++)
      {
        obj1.Count(chars, charNumber, c);
      }
    });
    var th2 = new Thread(() =>
    {
      for (char c = '1'; c <= '4'; c++)
      {
        obj2.Count(chars, charNumber, c);
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
      Assert.DoesNotContain(ucSeq, charsString);
    }

    var chars1String = string.Concat(chars.Where(c => c >= 'A'));
    var chars2String = string.Concat(chars.Where(c => c < 'A'));
    Assert.Equal(expectedChars1String, chars1String);
    Assert.Equal(expectedChars2String, chars2String);
  }

  [Fact(DisplayName = "Synchronized method: static in different threads")]
  public void SynchronizedMethodStaticInDifferentThreads()
  {
    var chars = new ConcurrentQueue<char>();
    const string expectedChars1String = "AAAAABBBBBCCCCCDDDDD";
    const string expectedChars2String = "11111222223333344444";
    const int charNumber = 5;
    var th1 = new Thread(() =>
    {
      for (char c = 'A'; c <= 'D'; c++)
      {
        SynchronizedMethodClass.CountStatic(chars, charNumber, c);
      }
    });
    var th2 = new Thread(() =>
    {
      for (char c = '1'; c <= '4'; c++)
      {
        SynchronizedMethodClass.CountStatic(chars, charNumber, c);
      }
    });

    th1.Start();
    th2.Start();
    th1.Join();
    th2.Join();

    var charsString = string.Concat(chars);
    var uniqueChars = chars.Distinct();

    // Demonstrates that the object being locked is the instance of "typeof(SynchronizedMethodClass)".
    foreach (var uc in uniqueChars)
    {
      var ucSeq = new string(uc, charNumber);
      Assert.Contains(ucSeq, charsString);
    }

    var chars1String = string.Concat(chars.Where(c => c >= 'A'));
    var chars2String = string.Concat(chars.Where(c => c < 'A'));
    Assert.Equal(expectedChars1String, chars1String);
    Assert.Equal(expectedChars2String, chars2String);
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
