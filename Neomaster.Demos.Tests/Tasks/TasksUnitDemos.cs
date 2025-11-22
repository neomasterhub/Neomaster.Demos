using System.ComponentModel;
using Neomaster.Demos.Shared;
using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

[Description("Basic")]
public class TasksUnitDemos(ITestOutputHelper output)
{
  [Fact(DisplayName = "Create task")]
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

  [Fact(DisplayName = "Task is in pool thread")]
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

  [Fact(DisplayName = "Wait task")]
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

  [Fact(DisplayName = "Wait task with timeout")]
  public void WaitTaskWithTimeout()
  {
    var t1 = Task.Run(() => Thread.Sleep(100));
    var t2 = Task.Run(() => Thread.Sleep(int.MaxValue));

    var t1IsCompleted = t1.Wait(200);
    var t2IsCompleted = t2.Wait(200);

    Assert.True(t1IsCompleted);
    Assert.False(t2IsCompleted);
  }

  [Fact(DisplayName = "Task is running after wait timeout")]
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

  [Fact(DisplayName = "Wait task with cancellation token")]
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

  [Fact(DisplayName = "Wait blocks thread")]
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

  [Fact(DisplayName = "Wait wraps task exception into `AggregateException`")]
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

  [Fact(DisplayName = "Result")]
  public void Result()
  {
    var t = Task.Run(() => true);

    Assert.True(t.Result);
  }

  [Fact(DisplayName = "Result blocks thread")]
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

  [Fact(DisplayName = "Result wraps task exception into `AggregateException`")]
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

  [Fact(DisplayName = "Delay")]
  public void Delay()
  {
    var t = Task.Delay(100);

    Assert.Equal(TaskStatus.WaitingForActivation, t.Status);

    t.Wait();

    Assert.Equal(TaskStatus.RanToCompletion, t.Status);
  }

  [Fact(DisplayName = "Delay with cancellation token")]
  public void DelayWithCancellationToken()
  {
    var cts = new CancellationTokenSource();
    var t = Task.Delay(int.MaxValue, cts.Token);

    Thread.Sleep(100);
    cts.Cancel();

    Assert.True(t.IsCanceled);
    Assert.Equal(TaskStatus.Canceled, t.Status);
  }

  [Fact(DisplayName = "Delay is working after wait timeout")]
  public void DelayIsWorkingAfterWaitTimeout()
  {
    var t = Task.Delay(200);

    Thread.Sleep(100);

    Assert.Equal(TaskStatus.WaitingForActivation, t.Status);
  }

  [Fact(DisplayName = "`await` releases manual thread")]
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

  [Fact(DisplayName = "`await` releases pool thread")]
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

  [Fact(DisplayName = "Method with `async`, without `await` is synchronous")]
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

  [Fact(DisplayName = "Method with `async`, with `await` is asynchronous")]
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

  [ExternalDemo("ConfigureAwait", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Tasks.TaskConfigureAwait/Form1.cs")]

  [Theory(DisplayName = "ConfigureAwait: effect on default sync context Post and Send")]
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

  [Theory(DisplayName = "ConfigureAwait: effect on UI sync context Post and Send")]
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

  [Fact(DisplayName = "Throwing task exception")]
  public async Task ThrowingTaskException()
  {
    var t = Task.Run(() =>
    {
      throw new InvalidOperationException();
      return 1;
    });

    Thread.Sleep(100);
    Assert.Equal(TaskStatus.Faulted, t.Status);

    var tAggEx = t.Exception;
    var tInnerEx = t.Exception.InnerException;
    Assert.NotNull(tAggEx);
    Assert.IsType<AggregateException>(tAggEx);
    Assert.IsType<InvalidOperationException>(tInnerEx);

    const int awaitThrowNumber = 2;
    const int waitThrowNumber = 3;
    const int resultThrowNumber = 4;
    var awaitThrowCount = 0;
    var waitThrowCount = 0;
    var resultThrowCount = 0;

    for (var i = 0; i < awaitThrowNumber; i++)
    {
      try
      {
        await t;
      }
      catch (InvalidOperationException ex)
      {
        awaitThrowCount++;
        Assert.Equal(tInnerEx, ex);
      }
    }

    for (var i = 0; i < waitThrowNumber; i++)
    {
      try
      {
        t.Wait();
      }
      catch (AggregateException aggEx)
      {
        waitThrowCount++;
        Assert.NotEqual(tAggEx, aggEx);
        Assert.Equal(tInnerEx, aggEx.InnerException);
      }
    }

    for (var i = 0; i < resultThrowNumber; i++)
    {
      try
      {
        _ = t.Result;
      }
      catch (AggregateException aggEx)
      {
        resultThrowCount++;
        Assert.NotEqual(tAggEx, aggEx);
        Assert.Equal(tInnerEx, aggEx.InnerException);
      }
    }

    Assert.Equal(awaitThrowNumber, awaitThrowCount);
    Assert.Equal(waitThrowNumber, waitThrowCount);
    Assert.Equal(resultThrowNumber, resultThrowCount);
  }

  [Fact(DisplayName = "ContinueWith: created task status")]
  public void ContinueWithCreatedTaskStatus()
  {
    var c = new Task(() => { }).ContinueWith(_ => { });
    Assert.Equal(TaskStatus.WaitingForActivation, c.Status);
  }

  [Fact(DisplayName = "ContinueWith: task chain")]
  public async Task ContinueWithTaskChain()
  {
    var actual = await Task.Run(() => 1)
      .ContinueWith(t1 => t1.Result * 10)
      .ContinueWith(t2 => "0" + t2.Result);

    Assert.Equal("010", actual);
  }

  [Fact(DisplayName = "ContinueWith: variable continuation")]
  public async Task ContinueWithVariableContinuation()
  {
    var events = new List<int>();

    void ContinueVar(Task prevTask)
    {
      if (prevTask.IsCompletedSuccessfully)
      {
        events.Add(0);
        return;
      }

      if (prevTask.IsFaulted)
      {
        events.Add(1);
        return;
      }

      if (prevTask.IsCanceled)
      {
        events.Add(2);
        return;
      }
    }

    await Task.Run(() => { }).ContinueWith(ContinueVar);
    await Task.Run(() => throw new InvalidOperationException()).ContinueWith(ContinueVar);
    await Task.Run(() => { }, new CancellationToken(true)).ContinueWith(ContinueVar);

    Assert.Equal([0, 1, 2], events);
  }

  [Fact(DisplayName = "ContinueWith: continuation options")]
  public async Task ContinueWithContinuationOptions()
  {
    var events = new List<int>();

    void C(Task t) => events.Add(1);

    await Task.Run(() => { }).ContinueWith(C, TaskContinuationOptions.OnlyOnRanToCompletion);
    await Task.Run(() => throw new InvalidOperationException()).ContinueWith(C, TaskContinuationOptions.OnlyOnFaulted);
    await Task.Run(() => { }, new CancellationToken(true)).ContinueWith(C, TaskContinuationOptions.OnlyOnCanceled);
    // and others

    Assert.Equal(3, events.Count);
    Assert.All(events, e => Assert.Equal(1, e));
  }

  [Fact(DisplayName = "ContinueWith: SetInterval()")]
  public void ContinueWithSetInterval()
  {
    const int expectedEventCount = 10;
    var cd = new CountdownEvent(10);
    var cts = new CancellationTokenSource();
    var events = new List<int>();

    void SetInterval(Action action, int delay, CancellationToken ct)
    {
      if (ct.IsCancellationRequested)
      {
        return;
      }

      Task.Delay(delay).ContinueWith(_ =>
      {
        action();
        SetInterval(action, delay, cts.Token);
      });
    }

    SetInterval(
      () =>
      {
        if (cts.Token.IsCancellationRequested)
        {
          return;
        }

        events.Add(1);
        cd.Signal();
      },
      100,
      cts.Token);

    cd.Wait();
    cts.Cancel();

    var eventCount1 = events.Count;
    Thread.Sleep(100);
    var eventCount2 = events.Count;

    Assert.Equal(eventCount1, eventCount2);
    Assert.Equal(expectedEventCount, eventCount1);
  }

  [Fact(DisplayName = "RunSynchronously")]
  public void RunSynchronously()
  {
    var th1Id = 0;
    var th2Id = 0;
    var events = new List<int>();

    th1Id = Thread.CurrentThread.ManagedThreadId;
    var t = new Task(() =>
    {
      th2Id = Thread.CurrentThread.ManagedThreadId;
      Thread.Sleep(100);
      events.Add(1);
    });

    t.RunSynchronously();
    events.Add(2);

    Assert.Equal([1, 2], events);
    Assert.Equal(th1Id, th2Id);
  }

  [Fact(DisplayName = "RunSynchronously and continuation")]
  public void RunSynchronouslyAndContinuation()
  {
    var th1Id = 0;
    var th2Id = 0;
    var th3Id = 0;
    var events = new List<char>();
    var cd = new CountdownEvent(2);

    th1Id = Thread.CurrentThread.ManagedThreadId;

    var t = new Task(() =>
    {
      th2Id = Thread.CurrentThread.ManagedThreadId;
      Thread.Sleep(100);
      events.Add('T');
      cd.Signal();
    });

    t.ContinueWith(_ =>
    {
      th3Id = Thread.CurrentThread.ManagedThreadId;
      Thread.Sleep(100);
      events.Add('C');
      cd.Signal();
    });

    t.RunSynchronously();
    events.Add('I');
    cd.Wait();

    Assert.Equal(['T', 'I', 'C'], events);
    Assert.Equal(th1Id, th2Id);
    Assert.NotEqual(th2Id, th3Id);
  }

  [Fact(DisplayName = "RunSynchronously and synchronous continuation")]
  public void RunSynchronouslyAndSynchronousContinuation()
  {
    var th1Id = 0;
    var th2Id = 0;
    var th3Id = 0;
    var events = new List<char>();
    var cd = new CountdownEvent(2);

    th1Id = Thread.CurrentThread.ManagedThreadId;

    var t = new Task(() =>
      {
        th2Id = Thread.CurrentThread.ManagedThreadId;
        Thread.Sleep(100);
        events.Add('T');
        cd.Signal();
      });

    t.ContinueWith(
      _ =>
      {
        th3Id = Thread.CurrentThread.ManagedThreadId;
        Thread.Sleep(100);
        events.Add('C');
        cd.Signal();
      },
      TaskContinuationOptions.ExecuteSynchronously);

    t.RunSynchronously();
    events.Add('I');
    cd.Wait();

    Assert.Equal(['T', 'C', 'I'], events);
    Assert.Equal(th1Id, th2Id);
    Assert.Equal(th2Id, th3Id);
  }

  [Fact(DisplayName = "RunSynchronously continuation")]
  public void RunSynchronouslyContinuation()
  {
    InvalidOperationException actual = null;
    var c = new Task(() => throw new ApplicationException()).ContinueWith(_ => { });

    try
    {
      c.RunSynchronously();
    }
    catch (InvalidOperationException ex)
    {
      actual = ex;
    }

    Assert.NotNull(actual);
    Assert.Equal(TaskStatus.WaitingForActivation, c.Status);
  }

  [Fact(DisplayName = "Set status to `TaskStatus.Canceled` after cancellation by `Token.ThrowIfCancellationRequested()`")]
  public void SetStatusToCanceledAfterCancellationByThrowingException()
  {
    var cd = new CountdownEvent(2);
    var cts1 = new CancellationTokenSource();
    var cts2 = new CancellationTokenSource();
    var t1 = Task.Run(
      () =>
      {
        cts1.Cancel();
        cts1.Token.ThrowIfCancellationRequested();
      });
    var t2 = Task.Run(
      () =>
      {
        cts2.Cancel();
        cts2.Token.ThrowIfCancellationRequested();
      },
      cts2.Token); // Relationship between canceled status and passed token.

    t1.ContinueWith(_ => cd.Signal(), TaskContinuationOptions.OnlyOnFaulted);
    t2.ContinueWith(_ => cd.Signal(), TaskContinuationOptions.OnlyOnCanceled);
    cd.Wait();

    Assert.Equal(TaskStatus.Faulted, t1.Status);
    Assert.Equal(TaskStatus.Canceled, t2.Status);
  }

  [Fact(DisplayName = "WhenAll")]
  public async Task WhenAll()
  {
    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      return 2;
    });
    var wat = Task.WhenAll(t1, t2);

    var results = await wat;

    Assert.Equal(TaskStatus.RanToCompletion, wat.Status);
    Assert.Equal([1, 2], results);
  }

  [Fact(DisplayName = "WhenAll: task exceptions")]
  public async Task WhenAllTaskExceptions()
  {
    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      throw new InvalidOperationException("0");
      return 2;
    });
    var t3 = Task.Run(() =>
    {
      Thread.Sleep(300);
      throw new InvalidOperationException("1");
      return 3;
    });
    var t4 = Task.Run(() =>
    {
      Thread.Sleep(400);
      return 4;
    });

    int[] results = [];
    Exception exception = null;
    var wat = Task.WhenAll(t1, t2, t3, t4);

    try
    {
      results = await wat;
    }
    catch (Exception ex)
    {
      exception = ex;
    }

    // Throwing 1st task exception.
    Assert.NotNull(exception);
    Assert.IsType<InvalidOperationException>(exception);
    Assert.Equal("0", exception.Message);

    // Collecting all task exceptions.
    var watIes = wat.Exception.InnerExceptions;
    Assert.Equal(2, watIes.Count);
    Assert.All(watIes, (e, i) =>
    {
      Assert.IsType<InvalidOperationException>(e);
      Assert.Equal(i.ToString(), e.Message);
    });

    // Does not return results.
    Assert.Empty(results);

    // Status.
    Assert.Equal(TaskStatus.Faulted, wat.Status);
  }

  [Fact(DisplayName = "WhenAll with canceled task")]
  public async Task WhenAllWithCanceledTask()
  {
    var t = Task.Run(() => 1);
    var ct = Task.Run(() => 2, new CancellationToken(true));
    var wat = Task.WhenAll(t, ct);

    int[] results = [];
    Exception exception = null;

    try
    {
      results = await wat;
    }
    catch (OperationCanceledException ex)
    {
      exception = ex;
    }

    Assert.Equal(TaskStatus.Canceled, wat.Status);
    Assert.NotNull(exception);
    Assert.Empty(results);
  }

  [Fact(DisplayName = "WhenAll with incorrect canceled task")]
  public async Task WhenAllWithIncorrectCanceledTask()
  {
    var cts1 = new CancellationTokenSource();
    var cts2 = new CancellationTokenSource();
    var t1 = Task.Run(
      () =>
      {
        cts1.Cancel();
        cts1.Token.ThrowIfCancellationRequested();
      }); // Incorrect.
    var t2 = Task.Run(
      () =>
      {
        cts2.Cancel();
        cts2.Token.ThrowIfCancellationRequested();
      },
      cts2.Token);
    var wat = Task.WhenAll(t1, t2);

    OperationCanceledException exception = null;

    try
    {
      await wat;
    }
    catch (OperationCanceledException ex)
    {
      exception = ex;
    }

    Assert.NotNull(exception);
    Assert.Equal(TaskStatus.Faulted, wat.Status);
  }

  [Fact(DisplayName = "WaitAll")]
  public void WaitAll()
  {
    var results = new List<int>();

    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      results.Add(1);
    });

    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      results.Add(2);
    });

    Task.WaitAll(t1, t2);

    Assert.Equal([1, 2], results);
  }

  [Fact(DisplayName = "WaitAll: task exceptions")]
  public void WaitAllTaskExceptions()
  {
    var results = new List<int>();

    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      results.Add(1);
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      throw new InvalidOperationException("0");
    });
    var t3 = Task.Run(() =>
    {
      Thread.Sleep(300);
      throw new InvalidOperationException("1");
    });
    var t4 = Task.Run(() =>
    {
      Thread.Sleep(400);
      results.Add(4);
    });

    AggregateException aggEx = null;

    try
    {
      Task.WaitAll(t1, t2, t3, t4);
    }
    catch (AggregateException ae)
    {
      aggEx = ae;
    }

    Assert.NotNull(aggEx);
    Assert.True(aggEx.InnerExceptions.Count > 0);
    Assert.All(aggEx.InnerExceptions, (e, i) =>
    {
      Assert.IsType<InvalidOperationException>(e);
      Assert.Equal(i.ToString(), e.Message);
    });
    Assert.Equal([1, 4], results);
  }

  [Fact(DisplayName = "WhenAny")]
  public async Task WhenAny()
  {
    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      return 2;
    });

    var wat = Task.WhenAny(t1, t2);
    var firstTask = await wat;
    var firstTaskResult = await firstTask;

    Assert.Equal(1, firstTaskResult);
  }

  [Fact(DisplayName = "WhenAny: timeout")]
  public async Task WhenAnyTimeout()
  {
    var cts = new CancellationTokenSource();
    var longRunningTask = Task.Run(() =>
    {
      while (true)
      {
        cts.Token.ThrowIfCancellationRequested();
        Thread.Sleep(200);
      }
    });
    var timeoutTask = Task.Run(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    var firstTask = await Task.WhenAny(longRunningTask, timeoutTask);
    await longRunningTask.ContinueWith(_ => { }, TaskContinuationOptions.OnlyOnCanceled);

    Assert.Equal(timeoutTask, firstTask);
    Assert.Equal(TaskStatus.Canceled, longRunningTask.Status);
  }

  [Fact(DisplayName = "WhenAny: task exception")]
  public async Task WhenAnyTaskException()
  {
    Exception exception = null;
    var wat = Task.WhenAny(Task.Run(() => throw new InvalidOperationException()));

    try
    {
      await wat;
    }
    catch (Exception ex)
    {
      exception = ex;
    }

    Assert.Null(exception);
    Assert.Equal(TaskStatus.RanToCompletion, wat.Status);
  }

  [Fact(DisplayName = "WaitAny")]
  public void WaitAny()
  {
    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      return 2;
    });

    var firstTaskIndex = Task.WaitAny(t1, t2);

    Assert.Equal(0, firstTaskIndex);
  }

  [Fact(DisplayName = "WaitAny: task exception")]
  public void WaitAnyTaskException()
  {
    Exception exception = null;
    var firstTaskIndex = 0;

    try
    {
      Task.WaitAny(Task.Run(() => throw new InvalidOperationException()));
    }
    catch (Exception ex)
    {
      exception = ex;
    }

    Assert.Equal(0, firstTaskIndex);
    Assert.Null(exception);
  }

  [Fact(DisplayName = "WhenEach")]
  public async Task WhenEach()
  {
    var t1 = Task.Run(() =>
    {
      Thread.Sleep(100);
    });

    var t2 = Task.Run(() =>
    {
      Thread.Sleep(200);
      throw new InvalidOperationException();
    });

    var cts3 = new CancellationTokenSource();
    var t3 = Task.Run(
      () =>
      {
        Thread.Sleep(300);
        cts3.Cancel();
        cts3.Token.ThrowIfCancellationRequested();
      },
      cts3.Token);

    var expectedStatusSeq = new[]
    {
      TaskStatus.RanToCompletion,
      TaskStatus.Faulted,
      TaskStatus.Canceled,
    };

    var i = 0;
    await foreach (var t in Task.WhenEach(t3, t2, t1))
    {
      Assert.Equal(expectedStatusSeq[i], t.Status);
      i++;
    }
  }

  [Fact(DisplayName = "Yield")]
  public async Task Yield()
  {
    var sw = new Stopwatch();
    var ready = false;
    var iterationCount = 0;

    async Task Producer()
    {
      await Task.Delay(20);
      ready = true;
    }

    async Task Consumer(bool useYield)
    {
      while (!ready)
      {
        iterationCount++;
        if (useYield)
        {
          await Task.Yield();
        }
      }
    }

    async Task<(int IterationCount, TimeSpan Time)> MeasureAsync(bool useYield)
    {
      ready = false;
      iterationCount = 0;

      var producer = Producer();
      var consumer = Consumer(useYield);

      sw.Restart();
      await Task.WhenAll(producer, consumer);
      sw.Stop();

      return (iterationCount, sw.Elapsed);
    }

    var m = await MeasureAsync(false);
    var my = await MeasureAsync(true);

    Assert.True(m.IterationCount > my.IterationCount);
    Assert.True(m.Time < my.Time);

    output.WriteLine(
      $"""
      Measure without yield
      Iterations: {m.IterationCount}
      Time: {m.Time}

      Measure with yield
      Iterations: {my.IterationCount}
      Time: {my.Time}
      """);
  }

  [Fact(DisplayName = "Awaiter: GetResult")]
  public void AwaiterGetResult()
  {
    var r1 = Task.Run(() => 1).GetAwaiter().GetResult(); // task.Result

    var r2 = 0;
    Task.Run(() => { r2 = 2; }).GetAwaiter().GetResult(); // task.Wait()

    Assert.Equal(1, r1);
    Assert.Equal(2, r2);
  }

  [Fact(DisplayName = "Awaiter: OnCompleted")]
  public void AwaiterOnCompleted()
  {
    var completed = false;
    var re = new ManualResetEvent(false);
    var t = Task.Run(() =>
    {
      Thread.Sleep(100);
    });

    var a = t.GetAwaiter();
    a.OnCompleted(() =>
    {
      completed = true;
      re.Set();
    });

    while (!a.IsCompleted)
    {
      Assert.False(completed);
      Thread.Sleep(20);
    }

    re.WaitOne();
    Assert.True(completed);
  }

  [Fact(DisplayName = "Awaiter: pattern")]
  public void AwaiterPattern()
  {
    string AwaitResult(Task<int> task, int? timeout = null)
    {
      var a = task.GetAwaiter();

      if (a.IsCompleted)
      {
        return $"{a.GetResult()} fast";
      }

      var re = new ManualResetEvent(false);
      var result = "time is out";
      a.OnCompleted(() =>
      {
        result = $"{a.GetResult()} slow";
        re.Set();
      });

      _ = timeout == null ? re.WaitOne() : re.WaitOne(timeout.Value);

      return result;
    }

    var t1 = Task.Run(() =>
    {
      return 1;
    });
    var r1 = AwaitResult(t1);
    Assert.Equal("1 fast", r1);

    var t2 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 2;
    });
    var r2 = AwaitResult(t2);
    Assert.Equal("2 slow", r2);

    var t3 = Task.Run(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var r3 = AwaitResult(t3, 50);
    Assert.Equal("time is out", r3);
  }

  /// <summary>
  /// See <see cref="TimeSpanAwaiter"/>.
  /// </summary>
  [Fact(DisplayName = "Awaiter: timespan awaiter")]
  public async Task TimespanAwaiter()
  {
    var sw = Stopwatch.StartNew();

    await TimeSpan.FromMilliseconds(100);

    sw.Stop();
    Assert.True(sw.Elapsed.TotalMilliseconds >= 100);
  }

  [Fact(DisplayName = "TaskCompletionSource: timeout")]
  public async Task TaskCompletionSourceTimeout()
  {
    Task<int> GetAsync(int timeout)
    {
      var tcs = new TaskCompletionSource<int>();

      var cts = new CancellationTokenSource(timeout);
      cts.Token.Register(() => tcs.TrySetException(new TimeoutException()));

      Task.Run(async () =>
      {
        await Task.Delay(100);
        tcs.TrySetResult(1);
      });

      return tcs.Task;
    }

    var x = await GetAsync(200);
    Assert.Equal(1, x);

    var ex = Assert.ThrowsAsync<TimeoutException>(() => GetAsync(50));
  }

  [Fact(DisplayName = "TaskCompletionSource: WithTimeout() extension")]
  public async Task TaskCompletionSourceWithTimeoutExtension()
  {
    var r1 = await Task.Delay(100).ContinueWith(_ => 1).WithTimeout(200);
    Assert.Equal(1, r1);

    var ex2 = await Assert.ThrowsAsync<TimeoutException>(() => Task.Delay(100).ContinueWith(_ => 1).WithTimeout(10));
  }

  [Fact(DisplayName = "TaskCompletionSource: external event source adapter")]
  public async Task TaskCompletionExternalEventSourceAdapter()
  {
    var adapter = new ExternalEventSourceAdapter();

    var t1 = adapter.DoAsync(100);
    var r1 = await t1;
    Assert.True(t1.IsCompletedSuccessfully);
    Assert.True(r1);

    var cts = new CancellationTokenSource(100);
    var t2 = adapter.DoAsync(200, cts.Token);
    var ex2 = await Assert.ThrowsAsync<TaskCanceledException>(() => t2);
    Assert.True(t2.IsCanceled);
  }

  [Fact(DisplayName = "FromResult")]
  public async Task TaskFromResult()
  {
    var rt = Task.FromResult(1);
    Assert.Equal(TaskStatus.RanToCompletion, rt.Status);
    var r = await rt;
    Assert.Equal(1, r);
  }

  [Fact(DisplayName = "FromCanceled")]
  public async Task TaskFromCanceled()
  {
    ArgumentOutOfRangeException ctEx = null;
    try
    {
      _ = Task.FromCanceled(new CancellationToken(false)); // CancellationToken.None
    }
    catch (ArgumentOutOfRangeException ex)
    {
      ctEx = ex;
    }
    finally
    {
      Assert.NotNull(ctEx);
    }

    var ct = Task.FromCanceled<int>(new CancellationToken(true));
    Assert.Equal(TaskStatus.Canceled, ct.Status);
    await Assert.ThrowsAsync<TaskCanceledException>(() => ct);
  }

  [Fact(DisplayName = "FromException")]
  public async Task TaskFromException()
  {
    Task et = null;
    InvalidOperationException etEx = null;

    try
    {
      et = Task.FromException(new InvalidOperationException());
    }
    catch (InvalidOperationException ex)
    {
      etEx = ex;
    }
    finally
    {
      Assert.Null(etEx);
    }

    await Assert.ThrowsAsync<InvalidOperationException>(() => et);
  }

  [Fact(DisplayName = "CompletedTask")]
  public void TaskCompletedTask()
  {
    var emptyVoidTask = Task.CompletedTask;
    Assert.Equal(TaskStatus.RanToCompletion, emptyVoidTask.Status);
  }

  [Fact(DisplayName = "ValueTask: cached result")]
  public async Task ValueTaskCachedResult()
  {
    int? cached = null;
    int taskCalls = 0;
    var random = new Random();

    Task<(int Result, bool FromCache)> GetTask()
    {
      return Task.Run(() =>
      {
        Interlocked.Increment(ref taskCalls);
        var result = random.Next(0, 1000);
        cached = result;
        return (result, false);
      });
    }

    ValueTask<(int Result, bool FromCache)> GetAsync()
    {
      return cached == null
        ? new ValueTask<(int, bool)>(GetTask())
        : new ValueTask<(int, bool)>((cached.Value, true));
    }

    var r1 = await GetAsync();
    Assert.False(r1.FromCache);

    var r2 = await GetAsync();
    Assert.True(r2.FromCache);

    Assert.Equal(cached, r1.Result);
    Assert.Equal(cached, r2.Result);
    Assert.Equal(1, taskCalls);
  }

  [Fact(DisplayName = "Factory")]
  public async Task Factory()
  {
    var r1 = 0;
    var f1 = new TaskFactory();
    var t1 = f1.StartNew(() => { r1 = 1; });
    await t1;
    Assert.Equal(1, r1);

    var r2 = 0;
    var f2 = new TaskFactory();
    var t2 = f2.StartNew(() => 2);
    r2 = await t2;
    Assert.Equal(2, r2);
  }

  [Fact(DisplayName = "Factory: continuations")]
  public async Task FactoryContinuations()
  {
    var f = new TaskFactory();

    var t1 = f.StartNew(() =>
    {
      Thread.Sleep(100);
      return 1;
    });
    var t2 = f.StartNew(() =>
    {
      Thread.Sleep(200);
      return 2;
    });

    var tasks = new Task[] { t1, t2 };

    var ctAny = f.ContinueWhenAny(tasks, completed => Array.IndexOf(tasks, completed));
    var ctAll = f.ContinueWhenAll(tasks, completed => completed.Length);

    var r1 = await ctAny;
    var r2 = await ctAll;

    Assert.Equal(0, r1);
    Assert.Equal(2, r2);
  }

  [Fact(DisplayName = "Factory: `TaskCreationOptions.LongRunning`")]
  public async Task FactoryCreationOptionsLongRunning()
  {
    var f = new TaskFactory(TaskCreationOptions.LongRunning, default);
    var t = f.StartNew(() => Thread.CurrentThread.IsThreadPoolThread);

    var fromThreadPool = await t;

    Assert.False(fromThreadPool);
  }

  [Theory(DisplayName = "Factory: child task attachment, `TaskCreationOptions.DenyChildAttach`")]
  [InlineData(TaskCreationOptions.None, false, true)]
  [InlineData(TaskCreationOptions.DenyChildAttach, true, true)]
  public void FactoryCreationOptionsChildTaskAttachment(
    TaskCreationOptions taskCreationOption,
    bool expectedWait1,
    bool expectedWait2)
  {
    var f = new TaskFactory(taskCreationOption, default);

    var tParent = f.StartNew(() =>
    {
      var tChild = Task.Factory.StartNew(
        () => Thread.Sleep(100),
        TaskCreationOptions.AttachedToParent);
    });

    Assert.Equal(expectedWait1, tParent.Wait(50));
    Assert.Equal(expectedWait2, tParent.Wait(150));
  }

  [Fact(DisplayName = "Factory: set task schedulers")]
  public async Task FactorySetTaskSchedulers()
  {
    using var s1 = new CustomTaskScheduler("s1");
    using var s2 = new CustomTaskScheduler("s2");

    Task<string> childTask = null;
    var parentTask = Task.Factory.StartNew(
      () =>
      {
        childTask = Task.Factory.StartNew(
          () => Thread.CurrentThread.Name,
          CancellationToken.None,
          TaskCreationOptions.AttachedToParent,
          s2);

        return Thread.CurrentThread.Name;
      },
      CancellationToken.None,
      TaskCreationOptions.None,
      s1);

    var parentThreadName = await parentTask;
    var childThreadName = await childTask;

    Assert.Equal("s1", parentThreadName);
    Assert.Equal("s2", childThreadName);
  }
}
