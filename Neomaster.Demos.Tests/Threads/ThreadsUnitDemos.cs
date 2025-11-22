using System.ComponentModel;
using Neomaster.Demos.Shared;
using Xunit;

namespace Neomaster.Demos.Tests.Threads;

[Description("Basic")]
public class ThreadsUnitDemos
{
  private readonly StringWriter _stringWriter = new();

  public ThreadsUnitDemos()
  {
    Console.SetOut(_stringWriter);
  }

  private string Output => _stringWriter.ToString();

  [Fact(DisplayName = "Create, Sleep, Join")]
  public void CreateThread()
  {
    var th = new Thread(() =>
    {
      Thread.Sleep(100);
      Console.Write("1");
    });

    th.Start();

    Console.Write("2");

    th.Join(); // The following instructions of this thread will be executed after completion of th1.

    Assert.Equal("21", Output);
  }

  [Fact(DisplayName = "Create with arg")]
  public void CreateThreadWithArg()
  {
    var pts = new ParameterizedThreadStart(obj =>
    {
      Thread.Sleep(100);
      Console.Write(obj);
    });
    var th = new Thread(pts);

    th.Start('x');

    Console.Write("2");

    th.Join();

    Assert.Equal("2x", Output);
  }

  [ExternalDemo("Foreground", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Foreground/Program.cs")]
  [ExternalDemo("Background", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.Background/Program.cs")]

  [Fact(DisplayName = "Info, current thread instance")]
  public void ThreadInfo()
  {
    static void WriteThreadInfo(Thread th)
    {
      Console.WriteLine("name: " + th.Name);
    }

    Thread.CurrentThread.Name = "Main";
    WriteThreadInfo(Thread.CurrentThread);

    var th = new Thread(() => WriteThreadInfo(Thread.CurrentThread));
    th.Name = "My Thread";

    th.Start();
    th.Join();

    Assert.Equal(
      """
      name: Main
      name: My Thread

      """,
      Output);
  }

  [Fact(DisplayName = "Is alive")]
  public void ThreadIsAlive()
  {
    var th = new Thread(() => Thread.Sleep(100));
    Console.WriteLine(th.IsAlive); // false

    th.Start();
    Console.WriteLine(th.IsAlive); // true

    th.Join();
    Thread.Sleep(100);
    Console.WriteLine(th.IsAlive); // false

    Assert.Equal(
      """
      False
      True
      False

      """,
      Output);
  }

  [Fact(DisplayName = "Join with timeout")]
  public void ThreadJoinWithTimeout()
  {
    var th = new Thread(() => Thread.Sleep(50));
    th.Start();
    Console.WriteLine(th.Join(0));
    Console.WriteLine(th.Join(100));

    Assert.Equal(
      """
      False
      True

      """,
      Output);
  }

  [ExternalDemo("Abort", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AbortThread/Program.cs")]
  [ExternalDemo("Affinity (run parameter)", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityParameterized/Program.cs")]
  [ExternalDemo("Affinity (programmatically)", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.AffinityProgrammed/Program.cs")]
  [ExternalDemo("Suspend, Resume", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResume/Program.cs")]
  [ExternalDemo("Suspend, Resume: Tick Tock", "Neomaster.Demos.Apps/Neomaster.Demos.Apps.Threads.SuspendResumeTickTock/Program.cs")]

  [Fact(DisplayName = "Abort in Core via `CancellationTokenSource`")]
  public void AbortThreadInCoreViaCancellationToken()
  {
    var cancelled = false;
    var cts = new CancellationTokenSource();
    var th1 = new Thread(() =>
    {
      while (!cts.IsCancellationRequested)
      {
        Thread.Sleep(10);
      }

      cancelled = true;
    });
    var th2 = new Thread(() =>
    {
      Thread.Sleep(100);
      cts.Cancel();
    });

    th1.Start();
    th2.Start();
    th1.Join();

    Assert.True(cancelled);
  }

  [Fact(DisplayName = "Suspend-Resume in Core via `ManualResetEventSlim`")]
  public void SuspendResumeInCoreViaManualResetEventSlim()
  {
    var re = new ManualResetEvent(false);
    var th = new Thread(() =>
    {
      re.WaitOne();
      Console.Write(2);
    });
    th.Start();

    Console.Write(1);

    re.Set();
    th.Join();

    Console.Write(3);

    Assert.Equal("123", Output);
  }

  [Fact(DisplayName = "Tick Tock in Core")]
  public void ManualResetEventSlimTickTock()
  {
    var tickCount = 0;
    var tockCount = 0;
    var tickRe = new ManualResetEvent(false);
    var tockRe = new ManualResetEvent(false);
    var tickTockPairNumber = 3;
    var tick = new Thread(() =>
    {
      while (tickCount++ < tickTockPairNumber)
      {
        tickRe.WaitOne();

        Console.Write("*");

        tickRe.Reset();
        tockRe.Set();
      }
    });
    var tock = new Thread(() =>
    {
      while (tockCount++ < tickTockPairNumber)
      {
        tockRe.WaitOne();

        Console.Write("-");

        tockRe.Reset();
        tickRe.Set();
      }
    });

    tick.Start();
    tock.Start();
    tickRe.Set();
    tick.Join();
    tock.Join();

    Assert.Equal("*-*-*-", Output);
  }
}
