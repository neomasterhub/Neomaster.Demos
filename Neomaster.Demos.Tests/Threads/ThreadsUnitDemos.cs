using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsUnitDemos
{
  private readonly StringWriter _stringWriter = new();

  public ThreadsUnitDemos()
  {
    Console.SetOut(_stringWriter);
  }

  private string Output => _stringWriter.ToString();

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
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

  [Fact]
  public void SuspendConsumeInCoreViaManualResetEventSlim()
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

  [Fact]
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
