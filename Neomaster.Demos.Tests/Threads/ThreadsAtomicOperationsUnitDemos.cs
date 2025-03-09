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
  public void LazyLazyInitialization()
  {
    var lo = new Lazy<bool>();

    Assert.False(lo.IsValueCreated);

    _ = lo.Value;

    Assert.True(lo.IsValueCreated);
  }

  [Fact]
  public void LazySingleInitialization()
  {
    var initializedEventCount = 0;
    var loValueAccessErrorCount = 0;
    var lo = new Lazy<string>(
      () =>
      {
        Interlocked.Increment(ref initializedEventCount);

        return "1";
      },
      LazyThreadSafetyMode.ExecutionAndPublication); // isThreadSafe: true
    var threads = Enumerable.Range(1, 20)
      .Select(i => new Thread(() =>
      {
        try
        {
          _ = lo.Value;
        }
        catch (InvalidOperationException)
        {
          Interlocked.Increment(ref loValueAccessErrorCount);
        }
      }))
      .ToList();

    threads.ForEach(th => th.Start());
    threads.ForEach(th => th.Join());

    Assert.Equal(1, initializedEventCount);
    Assert.Equal(0, loValueAccessErrorCount);
  }
}
