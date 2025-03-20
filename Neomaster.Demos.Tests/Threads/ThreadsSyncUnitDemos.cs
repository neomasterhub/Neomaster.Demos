using System.Collections.Concurrent;
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
    const string expected = "(!AAAAABBBBB)|(!BBBBBAAAAA)";
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
        actual += "!";

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

    Assert.Matches(expected, actual);
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

  [Fact]
  public void ThreadYieldFastWhileVsNormalWhile()
  {
    System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = 1;

    var ready = false;
    var sw = new Stopwatch();
    var yieldResults = new List<bool>();

    void Produce()
    {
      ready = false;
      Thread.Sleep(20);
      ready = true;
    }

    void Consume()
    {
      while (!ready)
      {
      }
    }

    void ConsumeYield()
    {
      while (!ready)
      {
        yieldResults.Add(Thread.Yield());
      }
    }

    TimeSpan Measure()
    {
      var producer1 = new Thread(Produce);
      var producer2 = new Thread(Produce);
      var consumer1 = new Thread(Consume);
      var consumer2 = new Thread(Consume);

      sw.Start();

      producer1.Start();
      producer2.Start();
      consumer1.Start();
      consumer2.Start();

      producer1.Join();
      producer2.Join();
      consumer1.Join();
      consumer2.Join();

      sw.Stop();

      return sw.Elapsed;
    }

    TimeSpan MeasureYield()
    {
      var producer1 = new Thread(Produce);
      var producer2 = new Thread(Produce);
      var consumerYield1 = new Thread(ConsumeYield);
      var consumerYield2 = new Thread(ConsumeYield);

      sw.Restart();

      producer1.Start();
      producer2.Start();
      consumerYield1.Start();
      consumerYield2.Start();

      producer1.Join();
      producer2.Join();
      consumerYield1.Join();
      consumerYield2.Join();

      sw.Stop();

      return sw.Elapsed;
    }

    var time = Measure().TotalMilliseconds; // 80
    var timeYield = MeasureYield().TotalMilliseconds; // 40

    Assert.True(timeYield < time);
    Assert.True(yieldResults[0]);
  }

  [Fact]
  public void ThreadSpinWaitFastWhileVsNormalWhile()
  {
    System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = 1;

    var ready = false;
    var sw = new Stopwatch();

    void Produce()
    {
      ready = false;
      Thread.Sleep(20);
      ready = true;
    }

    void Consume()
    {
      while (!ready)
      {
      }
    }

    void ConsumeYield()
    {
      while (!ready)
      {
        Thread.SpinWait(20_000);
      }
    }

    TimeSpan Measure()
    {
      var producer1 = new Thread(Produce);
      var producer2 = new Thread(Produce);
      var consumer1 = new Thread(Consume);
      var consumer2 = new Thread(Consume);

      sw.Start();

      producer1.Start();
      producer2.Start();
      consumer1.Start();
      consumer2.Start();

      producer1.Join();
      producer2.Join();
      consumer1.Join();
      consumer2.Join();

      sw.Stop();

      return sw.Elapsed;
    }

    TimeSpan MeasureYield()
    {
      var producer1 = new Thread(Produce);
      var producer2 = new Thread(Produce);
      var consumerYield1 = new Thread(ConsumeYield);
      var consumerYield2 = new Thread(ConsumeYield);

      sw.Restart();

      producer1.Start();
      producer2.Start();
      consumerYield1.Start();
      consumerYield2.Start();

      producer1.Join();
      producer2.Join();
      consumerYield1.Join();
      consumerYield2.Join();

      sw.Stop();

      return sw.Elapsed;
    }

    var time = Measure().TotalMilliseconds; // 90
    var timeYield = MeasureYield().TotalMilliseconds; // 60
    var dt = Math.Abs(time - timeYield);

    if (dt > 20) // SpinWait() is too CPU specific...
    {
      Assert.True(timeYield < time);
    }
  }

  [Fact]
  public void SpinWaitSpinOnce()
  {
    var sw = new SpinWait();
    var iterations = new Dictionary<int, bool>();

    for (int i = 0; i < 15; i++)
    {
      sw.SpinOnce();
      iterations.Add(i, sw.NextSpinWillYield);
    }

    Assert.Equal(sw.Count, iterations.Count);
    Assert.Equal([false, true], iterations.Values.Distinct());
  }

  [Fact]
  public void SpinWaitSpinUntil()
  {
    var ready = false;
    var th = new Thread(() =>
    {
      Thread.Sleep(30);
      ready = true;
    });

    th.Start();

    var result1 = SpinWait.SpinUntil(() => ready, 10);
    var result2 = SpinWait.SpinUntil(() => ready, 50);

    Assert.False(result1);
    Assert.True(result2);
  }

  [Fact]
  public void SemaphoreSignalPairs()
  {
    const int threadSignalsNumber = 10;
    var signals = new ConcurrentQueue<int>();
    var sm = new Semaphore(2, 2);
    var threads = Enumerable.Range(1, 4)
      .Select(n => new Thread(() =>
      {
        sm.WaitOne();

        for (var i = 0; i < threadSignalsNumber; i++)
        {
          signals.Enqueue(n);
          Thread.Sleep(20);
        }

        sm.Release();
      }))
      .ToList();

    threads.ForEach(th =>
    {
      th.Start();
      Thread.Sleep(20);
    });
    threads.ForEach(th => th.Join());

    var cleanXYSeqLength = (threadSignalsNumber * 2) - 2;
    Assert.All(signals.Take(cleanXYSeqLength), s => Assert.True(s is 1 or 2));
    Assert.All(signals.TakeLast(cleanXYSeqLength), s => Assert.True(s is 3 or 4));
  }

  [Fact]
  public void SemaphoreSignalPairsReleaseMaxSlots()
  {
    const int threadSignalsNumber = 10;
    var signals = new ConcurrentQueue<int>();
    var sm = new Semaphore(2, 3);
    var threads = Enumerable.Range(1, 5)
      .Select(n => new Thread(() =>
      {
        sm.WaitOne();

        for (var i = 0; i < threadSignalsNumber; i++)
        {
          signals.Enqueue(n);
          Thread.Sleep(20);
        }
      }))
      .ToList();

    threads.ForEach(th =>
    {
      th.Start();
      Thread.Sleep(20);
    });

    var clean12SeqLength = threadSignalsNumber * 2;
    SpinWait.SpinUntil(() => signals.Count == clean12SeqLength);
    sm.Release(3);

    threads.ForEach(th => th.Join());

    Assert.All(signals.Take(clean12SeqLength), s => Assert.True(s is 1 or 2));
    Assert.All(signals.Skip(clean12SeqLength), s => Assert.True(s is 3 or 4 or 5));
  }

  [Fact]
  public void SemaphoreNamedForProcesses()
  {
    const int smInitialCount = 2;
    const int smMaxCount = 2;
    const int smSignalsNumber = 10;
    var smName = $"test_sm_{nameof(SemaphoreNamedForProcesses)}";
    var signals = new List<string>();
    var sm = new Semaphore(smInitialCount, smMaxCount, smName);
    var processes = Enumerable.Range(1, 4)
      .Select(n =>
      {
        var pi = new ProcessStartInfo
        {
          FileName = Path.Combine("Apps", "Neomaster.Demos.Apps.Threads.SemaphoreProcess.exe"),
          Arguments = $"{smName} {smInitialCount} {smMaxCount} {n} {smSignalsNumber}",
          UseShellExecute = false,
          RedirectStandardOutput = true,
        };

        var p = new Process
        {
          StartInfo = pi,
          EnableRaisingEvents = true,
        };

        p.OutputDataReceived += (sender, e) =>
        {
          var signal = e.Data;
          if (!string.IsNullOrEmpty(signal))
          {
            signals.Add(e.Data);
          }
        };

        return p;
      })
      .ToList();

    processes.ForEach(p =>
    {
      p.Start();
      p.BeginOutputReadLine();
      Thread.Sleep(50);
    });
    processes.ForEach(p => p.WaitForExit());

    var cleanXYSeqLength = (smSignalsNumber * 2) - 2;
    Assert.Equal(cleanXYSeqLength, signals.Take(cleanXYSeqLength).Count(s => s is "1" or "2"));
    Assert.Equal(cleanXYSeqLength, signals.TakeLast(cleanXYSeqLength).Count(s => s is "3" or "4"));
  }

  [Fact]
  public void SemaphoreNamedCreatedNew()
  {
    var smName = $"test_sm_{nameof(SemaphoreNamedCreatedNew)}";
    var smCreatedNew = false;
    var th1 = new Thread(() =>
    {
      new Semaphore(1, 1, smName, out smCreatedNew);
    });
    var th2 = new Thread(() =>
    {
      new Semaphore(1, 1, smName, out smCreatedNew);
    });

    th1.Start();
    th1.Join();
    Assert.True(smCreatedNew);

    Thread.Sleep(100);

    th2.Start();
    th2.Join();
    Assert.False(smCreatedNew);
  }

  [Fact]
  public void MutexAsMonitor()
  {
    var mt = new Mutex();
    var signals = new ConcurrentQueue<int>();
    var threads = Enumerable.Range(1, 3)
      .Select(n => new Thread(() =>
      {
        mt.WaitOne();

        for (var i = 0; i < 10; i++)
        {
          signals.Enqueue(n);
          Thread.Sleep(20);
        }

        mt.ReleaseMutex();
      }))
      .ToList();

    threads.ForEach(th =>
    {
      th.Start();
      Thread.Sleep(20);
    });
    threads.ForEach(th => th.Join());

    Assert.Matches("1{10}2{10}3{10}", string.Concat(signals));
  }

  [Fact]
  public void MutexForSingletonThread()
  {
    const string mtName = "test_mt";
    var signals = new List<string>();
    var threads = Enumerable.Range(1, 2)
      .Select(n => new Thread(() =>
      {
        var mt = new Mutex(true, mtName, out var createdNew);
        if (!createdNew)
        {
          signals.Add($"{n}: The app is already running.");

          return;
        }

        try
        {
          for (var i = 0; i < 10; i++)
          {
            signals.Add($"{n}: Working...");
            Thread.Sleep(20);
          }
        }
        finally
        {
          mt.ReleaseMutex();
        }
      }))
      .ToList();

    threads.ForEach(th =>
    {
      th.Start();
      Thread.Sleep(50);
    });
    threads.ForEach(th => th.Join());

    Assert.Equal(10, signals.Count(s => s.StartsWith("1:")));
    Assert.Equal(1, signals.Count(s => s.StartsWith("2:")));
  }

  [Fact]
  public void MutexForSingletonApp()
  {
    var signals = new List<string>();
    var processes = Enumerable.Range(1, 2)
      .Select(n =>
      {
        var pi = new ProcessStartInfo
        {
          FileName = Path.Combine("Apps", "Neomaster.Demos.Apps.Threads.MutexSingletonApp.exe"),
          UseShellExecute = false,
          RedirectStandardOutput = true,
        };

        var s = File.Exists(pi.FileName);

        var p = new Process
        {
          StartInfo = pi,
          EnableRaisingEvents = true,
        };

        p.OutputDataReceived += (sender, e) =>
        {
          var signal = e.Data;
          if (!string.IsNullOrEmpty(signal))
          {
            signals.Add(e.Data);
          }
        };

        return p;
      })
      .ToList();

    processes.ForEach(p =>
    {
      p.Start();
      p.BeginOutputReadLine();
    });
    processes.ForEach(p => p.WaitForExit());

    Assert.Equal(1, signals.Count(s => s == "The app is already running."));
    Assert.Equal(10, signals.Count(s => s == "Working..."));
  }

  [Fact]
  public void InterruptThreadInSleepWaitJoinState()
  {
    var th1IsInterrupted = false;
    var th2IsInterrupted = false;
    var th1 = new Thread(() =>
    {
      try
      {
        Thread.Sleep(int.MaxValue);
      }
      catch (ThreadInterruptedException)
      {
        th1IsInterrupted = true;
      }
    });
    var th2 = new Thread(() =>
    {
      try
      {
        while (true)
        {
        }
      }
      catch (ThreadInterruptedException)
      {
        th2IsInterrupted = true;
      }
    });

    th1.Start();
    th2.Start();

    Thread.Sleep(100);
    th1.Interrupt();
    th2.Interrupt();

    th1.Join();
    th2.Join(0);

    Assert.True(th1IsInterrupted);
    Assert.False(th2IsInterrupted);
  }

  [Fact]
  public void DeadlockRecursiveLocking()
  {
    var thIsInterrupted = false;
    var th = new Thread(() =>
    {
      try
      {
        lock (_lock)
        {
          lock (_lock)
          {
            // Recursive locking.
          }
        }
      }
      catch (ThreadInterruptedException)
      {
        thIsInterrupted = true;
      }
    });

    th.Start();

    Thread.Sleep(100);

    th.Interrupt();
    th.Join();

    Assert.False(thIsInterrupted);
  }

  [Fact]
  public void DeadlockMutualWaitingBreakByInterrupt()
  {
    var lock1 = new object();
    var lock2 = new object();
    var th1IsInterrupted = false;
    var th2IsInterrupted = false;
    var th1 = new Thread(() =>
    {
      try
      {
        lock (lock1)
        {
          Thread.Sleep(100);
          lock (lock2)
          {
          }
        }
      }
      catch (ThreadInterruptedException)
      {
        th1IsInterrupted = true;
      }
    });
    var th2 = new Thread(() =>
    {
      try
      {
        lock (lock2)
        {
          Thread.Sleep(100);
          lock (lock1)
          {
          }
        }
      }
      catch (ThreadInterruptedException)
      {
        th2IsInterrupted = true;
      }
    });

    th1.Start();
    Thread.Sleep(30);
    th2.Start();

    Thread.Sleep(500);
    th1.Interrupt();
    th2.Interrupt();

    th1.Join();
    th2.Join();

    Assert.True(th1IsInterrupted);
    Assert.True(th2IsInterrupted);
  }

  [Fact]
  public void DeadlockMutualWaitingBreakByJoinWithTimeout()
  {
    var lock1 = new object();
    var lock2 = new object();
    var th1 = new Thread(() =>
    {
      lock (lock1)
      {
        Thread.Sleep(100);
        lock (lock2)
        {
        }
      }
    });
    var th2 = new Thread(() =>
    {
      lock (lock2)
      {
        Thread.Sleep(100);
        lock (lock1)
        {
        }
      }
    });

    th1.Start();
    Thread.Sleep(30);
    th2.Start();

    Thread.Sleep(500);

    Assert.False(th1.Join(0));
    Assert.False(th2.Join(0));
  }
}
