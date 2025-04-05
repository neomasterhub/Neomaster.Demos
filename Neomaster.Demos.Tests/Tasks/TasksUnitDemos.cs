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

  [Fact]
  public void AwaitReleasesManualThread()
  {
    Thread th1 = null;
    Thread th2 = null;
    Thread th3 = null;
    ThreadState? th1StateInTask = null;
    var re = new ManualResetEvent(false);

    async void MethodAsync()
    {
      th1 = Thread.CurrentThread;

      await Task.Run(() =>
      {
        th1StateInTask = th1.ThreadState;
        th2 = Thread.CurrentThread;
      });

      th3 = Thread.CurrentThread;

      re.Set();
    }

    var th = new Thread(MethodAsync);
    th.Start();

    re.WaitOne();

    Assert.False(th.IsThreadPoolThread);
    Assert.Equal(th1.ManagedThreadId, th.ManagedThreadId);
    Assert.Equal(ThreadState.Stopped, th1.ThreadState);
    Assert.NotEqual(th2.ManagedThreadId, th1.ManagedThreadId);
    Assert.True(th2.IsThreadPoolThread);
    Assert.Equal(th2.ManagedThreadId, th3.ManagedThreadId);
  }

  [Fact]
  public void AwaitReleasesPoolThread()
  {
    Thread th1 = null;
    Thread th2 = null;
    Thread th3 = null;
    var re = new ManualResetEvent(false);
    var events = new List<int>();

    async void MethodAsync()
    {
      th1 = Thread.CurrentThread;

      await Task.Run(() =>
      {
        th2 = Thread.CurrentThread;

        Thread.Sleep(100);
        events.Add(2);
      });

      th3 = Thread.CurrentThread;
    }

    Thread th = null;
    Task.Run(() =>
    {
      th = Thread.CurrentThread;
      MethodAsync();

      Thread.Sleep(50);
      events.Add(1);

      Thread.Sleep(100);
      re.Set();
    });

    re.WaitOne();

    Assert.Equal([1, 2], events);
    Assert.Equal(th1.ManagedThreadId, th.ManagedThreadId);
    Assert.NotEqual(th2.ManagedThreadId, th.ManagedThreadId);
    Assert.Equal(th3.ManagedThreadId, th2.ManagedThreadId);
  }

  [Fact]
  public void MethodWithAsyncWithoutAwaitIsSync()
  {
    var events = new List<int>();
    var cd = new CountdownEvent(2);

    async void Add2()
    {
      Thread.Sleep(100);
      events.Add(2);

      cd.Signal();
    }

    Task.Run(() =>
    {
      Add2();
      events.Add(1);

      cd.Signal();
    });

    cd.Wait();

    Assert.Equal([2, 1], events);
  }

  [Fact]
  public void MethodWithAsyncWithAwaitIsAsync()
  {
    var events = new List<int>();
    var cd = new CountdownEvent(2);

    async void Add2()
    {
      await Task.Delay(100);
      events.Add(2);

      cd.Signal();
    }

    Task.Run(() =>
    {
      Add2();
      events.Add(1);

      cd.Signal();
    });

    cd.Wait();

    Assert.Equal([1, 2], events);
  }

  [Theory]
  [InlineData(true, 1, 0)]
  [InlineData(false, 0, 0)]
  public async Task ConfigureAwaitEffectOnDefaultSyncContextPostAndSend(
    bool configureAwait,
    int expectedPostCallCount,
    int expectedSendCallCount)
  {
    var originalCtx = SynchronizationContext.Current;
    var defaultCtx = new DefaultSyncCtx();
    SynchronizationContext.SetSynchronizationContext(defaultCtx);
    Assert.IsType<DefaultSyncCtx>(SynchronizationContext.Current);

    try
    {
      await Task.Delay(100).ConfigureAwait(configureAwait);
    }
    finally
    {
      SynchronizationContext.SetSynchronizationContext(originalCtx);
    }

    Assert.Equal(expectedPostCallCount, defaultCtx.PostCallCount);
    Assert.Equal(expectedSendCallCount, defaultCtx.SendCallCount);
  }

  [Theory]
  [InlineData(true, 0, 1)]
  [InlineData(false, 0, 0)]
  public async Task ConfigureAwaitEffectOnUISyncContextPostAndSend(
    bool configureAwait,
    int expectedPostCallCount,
    int expectedSendCallCount)
  {
    var originalCtx = SynchronizationContext.Current;
    var uiCtx = new UISyncCtx();
    SynchronizationContext.SetSynchronizationContext(uiCtx);
    Assert.IsType<UISyncCtx>(SynchronizationContext.Current);

    try
    {
      await Task.Delay(100).ConfigureAwait(configureAwait);
    }
    finally
    {
      SynchronizationContext.SetSynchronizationContext(originalCtx);
    }

    Assert.Equal(expectedPostCallCount, uiCtx.PostCallCount);
    Assert.Equal(expectedSendCallCount, uiCtx.SendCallCount);
  }

  public class DefaultSyncCtx : SynchronizationContext
  {
    private int _postCallCount;
    private int _sendCallCount;

    public int PostCallCount => _postCallCount;
    public int SendCallCount => _sendCallCount;

    public override void Post(SendOrPostCallback d, object state)
    {
      Interlocked.Increment(ref _postCallCount);
      base.Post(d, state);
    }

    public override void Send(SendOrPostCallback d, object state)
    {
      Interlocked.Increment(ref _sendCallCount);
      base.Send(d, state);
    }
  }

  public class UISyncCtx : SynchronizationContext
  {
    private readonly int _mainThreadId = Thread.CurrentThread.ManagedThreadId;

    private int _postCallCount;
    private int _sendCallCount;

    public int PostCallCount => _postCallCount;
    public int SendCallCount => _sendCallCount;

    public override void Post(SendOrPostCallback d, object state)
    {
      if (Thread.CurrentThread.ManagedThreadId == _mainThreadId)
      {
        Send(d, state);
        return;
      }

      Interlocked.Increment(ref _postCallCount);
      ThreadPool.QueueUserWorkItem(_ => d(state));
    }

    public override void Send(SendOrPostCallback d, object state)
    {
      Interlocked.Increment(ref _sendCallCount);
      d(state);
    }
  }
}
