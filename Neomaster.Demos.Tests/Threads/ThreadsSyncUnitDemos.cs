using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsSyncUnitDemos
{
  private static readonly object _lock = new();

  [Fact]
  public void LockAddingToList()
  {
    const int count = 100;
    var numbers = new List<int>(count);
    var rnd = new Random();
    var threads = Enumerable.Range(1, count)
      .Select(n => new Thread(() =>
      {
        lock (_lock)
        {
          Thread.Sleep(rnd.Next(0, 20));
          numbers.Add(n);
        }
      }))
      .ToArray();

    foreach (var th in threads)
    {
      th.Start();
    }

    foreach (var th in threads)
    {
      th.Join();
    }

    Assert.Equal(Enumerable.Range(1, count), numbers);
  }
}
