using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsSyncUnitDemos
{
  private static readonly Random _random = new();
  private static readonly object _lock = new();

  [Fact]
  public void LockAddingToList()
  {
    const int count = 5;
    var numbers = new List<int>(count);
    var threads = Enumerable.Range(1, count)
      .Select(n => new Thread(() =>
      {
        lock (_lock)
        {
          var sleep = _random.Next(0, 9) * 100;
          Thread.Sleep(sleep);
          numbers.Add(n);
        }
      }))
      .ToArray();

    foreach (var th in threads)
    {
      th.Start();

      // Run threads sequentially.
      // Start() does not wait for the OS to finish creating a physical thread!
      Thread.Sleep(20);
    }

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.Equal(Enumerable.Range(1, count), numbers);
  }

  [Fact]
  public void MonitorAddingToList()
  {
    const int count = 5;
    var numbers = new List<int>(count);
    var threads = Enumerable.Range(1, count)
      .Select(n => new Thread(() =>
      {
        Monitor.Enter(_lock);

        try
        {
          var sleep = _random.Next(0, 9) * 100;
          Thread.Sleep(sleep);
          numbers.Add(n);
        }
        finally
        {
          Monitor.Exit(_lock);
        }
      }))
      .ToArray();

    foreach (var th in threads)
    {
      th.Start();

      // Run threads sequentially.
      // Start() does not wait for the OS to finish creating a physical thread!
      Thread.Sleep(20);
    }

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.Equal(Enumerable.Range(1, count), numbers);
  }

  [Fact]
  public void WaitQueue()
  {
    const string expected = "123";
    var actual = string.Empty;
    var e = new AutoResetEvent(false);
    var th1 = new Thread(() =>
    {
      actual += "1";
      e.WaitOne(); // th1 is placed in Wait Queue.

      // The event occurred.
      // th1 is placed in Ready Queue.
      actual += "3";
    });
    var th2 = new Thread(() =>
    {
      Thread.Sleep(100);
      actual += "2";
      e.Set();
    });

    th1.Start();
    th2.Start();
    th1.Join();

    Assert.Equal(expected, actual);
  }
}
