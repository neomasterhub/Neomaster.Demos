using Xunit;

namespace Neomaster.Demos.Tests.Threads;

public class ThreadsAtomicOperationsUnitDemos
{
  [Fact]
  public void VolatileClass()
  {
    var ready = false;
    int? data = null;

    void Producer()
    {
      Thread.Sleep(100);
      data = 1;

      Volatile.Write(ref ready, true);
    }

    void Consumer()
    {
      while (!Volatile.Read(ref ready))
      {
        // The Consumer thread may get stuck in a while (!ready) loop
        // because the processor may cache ready and not see it change.
      }
    }

    var producerTh = new Thread(Producer);
    var consumerTh = new Thread(Consumer);

    producerTh.Start();
    consumerTh.Start();

    consumerTh.Join();
    producerTh.Join();

    Assert.Equal(1, data);
  }
}
