using System.ComponentModel;
using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

[Description("2. Synchronization")]
public class TasksSyncUnitDemos(ITestOutputHelper output)
{
  [Fact(DisplayName = "`lock` within task")]
  public void LockWithinTask()
  {
    const int taskNumber = 3;
    const int taskSignalNumber = 10;
    var signals = new List<int>();
    var locked = new object();

    var tasks = Enumerable
      .Range(0, taskNumber)
      .Select(_ => Task.Run(() =>
      {
        lock (locked)
        {
          for (var i = 0; i < taskSignalNumber; i++)
          {
            Thread.Sleep(100);
            signals.Add(Task.CurrentId.Value);
          }
        }
      }));
    Task.WaitAll(tasks);

    output.WriteLine(string.Concat(signals));

    Assert.Equal(taskNumber * taskSignalNumber, signals.Count);

    var skip = 0;
    for (var i = 0; i < taskNumber; i++)
    {
      var partition = signals.Skip(skip).Take(taskSignalNumber).ToArray();

      output.WriteLine(string.Concat(partition));

      Assert.Single(partition.Distinct());
      skip += taskSignalNumber;
    }

    // Output:
    // 111111111122222222223333333333
    // 1111111111
    // 2222222222
    // 3333333333
  }

  [Fact(DisplayName = "Task within `lock`")]
  public async Task TaskWithinLock()
  {
    var locked = new object();
    lock (locked)
    {
      // await Task.Delay(1000); Compiler error: CS1996: Cannot await in the body of a lock statement
    }
  }

  [Fact(DisplayName = "Lock via semaphore")]
  public async Task LockViaSemaphore()
  {
    const int taskNumber = 3;
    const int taskSignalNumber = 10;
    var signals = new List<int>();
    var sm = new SemaphoreSlim(1);

    var tasks = Enumerable
      .Range(0, taskNumber)
      .Select(n => Task.Run(async () =>
      {
        await sm.WaitAsync();
        try
        {
          for (var i = 0; i < taskSignalNumber; i++)
          {
            Thread.Sleep(100);
            signals.Add(n);
          }
        }
        finally
        {
          sm.Release();
        }
      }));
    await Task.WhenAll(tasks);

    output.WriteLine(string.Concat(signals));

    Assert.Equal(taskNumber * taskSignalNumber, signals.Count);

    var skip = 0;
    for (var i = 0; i < taskNumber; i++)
    {
      var partition = signals.Skip(skip).Take(taskSignalNumber).ToArray();

      output.WriteLine(string.Concat(partition));

      Assert.Single(partition.Distinct());
      skip += taskSignalNumber;
    }

    // Output:
    // 111111111122222222223333333333
    // 1111111111
    // 2222222222
    // 3333333333
  }
}
