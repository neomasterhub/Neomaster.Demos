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

  [Fact]
  public void ThreadPoolQueueUserWorkItemWithState()
  {
    var result = 0;

    var queued = ThreadPool.QueueUserWorkItem(
      state =>
      {
        Thread.Sleep(100);
        result = (int)state;
      },
      1);

    Assert.True(queued);
    Assert.NotEqual(1, result);
    Thread.Sleep(200);
    Assert.Equal(1, result);
  }

  [Fact]
  public void ThreadPoolQueueUserWorkItemJoin()
  {
    var result = false;
    var resetEvent = new ManualResetEvent(false);

    var queued = ThreadPool.QueueUserWorkItem(_ =>
    {
      Thread.Sleep(100);
      result = true;
      resetEvent.Set();
    });

    Assert.True(queued);
    Assert.False(result);
    resetEvent.WaitOne();
    Assert.True(result);
  }
}
