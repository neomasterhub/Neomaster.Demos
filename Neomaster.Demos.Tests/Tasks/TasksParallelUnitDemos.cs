using System.Collections.Concurrent;
using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksParallelUnitDemos(ITestOutputHelper output)
{
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
}
