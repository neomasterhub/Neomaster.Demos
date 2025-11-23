using System.Collections.Concurrent;
using System.ComponentModel;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

[Description("3. Event Synchronization")]
public class ThreadsEventSyncUnitDemos
{
  [Fact(DisplayName = "EventWaitHandle.Set")]
  public void EventWaitHandle_ManualResetMode_Set()
  {
    const int partThreadsNumber = 3;
    var eh = new EventWaitHandle(false, EventResetMode.ManualReset);
    var events = new ConcurrentQueue<string>();
    var expectedEventPatterns = new List<string>
    {
      "Thread 1.1 is waiting...",
      "Thread 1.2 is waiting...",
      "Thread 1.3 is waiting...",
      "> Set the signal.----------------🠇",
      "Thread 1.[1-3] is completed.",
      "Thread 1.[1-3] is completed.",
      "Thread 1.[1-3] is completed.",
      "> Run new threads.",
      "Thread 2.1 is waiting...",
      "Thread 2.1 is completed.",
      "Thread 2.2 is waiting...",
      "Thread 2.2 is completed.",
      "Thread 2.3 is waiting...",
      "Thread 2.3 is completed.",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.WaitOne();

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads1 = Enumerable.Range(1, partThreadsNumber)
      .Select(i => new Thread(Worker) { Name = $"1.{i}" })
      .ToList();

    foreach (var th1 in threads1)
    {
      th1.Start();
      Thread.Sleep(20);
    }

    events.Enqueue("> Set the signal.----------------🠇");
    eh.Set();
    threads1.ForEach(th1 => th1.Join());

    var threads2 = Enumerable.Range(1, partThreadsNumber)
      .Select(i => new Thread(Worker) { Name = $"2.{i}" })
      .ToList();

    events.Enqueue("> Run new threads.");
    foreach (var th2 in threads2)
    {
      th2.Start();
      Thread.Sleep(20);
    }

    threads2.ForEach(th2 => th2.Join());

    Assert.All(events, (e, i) => Assert.Matches(expectedEventPatterns[i], e));
  }

  [Fact(DisplayName = "EventWaitHandle.Reset")]
  public void EventWaitHandle_ManualResetMode_Reset()
  {
    const int partThreadsNumber = 3;
    var eh = new EventWaitHandle(false, EventResetMode.ManualReset);
    var events = new ConcurrentQueue<string>();
    var expectedEventPatterns = new List<string>
    {
      "Thread 1.1 is waiting...",
      "Thread 1.2 is waiting...",
      "Thread 1.3 is waiting...",
      "> Set the signal.----------------🠇",
      "Thread 1.[1-3] is completed.",
      "Thread 1.[1-3] is completed.",
      "Thread 1.[1-3] is completed.",
      "> Reset the signal.--------------x",
      "> Run new threads.",
      "Thread 2.1 is waiting...",
      "Thread 2.2 is waiting...",
      "Thread 2.3 is waiting...",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.WaitOne();

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads1 = Enumerable.Range(1, partThreadsNumber)
      .Select(i => new Thread(Worker) { Name = $"1.{i}" })
      .ToList();

    foreach (var th1 in threads1)
    {
      th1.Start();
      Thread.Sleep(20);
    }

    events.Enqueue("> Set the signal.----------------🠇");
    eh.Set();
    threads1.ForEach(th1 => th1.Join());

    var threads2 = Enumerable.Range(1, partThreadsNumber)
      .Select(i => new Thread(Worker) { Name = $"2.{i}" })
      .ToList();

    events.Enqueue("> Reset the signal.--------------x");
    eh.Reset();

    events.Enqueue("> Run new threads.");
    foreach (var th2 in threads2)
    {
      th2.Start();
      Thread.Sleep(20);
    }

    threads2.ForEach(th2 => th2.Join(500));

    Assert.All(events, (e, i) => Assert.Matches(expectedEventPatterns[i], e));
  }

  [Fact(DisplayName = "EventWaitHandle.Set with auto-reset")]
  public void EventWaitHandle_AutoResetMode_Set()
  {
    const int threadsNumber = 3;
    var eh = new EventWaitHandle(false, EventResetMode.AutoReset);
    var events = new ConcurrentQueue<string>();
    var expectedEvents = new List<string>
    {
      "Thread 1 is waiting...",
      "Thread 2 is waiting...",
      "Thread 3 is waiting...",
      "🠆.",
      "Thread 1 is completed.",
      "🠆.",
      "Thread 2 is completed.",
      "🠆.",
      "Thread 3 is completed.",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.WaitOne();

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads = Enumerable.Range(1, threadsNumber)
      .Select(i => new Thread(Worker) { Name = $"{i}" })
      .ToList();

    foreach (var th in threads)
    {
      th.Start();
      Thread.Sleep(20);
    }

    for (var i = 0; i < threadsNumber; i++)
    {
      events.Enqueue("🠆."); // Set and auto-reset the signal.
      eh.Set();

      SpinWait.SpinUntil(() => events.Last().EndsWith("is completed."));
    }

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.Equal(expectedEvents, events);
  }

  [Fact(DisplayName = "AutoResetEvent.Set")]
  public void AutoResetEvent_Set()
  {
    const int threadsNumber = 3;
    var eh = new AutoResetEvent(false);
    var events = new ConcurrentQueue<string>();
    var expectedEvents = new List<string>
    {
      "Thread 1 is waiting...",
      "Thread 2 is waiting...",
      "Thread 3 is waiting...",
      "🠆.",
      "Thread 1 is completed.",
      "🠆.",
      "Thread 2 is completed.",
      "🠆.",
      "Thread 3 is completed.",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.WaitOne();

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads = Enumerable.Range(1, threadsNumber)
      .Select(i => new Thread(Worker) { Name = $"{i}" })
      .ToList();

    foreach (var th in threads)
    {
      th.Start();
      Thread.Sleep(20);
    }

    for (var i = 0; i < threadsNumber; i++)
    {
      events.Enqueue("🠆."); // Set and auto-reset the signal.
      eh.Set();

      SpinWait.SpinUntil(() => events.Last().EndsWith("is completed."));
    }

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.Equal(expectedEvents, events);
  }

  [Fact(DisplayName = "ManualResetEvent.Set")]
  public void ManualResetEvent_Set()
  {
    const int threadsNumber = 3;
    var eh = new ManualResetEvent(false);
    var events = new ConcurrentQueue<string>();
    var expectedEventPatterns = new List<string>
    {
      "Thread 1 is waiting...",
      "Thread 2 is waiting...",
      "Thread 3 is waiting...",
      "🠆",
      "Thread [1-3] is completed.",
      "Thread [1-3] is completed.",
      "Thread [1-3] is completed.",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.WaitOne();

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads = Enumerable.Range(1, threadsNumber)
      .Select(i => new Thread(Worker) { Name = $"{i}" })
      .ToList();

    foreach (var th in threads)
    {
      th.Start();
      Thread.Sleep(20);
    }

    events.Enqueue("🠆"); // Set the signal.
    eh.Set();

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.All(events, (e, i) => Assert.Matches(expectedEventPatterns[i], e));
  }

  [Fact(DisplayName = "ManualResetEventSlim.Set")]
  public void ManualResetEventSlim_Set()
  {
    const int threadsNumber = 3;
    var eh = new ManualResetEventSlim(false);
    var events = new ConcurrentQueue<string>();
    var expectedEventPatterns = new List<string>
    {
      "Thread 1 is waiting...",
      "Thread 2 is waiting...",
      "Thread 3 is waiting...",
      "🠆",
      "Thread [1-3] is completed.",
      "Thread [1-3] is completed.",
      "Thread [1-3] is completed.",
    };

    void Worker()
    {
      var thName = Thread.CurrentThread.Name;

      events.Enqueue($"Thread {thName} is waiting...");

      eh.Wait(); // SpinWait before blocking.

      events.Enqueue($"Thread {thName} is completed.");
    }

    var threads = Enumerable.Range(1, threadsNumber)
      .Select(i => new Thread(Worker) { Name = $"{i}" })
      .ToList();

    foreach (var th in threads)
    {
      th.Start();
      Thread.Sleep(20);
    }

    events.Enqueue("🠆"); // Set the signal.
    eh.Set();

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.All(events, (e, i) => Assert.Matches(expectedEventPatterns[i], e));
  }

  [Fact(DisplayName = "ManualResetEventSlim.Wait with timeout")]
  public void ManualResetEventSlim_WaitWithTimeout()
  {
    var eh = new ManualResetEventSlim(false);
    var events = new ConcurrentQueue<int>();
    var th = new Thread(() =>
    {
      var signalWasSet = eh.Wait(1000);

      if (!signalWasSet)
      {
        events.Enqueue(2);
      }
    });

    th.Start();

    events.Enqueue(1);

    th.Join();

    Assert.Equal([1, 2], events);
  }

  [Fact(DisplayName = "CountdownEvent.Wait as Join")]
  public void CountdownEvent_WaitAsJoin()
  {
    const int threadNumber = 5;
    var cd = new CountdownEvent(threadNumber);
    var count = 0;

    for (var i = 0; i < threadNumber; i++)
    {
      var th = new Thread(() =>
      {
        Thread.Sleep(50);

        cd.Signal();

        Interlocked.Increment(ref count);
      });

      th.Start();
    }

    cd.Wait();

    Assert.Equal(threadNumber, count);
  }

  [Fact(DisplayName = "CountdownEvent.AddCount")]
  public void CountdownEvent_AddCount()
  {
    const int threadNumber = 5;
    var cd = new CountdownEvent(threadNumber);
    var count = 0;

    void RunThread()
    {
      var th = new Thread(() =>
      {
        Thread.Sleep(50);

        cd.Signal();

        Interlocked.Increment(ref count);
      });

      th.Start();
    }

    for (var i = 0; i < threadNumber; i++)
    {
      RunThread();
    }

    cd.AddCount(1);
    RunThread();

    cd.Wait();

    Assert.Equal(threadNumber + 1, count);
  }

  [Fact(DisplayName = "CountdownEvent.TryAddCount")]
  public void CountdownEvent_TryAddCount()
  {
    var cd = new CountdownEvent(1);

    cd.Signal();
    cd.Wait();

    var countAdded = cd.TryAddCount(1);

    Assert.False(countAdded);
  }

  [Fact(DisplayName = "CountdownEvent.IsSet")]
  public void CountdownEvent_IsSet()
  {
    var cd = new CountdownEvent(2);

    Assert.False(cd.IsSet);
    cd.Signal();
    Assert.False(cd.IsSet);
    cd.Signal();
    Assert.True(cd.IsSet);

    cd.Wait();

    Assert.True(cd.IsSet);
  }

  [Fact(DisplayName = "CountdownEvent.Reset")]
  public void CountdownEvent_Reset()
  {
    const int initialCount = 3;
    var cd = new CountdownEvent(initialCount);

    cd.AddCount(10);
    cd.Signal();

    cd.Reset();

    Assert.Equal(initialCount, cd.InitialCount);
    Assert.Equal(initialCount, cd.CurrentCount);
  }

  [Fact(DisplayName = "CountdownEvent.Reset with arg")]
  public void CountdownEvent_ResetWithArg()
  {
    const int resetInitialCount = 50;
    var cd = new CountdownEvent(1);

    cd.AddCount(100);
    cd.Signal();

    cd.Reset(resetInitialCount);

    Assert.Equal(resetInitialCount, cd.InitialCount);
    Assert.Equal(resetInitialCount, cd.CurrentCount);
  }

  [Fact(DisplayName = "Barrier: Phases")]
  public void Barrier_Phases()
  {
    const int phasesNumber = 3;
    var expectedMessagePatterns = new string[]
    {
      "Thread [0-1] works in phase 0",
      "Thread [0-1] works in phase 0",
      "Phase 0 is complete!",
      "Thread [0-1] works in phase 1",
      "Thread [0-1] works in phase 1",
      "Phase 1 is complete!",
      "Thread [0-1] works in phase 2",
      "Thread [0-1] works in phase 2",
      "Phase 2 is complete!",
    };
    var messages = new ConcurrentQueue<string>();
    var barrier = new Barrier(2, b => messages.Enqueue($"Phase {b.CurrentPhaseNumber} is complete!"));
    var threads = Enumerable.Range(0, 2)
      .Select(i =>
      {
        var th = new Thread(() =>
        {
          for (var ph = 0; ph < phasesNumber; ph++)
          {
            Thread.Sleep(new Random().Next(5, 50) * 10);

            messages.Enqueue($"Thread {Thread.CurrentThread.Name} works in phase {ph}");

            barrier.SignalAndWait();
          }
        });

        th.Name = i.ToString();

        return th;
      })
      .ToList();

    threads.ForEach(th => th.Start());
    threads.ForEach(th => th.Join());

    Assert.All(messages, (m, i) => Assert.Matches(expectedMessagePatterns[i], m));
  }

  [Fact(DisplayName = "CancellationToken: create token")]
  public void CancellationToken_CreateToken()
  {
    var cts = new CancellationTokenSource();

    var ct1 = cts.Token;
    var ct2 = cts.Token;

    Assert.Equal(ct1.GetHashCode(), ct2.GetHashCode());
  }

  [Fact(DisplayName = "CancellationToken: cancellation request")]
  public void CancellationToken_CancellationRequest()
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;
    var th = new Thread(() =>
    {
      while (!ct.IsCancellationRequested)
      {
        Thread.Sleep(20);
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    Assert.False(ct.IsCancellationRequested);

    th.Start();
    cTh.Start();
    th.Join();

    Assert.True(ct.IsCancellationRequested);
  }

  [Fact(DisplayName = "CancellationToken: cancellation callback sequence")]
  public void CancellationToken_CancellationCallbackSequence()
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;
    var cancellationEvents = new List<int>();
    var th = new Thread(() =>
    {
      while (!ct.IsCancellationRequested)
      {
        Thread.Sleep(20);
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    ct.Register(() => cancellationEvents.Add(1));
    ct.Register(state => cancellationEvents.Add((int)state), 2);
    ct.Register((s, t) => cancellationEvents.Add((int)s), 3);
    ct.Register((s, t) => cancellationEvents.Add(t.GetHashCode()), null);

    th.Start();
    cTh.Start();
    th.Join();

    Assert.Equal([ct.GetHashCode(), 3, 2, 1], cancellationEvents);
  }

  [Fact(DisplayName = "CancellationToken: cancellation callback sequence: before first exception")]
  public void CancellationToken_CancellationCallbackSequence_BeforeFirstException()
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;
    var cancellationEvents = new List<int>();
    var callbackThrewException = false;
    var th = new Thread(() =>
    {
      while (!ct.IsCancellationRequested)
      {
        Thread.Sleep(20);
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);

      try
      {
        cts.Cancel(true);
      }
      catch (InvalidOperationException)
      {
        callbackThrewException = true;
      }
    });

    ct.Register(() => cancellationEvents.Add(1));
    ct.Register(() => cancellationEvents.Add(2));
    ct.Register(() =>
    {
      cancellationEvents.Add(3);
      throw new InvalidOperationException();
    });
    ct.Register(() => cancellationEvents.Add(4));
    ct.Register(() => cancellationEvents.Add(5));

    th.Start();
    cTh.Start();
    th.Join();
    cTh.Join();

    Assert.True(callbackThrewException);
    Assert.Equal([5, 4, 3], cancellationEvents);
  }

  [Fact(DisplayName = "CancellationToken: cancellation callback sequence: all, ignoring exceptions")]
  public void CancellationToken_CancellationCallbackSequence_AllIgnoringExceptions()
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;
    var cancellationEvents = new List<int>();
    var callbackThrewException = false;
    var th = new Thread(() =>
    {
      while (!ct.IsCancellationRequested)
      {
        Thread.Sleep(20);
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);

      try
      {
        cts.Cancel(false);
      }
      catch (AggregateException ae)
      {
        callbackThrewException = ae.InnerExceptions.SingleOrDefault(ie => ie is InvalidOperationException) != null;
      }
    });

    ct.Register(() => cancellationEvents.Add(1));
    ct.Register(() => cancellationEvents.Add(2));
    ct.Register(() =>
    {
      cancellationEvents.Add(3);
      throw new InvalidOperationException();
    });
    ct.Register(() => cancellationEvents.Add(4));
    ct.Register(() => cancellationEvents.Add(5));

    th.Start();
    cTh.Start();
    th.Join();
    cTh.Join();

    Assert.True(callbackThrewException);
    Assert.Equal([5, 4, 3, 2, 1], cancellationEvents);
  }

  [Fact(DisplayName = "CancellationToken.ThrowIfCancellationRequested")]
  public void CancellationToken_ThrowIfCancellationRequested()
  {
    var cts = new CancellationTokenSource();
    var ct = cts.Token;
    var operationCanceledExceptionWasThrown = false;
    var th = new Thread(() =>
    {
      try
      {
        while (true)
        {
          ct.ThrowIfCancellationRequested();
          Thread.Sleep(20);
        }
      }
      catch (OperationCanceledException)
      {
        operationCanceledExceptionWasThrown = true;
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    th.Start();
    cTh.Start();
    th.Join();

    Assert.True(ct.IsCancellationRequested);
    Assert.True(operationCanceledExceptionWasThrown);
  }

  [Fact(DisplayName = "CancellationToken.None usage")]
  public void CancellationToken_NoneUsage()
  {
    var events = new List<int>();

    void Worker(CancellationToken ct)
    {
      for (int i = 0; i < 3; i++)
      {
        ct.ThrowIfCancellationRequested();
        events.Add(i);
      }
    }

    var th = new Thread(() => Worker(CancellationToken.None));

    th.Start();
    th.Join();

    Assert.Equal([0, 1, 2], events);
  }

  [Fact(DisplayName = "CancellationToken.None variants")]
  public void CancellationToken_NoneVariants()
  {
    var ct1 = new CancellationToken(false);
    var ct2 = new CancellationToken(true);
    var ct3 = CancellationToken.None;

    Assert.False(ct1.IsCancellationRequested);
    Assert.True(ct2.IsCancellationRequested);
    Assert.False(ct3.IsCancellationRequested);

    Assert.False(ct1.CanBeCanceled);
    Assert.True(ct2.CanBeCanceled);
    Assert.False(ct3.CanBeCanceled);
    Assert.Equal(CancellationToken.None.GetHashCode(), ct3.GetHashCode());
  }

  [Fact(DisplayName = "CancellationTokenSource.CreateLinkedTokenSource")]
  public void CancellationTokenSource_CreateLinkedTokenSource()
  {
    var cts1 = new CancellationTokenSource();
    var cts2 = new CancellationTokenSource();
    var ct1 = cts1.Token;
    var ct2 = cts2.Token;
    var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct1, ct2);
    var linkedCt = linkedCts.Token;
    var th = new Thread(() =>
    {
      while (!linkedCt.IsCancellationRequested)
      {
        Thread.Sleep(20);
      }
    });
    var cTh = new Thread(() =>
    {
      Thread.Sleep(100);
      cts1.Cancel();
    });

    th.Start();
    cTh.Start();
    th.Join();

    Assert.True(cts1.IsCancellationRequested);
    Assert.True(ct1.IsCancellationRequested);
    Assert.True(linkedCts.IsCancellationRequested);
    Assert.True(linkedCt.IsCancellationRequested);
    Assert.False(cts2.IsCancellationRequested);
    Assert.False(ct2.IsCancellationRequested);
  }
}
