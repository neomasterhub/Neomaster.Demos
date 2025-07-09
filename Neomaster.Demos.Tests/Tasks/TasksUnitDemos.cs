using Xunit;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksUnitDemos(ITestOutputHelper output)
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

  [Fact]
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

  [Fact]
  public void ContinueWithCreatedTaskStatus()
  {
    var c = new Task(() => { }).ContinueWith(_ => { });
    Assert.Equal(TaskStatus.WaitingForActivation, c.Status);
  }

  [Fact]
  public async Task ContinueWithTaskChain()
  {
    var actual = await Task.Run(() => 1)
      .ContinueWith(t1 => t1.Result * 10)
      .ContinueWith(t2 => "0" + t2.Result);

    Assert.Equal("010", actual);
  }

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
  public void AwaiterGetResult()
  {
    var r1 = Task.Run(() => 1).GetAwaiter().GetResult(); // task.Result

    var r2 = 0;
    Task.Run(() => { r2 = 2; }).GetAwaiter().GetResult(); // task.Wait()

    Assert.Equal(1, r1);
    Assert.Equal(2, r2);
  }

  [Fact]
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

  [Fact]
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
  [Fact]
  public async Task TimespanAwaiter()
  {
    var sw = Stopwatch.StartNew();

    await TimeSpan.FromMilliseconds(100);

    sw.Stop();
    Assert.True(sw.Elapsed.TotalMilliseconds >= 100);
  }

  [Fact]
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

  [Fact]
  public async Task TaskCompletionSourceWithTimeoutExtension()
  {
    var r1 = await Task.Delay(100).ContinueWith(_ => 1).WithTimeout(200);
    Assert.Equal(1, r1);

    var ex2 = await Assert.ThrowsAsync<TimeoutException>(() => Task.Delay(100).ContinueWith(_ => 1).WithTimeout(10));
  }

  [Fact]
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

  [Fact]
  public async Task TaskFromResult()
  {
    var rt = Task.FromResult(1);
    Assert.Equal(TaskStatus.RanToCompletion, rt.Status);
    var r = await rt;
    Assert.Equal(1, r);
  }

  [Fact]
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

  [Fact]
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
}
