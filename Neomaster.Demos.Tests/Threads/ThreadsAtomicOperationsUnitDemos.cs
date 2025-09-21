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

  [Fact]
  public void InterlockedIncrement()
  {
    const int threadNumber = 20;
    const int threadSignalNumber = 100_000;
    var sum1 = 0;
    var sum2 = 0;
    var threads = Enumerable
      .Range(1, threadNumber)
      .Select(_ => new Thread(() =>
      {
        for (var i = 0; i < threadSignalNumber; i++)
        {
          sum1++;
          Interlocked.Increment(ref sum2);
        }
      }))
      .ToList();

    threads.ForEach(th => th.Start());
    threads.ForEach(th => th.Join());

    Assert.True(sum1 < sum2);
    Assert.Equal(threadNumber * threadSignalNumber, sum2);
  }
}
