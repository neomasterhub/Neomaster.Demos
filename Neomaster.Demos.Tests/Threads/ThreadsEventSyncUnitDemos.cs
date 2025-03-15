using System.Collections.Concurrent;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsEventSyncUnitDemos
{
  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
  public void CountdownEvent_AsJoin()
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

  [Fact]
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
}
