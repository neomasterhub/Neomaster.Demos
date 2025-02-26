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
    const string expected = "*AAAAABBBBB";
    const int charRepetitions = 5;
    var chars = new char[] { 'A', 'B' };
    var actual = string.Empty;
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
    const string expected = "*AAAAA";
    const int charRepetitions = 5;
    var chars = new char[] { 'A', 'B' };
    var actual = string.Empty;
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
    const int noiseSignalNumber = 3;
    var tickTockPairCount = 2;
    const string expected = "1___2___1___2___";
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

  [Fact]
  public void MonitorWaitWithTimeoutAsSleep()
  {
    const string expected = "(Running){2,}(WaitSleepJoin){2,}(Running){2,}";

    var states = new List<string>();
    Thread stateLogger = null;

    static void LongOp()
    {
      var j = int.MaxValue;
      while (j-- > 0)
      {
      }
    }

    var th = new Thread(() =>
    {
      stateLogger.Start();

      LongOp();

      lock (_lock)
      {
        Monitor.Wait(_lock, 500);
      }

      LongOp();
    });

    stateLogger = new Thread(() =>
    {
      while (th.IsAlive)
      {
        states.Add(th.ThreadState.ToString());
        Thread.Sleep(100);
      }
    });

    th.Start();
    th.Join();

    var actual = string.Concat(states);
    Assert.Matches(expected, actual);
  }

  [Fact]
  public void SpinLockVsLock()
  {
    var gotLock = false;
    var sl = new SpinLock(false);
    var lSw = new Stopwatch();
    var slSw = new Stopwatch();
    const int lockNumber = 10;

    var lTh = new Thread(() =>
    {
      lSw.Start();

      for (var i = 0; i < lockNumber; i++)
      {
        lock (_lock)
        {
        }
      }

      lSw.Stop();
    });

    var slTh = new Thread(() =>
    {
      slSw.Start();

      for (var i = 0; i < lockNumber; i++)
      {
        gotLock = false;
        sl.Enter(ref gotLock);
        sl.Exit();
      }

      slSw.Stop();
    });

    lTh.Start();
    lTh.Join();
    slTh.Start();
    slTh.Join();

    var lMs = lSw.Elapsed.TotalMilliseconds;   // ~ 0.15
    var slMs = slSw.Elapsed.TotalMilliseconds; // ~ 0.02

    Assert.True(slMs < lMs);
  }

  [Fact]
  public void SpinLockAsSleep()
  {
    const string expected = "(_Running){2,}(#Running){2,}(_Running){2,}";

    var states = new List<string>();
    Thread stateLogger = null;

    var sl = new SpinLock(false);
    var gotLock = false;

    void LongOp()
    {
      var j = int.MaxValue;
      while (j-- > 0)
      {
      }
    }

    void SpinLockedLongOp()
    {
      gotLock = false;
      try
      {
        sl.Enter(ref gotLock);
        LongOp();
      }
      finally
      {
        if (gotLock)
        {
          gotLock = false;
          sl.Exit();
        }
      }
    }

    var th = new Thread(() =>
    {
      stateLogger.Start();

      LongOp();
      SpinLockedLongOp();
      LongOp();
    });

    stateLogger = new Thread(() =>
    {
      while (th.IsAlive)
      {
        states.Add((gotLock ? "#" : "_") + th.ThreadState.ToString());
        Thread.Sleep(100);
      }
    });

    th.Start();
    th.Join();

    var actual = string.Concat(states);
    Assert.Matches(expected, actual);
  }

  [Fact]
  public void SpinLockForFastLogging()
  {
    var sl = new SpinLock(false);
    var log = new List<int>();
    var logger = () =>
    {
      var gotLock = false;
      try
      {
        sl.Enter(ref gotLock);
        log.Add(Thread.CurrentThread.ManagedThreadId);
      }
      finally
      {
        if (gotLock)
        {
          sl.Exit();
        }
      }
    };
    var th = new Thread(() => logger());
    var expectedLog = new List<int> { th.ManagedThreadId };

    th.Start();
    th.Join();

    Assert.Equal(expectedLog, log);
  }

  [Theory]
  [InlineData(true, true)]
  [InlineData(false, false)]
  public void SpinLockThrowingSynchronizationLockException(
    bool enableThreadOwnerTracking,
    bool exceptionWasThrown)
  {
    SynchronizationLockException ex = null;
    var gotLock = false;
    var sl = new SpinLock(enableThreadOwnerTracking);
    var th1 = new Thread(() =>
    {
      sl.Enter(ref gotLock);
    });
    var th2 = new Thread(() =>
    {
      try
      {
        sl.Exit();
      }
      catch (SynchronizationLockException e)
      {
        ex = e;
      }
    });

    th1.Start();
    th1.Join();
    th2.Start();
    th2.Join();

    Assert.Equal(exceptionWasThrown, ex != null);
  }
}
