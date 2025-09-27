using System.Collections.Concurrent;
using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksParallelUnitDemos
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
}
