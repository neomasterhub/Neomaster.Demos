using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksSyncUnitDemos(ITestOutputHelper output)
{
  [Fact]
  public void LockWithinTask()
  {
    const int taskNumber = 3;
    const int taskSignalNumber = 10;
    var signals = new List<int>();
    var locked = new object();

    var tasks = Enumerable
      .Range(0, taskNumber)
      .Select(n => Task.Run(() =>
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

    output.WriteLine(string.Concat(signals)); // 111111111122222222223333333333

    Assert.Equal(taskNumber * taskSignalNumber, signals.Count);

    var skip = 0;
    for (var i = 0; i < taskNumber; i++)
    {
      Assert.Single(signals.Skip(skip).Take(taskSignalNumber).Distinct());
      skip += taskSignalNumber;
    }
  }
}
