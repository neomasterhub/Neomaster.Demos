using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksUnitDemos
{
  [Fact]
  public void CreateTask()
  {
    var events = new List<int>();

    // Create
    var t1 = new Task(() =>
    {
      Thread.Sleep(100);
      events.Add(3);
    });
    t1.Start();

    // Create and start
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(50);
      events.Add(2);
    });

    events.Add(1);

    Thread.Sleep(150);

    Assert.Equal([1, 2, 3], events);
  }

  [Fact]
  public void TaskIsInPoolThread()
  {
    var t1ThreadIsPoolThread = false;
    var t2ThreadIsPoolThread = false;

    new Task(() => t1ThreadIsPoolThread = Thread.CurrentThread.IsThreadPoolThread).Start();
    Task.Run(() => t2ThreadIsPoolThread = Thread.CurrentThread.IsThreadPoolThread);

    Thread.Sleep(100);

    Assert.True(t1ThreadIsPoolThread);
    Assert.True(t2ThreadIsPoolThread);
  }

  [Fact]
  public void WaitTask()
  {
    var r = false;

    var t = Task.Run(() =>
    {
      Thread.Sleep(100);
      r = true;
    });

    t.Wait();

    Assert.True(r);
  }

  [Fact]
  public void WaitTaskWithTimeout()
  {
    var t1 = Task.Run(() => Thread.Sleep(100));
    var t2 = Task.Run(() => Thread.Sleep(int.MaxValue));

    var t1IsCompleted = t1.Wait(200);
    var t2IsCompleted = t2.Wait(200);

    Assert.True(t1IsCompleted);
    Assert.False(t2IsCompleted);
  }

  [Fact]
  public void TaskIsRunningAfterWaitTimeout()
  {
    var r = false;

    var t = Task.Run(() =>
    {
      Thread.Sleep(200);
      r = true;
    });

    t.Wait(100);

    Assert.Equal(TaskStatus.Running, t.Status);
    Assert.False(r);

    t.Wait();

    Assert.Equal(TaskStatus.RanToCompletion, t.Status);
    Assert.True(r);
  }

  [Fact]
  public void WaitTaskWithCancellationToken()
  {
    var cts = new CancellationTokenSource();

    var t1 = Task.Run(() =>
    {
      Thread.Sleep(int.MaxValue);
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    Assert.Throws<OperationCanceledException>(() => t1.Wait(cts.Token));
  }

  [Fact]
  public void WaitBlocksThread()
  {
    var th = Thread.CurrentThread;
    th.IsBackground = false; // To avoid the additional state flag ThreadState.Background.
    th.Name = "th";

    string thNameInWaiting = null;
    ThreadState? thStateInWaiting = null;

    var t = Task.Run(() =>
    {
      Thread.Sleep(100);

      thNameInWaiting = th.Name;
      thStateInWaiting = th.ThreadState;
    });

    t.Wait(200);

    Assert.Equal(th.Name, thNameInWaiting);
    Assert.Equal(ThreadState.WaitSleepJoin, thStateInWaiting);
  }

  [Fact]
  public void WaitWrapsTaskExceptionIntoAggregateException()
  {
    var aex = Assert.Throws<AggregateException>(() =>
    {
      var t = Task.Run(() =>
      {
        Thread.Sleep(100);
        throw new InvalidOperationException();
      });

      t.Wait();
    });
    Assert.Single(aex.InnerExceptions);
    Assert.IsType<InvalidOperationException>(aex.InnerException);
  }

  [Fact]
  public void Result()
  {
    var t = Task.Run(() => true);

    Assert.True(t.Result);
  }

  [Fact]
  public void ResultBlocksThread()
  {
    var th = Thread.CurrentThread;
    var thIsInWaitSleepJoin = false;

    var t1 = Task.Run(() =>
    {
      Thread.Sleep(200);
      return true;
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(100);
      thIsInWaitSleepJoin = th.ThreadState.HasFlag(ThreadState.WaitSleepJoin);
    });

    var r1 = t1.Result;

    Assert.True(r1);
    Assert.True(thIsInWaitSleepJoin);
  }

  [Fact]
  public void ResultTaskExceptionIntoAggregateException()
  {
    var aex = Assert.Throws<AggregateException>(() =>
    {
      var t = Task.Run(() =>
      {
        Thread.Sleep(100);
        throw new InvalidOperationException();
        return true;
      });

      _ = t.Result;
    });
    Assert.Single(aex.InnerExceptions);
    Assert.IsType<InvalidOperationException>(aex.InnerException);
  }

  [Fact]
  public void Delay()
  {
    var t = Task.Delay(100);

    Assert.Equal(TaskStatus.WaitingForActivation, t.Status);

    t.Wait();

    Assert.Equal(TaskStatus.RanToCompletion, t.Status);
  }

  [Fact]
  public void DelayWithCancellationToken()
  {
    var cts = new CancellationTokenSource();
    var t = Task.Delay(int.MaxValue, cts.Token);

    Thread.Sleep(100);
    cts.Cancel();

    Assert.True(t.IsCanceled);
    Assert.Equal(TaskStatus.Canceled, t.Status);
  }

  [Fact]
  public void DelayIsWorkingAfterWaitTimeout()
  {
    var t = Task.Delay(200);

    Thread.Sleep(100);

    Assert.Equal(TaskStatus.WaitingForActivation, t.Status);
  }
}
