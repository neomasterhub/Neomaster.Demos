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
}
