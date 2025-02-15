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
}
