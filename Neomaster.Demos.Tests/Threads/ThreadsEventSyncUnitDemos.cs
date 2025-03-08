using System.Collections.Concurrent;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsEventSyncUnitDemos
{
  [Fact]
  public void EventWaitHandle_ManualReset_Set()
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
  public void EventWaitHandle_ManualReset_Reset()
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
  public void EventWaitHandle_AutoReset_Set()
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
}
