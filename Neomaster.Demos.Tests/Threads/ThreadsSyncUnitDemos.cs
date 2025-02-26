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

  [Fact]
  public void MonitorPulseAll()
  {
    const int charRepetitions = 5;
    var chars = new char[] { 'A', 'B' };
    var actual = string.Empty;
    var expected = "*" + string.Concat(chars.Select(c => new string(c, charRepetitions)));
    var wqThreads = chars
      .Select(c => new Thread(() =>
      {
        lock (_lock)
        {
          Monitor.Wait(_lock);

          for (var i = 0; i < charRepetitions; i++)
          {
            actual += c;
          }
        }
      }))
      .ToArray();
    var paThread = new Thread(() =>
    {
      lock (_lock)
      {
        actual += "*";

        Monitor.PulseAll(_lock);
      }
    });

    foreach (var wqt in wqThreads)
    {
      wqt.Start();
      Thread.Sleep(20);
    }

    paThread.Start();

    foreach (var wqt in wqThreads)
    {
      wqt.Join();
    }

    Assert.Equal(expected, actual);
  }

  [Fact]
  public void MonitorPulse()
  {
    const int charRepetitions = 5;
    var chars = new char[] { 'A', 'B' };
    var actual = string.Empty;
    var expected = "*" + new string(chars[0], charRepetitions);
    var wqThreads = chars
      .Select(c => new Thread(() =>
      {
        lock (_lock)
        {
          Monitor.Wait(_lock);

          for (var i = 0; i < charRepetitions; i++)
          {
            actual += c;
          }
        }
      }))
      .ToArray();
    var paThread = new Thread(() =>
    {
      lock (_lock)
      {
        actual += "*";

        Monitor.Pulse(_lock);
      }
    });

    foreach (var wqt in wqThreads)
    {
      wqt.Start();
      Thread.Sleep(20);
    }

    paThread.Start();

    var allThreadsAreExecuted = true;
    foreach (var wqt in wqThreads)
    {
      allThreadsAreExecuted &= wqt.Join(1000);
    }

    Assert.Equal(expected, actual);
    Assert.False(allThreadsAreExecuted);
  }

  [Fact]
  public void MonitorPulseWaitTickTock()
  {
    const string tickSignal = "1";
    const string tockSignal = "2";
    const string noiseSignal = "_";
    var noiseSignalNumber = 3;
    var tickTockPairCount = 2;
    var noiseSeq = string.Concat(Enumerable.Repeat(noiseSignal, noiseSignalNumber));
    var expectedPair = tickSignal + noiseSeq + tockSignal + noiseSeq;
    var expected = string.Concat(Enumerable.Repeat(expectedPair, tickTockPairCount));
    var actual = string.Empty;

    void Beep(object signal)
    {
      lock (_lock)
      {
        while (tickTockPairCount > 0)
        {
          var s = (string)signal;
          actual += s;

          Monitor.Pulse(_lock);

          for (var i = 0; i < noiseSignalNumber; i++)
          {
            Thread.Sleep(100);
            actual += noiseSignal;
          }

          if (s == tockSignal)
          {
            tickTockPairCount--;

            if (tickTockPairCount == 0)
            {
              return;
            }
          }

          Monitor.Wait(_lock);
        }
      }
    }

    var tickTh = new Thread(Beep);
    var tockTh = new Thread(Beep);

    tickTh.Start(tickSignal);

    while (tickTh.ThreadState != ThreadState.WaitSleepJoin)
    {
    }

    tockTh.Start(tockSignal);

    tickTh.Join();
    tockTh.Join();

    Assert.Equal(expected, actual);
  }
}
