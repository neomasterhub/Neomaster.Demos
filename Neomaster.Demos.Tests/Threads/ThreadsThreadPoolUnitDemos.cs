using System.ComponentModel;
using Neomaster.Demos.Shared;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

[Description("4. Thread Pool")]
public class ThreadsThreadPoolUnitDemos
{
  [Fact(DisplayName = "QueueUserWorkItem")]
  public void QueueUserWorkItem()
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

  [Fact(DisplayName = "QueueUserWorkItem with state")]
  public void QueueUserWorkItemWithState()
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

  [Fact(DisplayName = "QueueUserWorkItem Join")]
  public void QueueUserWorkItemJoin()
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

  [ExternalDemo("Set pool thread as foreground", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SetPoolThreadAsForeground/Program.cs")]

  [Fact(DisplayName = "Pool restores thread as background")]
  public void PoolRestoresThreadAsBackground()
  {
    var poolThreadId = 0;
    var poolThreadWasFound = false;
    var poolThreadIsBackground = false;
    var resetEvent = new ManualResetEvent(false);

    ThreadPool.QueueUserWorkItem(_ =>
    {
      Thread.CurrentThread.IsBackground = false;
      poolThreadId = Thread.CurrentThread.ManagedThreadId;
      resetEvent.Set();
    });

    resetEvent.WaitOne();

    var cts = new CancellationTokenSource();
    while (!cts.IsCancellationRequested)
    {
      Thread.Sleep(100);

      ThreadPool.QueueUserWorkItem(_ =>
      {
        if (Thread.CurrentThread.ManagedThreadId == poolThreadId)
        {
          poolThreadWasFound = true;
          poolThreadIsBackground = Thread.CurrentThread.IsBackground;
          cts.Cancel();
        }
      });
    }

    Assert.True(poolThreadWasFound);
    Assert.True(poolThreadIsBackground);
  }
}
