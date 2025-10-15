using System.Collections.Concurrent;
using Xunit;
using Timer = System.Timers.Timer;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksFeaturesUnitDemos(ITestOutputHelper output)
{
  [Fact]
  public void Callback()
  {
    const int eventNumber = 3;
    var eventCount = 0;
    var cd = new CountdownEvent(eventNumber);
    var t = new Timer(1000);

    t.Elapsed += (sender, e) =>
    {
      output.WriteLine(@$"{sender} {e.SignalTime:ss}");
      eventCount++;
      cd.Signal();
    };

    t.Start();
    cd.Wait();

    Assert.Equal(eventNumber, eventCount);
    Assert.True(t.AutoReset);

    // Output:
    // System.Timers.Timer 04
    // System.Timers.Timer 05
    // System.Timers.Timer 06
  }

  [Fact]
  public void AutoResetFalse()
  {
    var eventCount = 0;
    var cd = new CountdownEvent(5);
    var t = new Timer(100)
    {
      AutoReset = false,
    };

    t.Elapsed += (_, _) =>
    {
      eventCount++;
      cd.Signal();
    };

    t.Start();
    var cdWasSet = cd.Wait((int)t.Interval * 3);

    Assert.False(cdWasSet);
    Assert.Equal(cd.InitialCount - 1, cd.CurrentCount);
    Assert.Equal(1, eventCount);
  }

  [Fact]
  public void Alarms()
  {
    var now = DateTime.Now;
    var alarms = new AlarmInfo[]
    {
      new() { Name = "1", Time = TimeOnly.FromDateTime(now.AddMilliseconds(500)), Enabled = true },
      new() { Name = "2", Time = TimeOnly.FromDateTime(now.AddMilliseconds(600)), Enabled = false },
      new() { Name = "3", Time = TimeOnly.FromDateTime(now.AddMilliseconds(700)), Enabled = true, LastTriggeredDate = DateOnly.FromDateTime(now) },
      new() { Name = "4", Time = TimeOnly.FromDateTime(now.AddMilliseconds(800)), Enabled = true },
    };
    var triggeredAlarmNames = new List<string>();
    var expectedTriggeredAlarmNames = new string[] { "1", "4" };
    var cd = new CountdownEvent(expectedTriggeredAlarmNames.Length);

    var t = new Timer(30);
    t.Elapsed += (s, e) =>
    {
      var now = DateTime.Now;
      var nowDate = DateOnly.FromDateTime(now);
      var nowTime = TimeOnly.FromDateTime(now);

      foreach (var a in alarms)
      {
        if (a.Enabled
          && nowDate != a.LastTriggeredDate
          && nowTime > a.Time)
        {
          a.LastTriggeredDate = nowDate;

          triggeredAlarmNames.Add(a.Name);
          output.WriteLine($"⏰{a.Name}");

          cd.Signal();
        }
      }
    };

    t.Start();

    Assert.True(cd.Wait(1000));
    Assert.Equal(expectedTriggeredAlarmNames, triggeredAlarmNames);
    Assert.All(alarms, a => a.LastTriggeredDate = DateOnly.FromDateTime(now));

    // Output:
    // ⏰1
    // ⏰4
  }

  [Fact]
  public void ParallelFor()
  {
    const int numberOf1 = 1000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;
    var thIds = new ConcurrentBag<int>();
    var thsFromPool = true;

    var result = Parallel.For(0, arr.Length, i =>
    {
      Interlocked.Add(ref sum, arr[i]);
      thIds.Add(Thread.CurrentThread.ManagedThreadId);
      thsFromPool &= Thread.CurrentThread.IsThreadPoolThread;
    });

    var thIdsCount = thIds.Distinct().Count();
    Assert.True(result.IsCompleted);
    Assert.Null(result.LowestBreakIteration);
    Assert.Equal(numberOf1, sum);
    Assert.True(thIdsCount > 1 && thIdsCount < numberOf1);
    Assert.True(thsFromPool);
  }

  [Fact]
  public void ParallelForStop()
  {
    const int numberOf1 = 10000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;

    var stop = false;
    var timeout = new Thread(() =>
    {
      Thread.Sleep(500);
      stop = true;
    });
    timeout.Start();

    var result = Parallel.For(0, arr.Length, (i, state) =>
    {
      if (stop)
      {
        state.Stop();
      }

      Thread.SpinWait(10000);
      Interlocked.Add(ref sum, arr[i]);
    });

    timeout.Join();

    Assert.False(result.IsCompleted);
    Assert.Null(result.LowestBreakIteration);
    Assert.True(sum > 0);
    Assert.True(sum < numberOf1);

    output.WriteLine($"sum: {sum}"); // 2541
  }

  [Fact]
  public void ParallelForBreak()
  {
    const int numberOf1 = 10000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;

    var stop = false;
    var timeout = new Thread(() =>
    {
      Thread.Sleep(500);
      stop = true;
    });
    timeout.Start();

    var result = Parallel.For(0, arr.Length, (i, state) =>
    {
      if (stop)
      {
        state.Break();
      }

      Thread.SpinWait(10000);
      Interlocked.Add(ref sum, arr[i]);
    });

    timeout.Join();

    Assert.False(result.IsCompleted);
    Assert.NotNull(result.LowestBreakIteration);
    Assert.True(sum > 0);
    Assert.True(sum < numberOf1);

    output.WriteLine($"sum: {sum}"); // 2541
    output.WriteLine($"broken at: {result.LowestBreakIteration}"); // 1306
  }

  [Fact]
  public void ParallelForLocalVar()
  {
    const int numberOf1 = 1000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;
    var localInit = () => 0;

    Parallel.For(
      0,
      arr.Length,
      localInit,
      (i, state, local) =>
      {
        local += arr[i];
        return local;
      },
      localFinally =>
      {
        Interlocked.Add(ref sum, localFinally);
        output.WriteLine($"local finally: {localFinally}");
      });

    Assert.Equal(numberOf1, sum);

    // local finally: 452
    // local finally: 548
  }

  [Fact]
  public void ParallelForException()
  {
    const int numberOf1 = 1000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;
    var exceptions = new ConcurrentBag<string>();
    ParallelLoopResult result = default;

    try
    {
      result = Parallel.For(0, numberOf1, i =>
      {
        Interlocked.Add(ref sum, arr[i]);

        if (i > 0)
        {
          throw new Exception($"thread id: {Thread.CurrentThread.ManagedThreadId}");
        }
      });
    }
    catch (AggregateException ae)
    {
      Assert.True(ae.InnerExceptions.Count > 0);

      foreach (var ie in ae.InnerExceptions)
      {
        exceptions.Add(ie.Message);
      }
    }

    Assert.True(exceptions.Count > 1);
    Assert.True(sum > 0);
    Assert.True(sum < numberOf1);
    Assert.False(result.IsCompleted);

    exceptions.ToList().ForEach(output.WriteLine);
    output.WriteLine($"sum: {sum}");

    // thread id: 13
    // thread id: 14
    // sum: 3
  }

  [Fact]
  public void ParallelForOptions()
  {
    const int numberOf1 = 1000;
    var arr = Enumerable.Repeat(1, numberOf1).ToArray();
    var sum = 0;
    var thIds = new ConcurrentBag<int>();

    Parallel.For(0, arr.Length, new ParallelOptions { MaxDegreeOfParallelism = 1 }, i =>
    {
      Interlocked.Add(ref sum, arr[i]);
      thIds.Add(Thread.CurrentThread.ManagedThreadId);
    });

    Assert.Single(thIds.Distinct());
    Assert.Equal(numberOf1, sum);
  }

  [Fact]
  public void ParallelForStateChecks()
  {
    var stateIsChecked = false;

    Parallel.For(0, 1000, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (i, state) =>
    {
      if (i == 0)
      {
        Assert.False(state.ShouldExitCurrentIteration);
        state.Stop();
        Assert.True(state.IsStopped);
        Assert.True(state.ShouldExitCurrentIteration);
      }

      if (i > 0)
      {
        Thread.Sleep(100);
        Assert.True(state.ShouldExitCurrentIteration);
        Assert.True(state.IsStopped);
        stateIsChecked = true;
      }
    });

    Assert.True(stateIsChecked);
  }

  [Fact]
  public void ParallelForeach()
  {
    const int enemyNumber = 1000;
    var enemies = Enumerable.Range(1, enemyNumber)
      .Select(n => new Enemy
      {
        Id = n,
        HP = n % 4 == 0
          ? 0
          : Shared.Random.Next(1, 101),
        ToDestroy = false,
      })
      .ToList();

    Parallel.ForEach(enemies, e =>
    {
      if (e.HP == 0)
      {
        e.ToDestroy = true;
      }
    });
    var destroyedCount = enemies.RemoveAll(e => e.ToDestroy);

    Assert.NotEqual(0, destroyedCount);
    Assert.Equal(enemyNumber - destroyedCount, enemies.Count);
    Assert.All(
      enemies,
      e =>
      {
        Assert.NotEqual(0, e.HP);
        Assert.NotEqual(0, e.Id % 4);
      });
  }

  private class Enemy
  {
    public int Id { get; set; }
    public int HP { get; set; }
    public bool ToDestroy { get; set; }
  }
}
