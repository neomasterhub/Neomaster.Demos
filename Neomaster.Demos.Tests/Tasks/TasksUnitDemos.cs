using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksUnitDemos
{
  [Fact]
  public void ThreadPoolQueueUserWorkItem()
  {
    var result = false;

    var queued = ThreadPool.QueueUserWorkItem(_ =>
    {
      Thread.Sleep(100);
      result = true;
    });

    Assert.True(queued);
    Assert.False(result);
    Thread.Sleep(200);
    Assert.True(result);
  }
}
