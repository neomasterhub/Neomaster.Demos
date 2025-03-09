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

  [Fact]
  public void LazyMultipleInitialization()
  {
    var initializedEventCount = 0;
    var loValueAccessErrorCount = 0;
    var initializationValues = new List<string>();
    var returnValues = new List<string>();
    var lo = new Lazy<string>(
      () =>
      {
        Thread.Sleep(100);

        Interlocked.Increment(ref initializedEventCount);

        var value = DateTime.Now.ToString("mm:ss.fff");
        initializationValues.Add(value);

        return value;
      },
      LazyThreadSafetyMode.PublicationOnly);
    var threads = Enumerable.Range(1, 20)
      .Select(i => new Thread(() =>
      {
        try
        {
          returnValues.Add(lo.Value);
        }
        catch (InvalidOperationException)
        {
          Interlocked.Increment(ref loValueAccessErrorCount);
        }
      }))
      .ToList();

    threads.ForEach(th => th.Start());
    threads.ForEach(th => th.Join());

    Assert.True(initializedEventCount > 1);
    Assert.True(initializationValues.Count > 1);
    Assert.Equal(0, loValueAccessErrorCount);
    Assert.Single(returnValues.Distinct());
  }

  [Fact]
  public void LazyUnsafeInitialization()
  {
    var initializedEventCount = 0;
    var loValueAccessErrorCount = 0;
    var lo = new Lazy<string>(
      () =>
      {
        Thread.Sleep(100);

        Interlocked.Increment(ref initializedEventCount);

        return "1";
      },
      LazyThreadSafetyMode.None); // isThreadSafe: false
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

    Assert.True(initializedEventCount >= 1);
    Assert.True(loValueAccessErrorCount > 0);
  }
}
