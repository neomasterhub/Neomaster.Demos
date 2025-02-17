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
    var count = 0;
    var cts = new CancellationTokenSource();
    var thread = new Thread(() =>
    {
      while (!cts.IsCancellationRequested)
      {
        count++;
        Thread.Sleep(10);
      }
    });

    thread.Start();
    Thread.Sleep(100);
    cts.Cancel();

    Assert.True(count > 0 && count < 200);
  }
}
