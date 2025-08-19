using Xunit;
using Timer = System.Timers.Timer;

namespace Neomaster.Demos.Tests.Tasks;

public class TasksTimerUnitDemos(ITestOutputHelper output)
{
  [Fact]
  public void Callback()
  {
    const int eventNumber = 3;
    var eventCount = 0;
    var cd = new CountdownEvent(eventNumber);
    var t = new Timer(1000);

    t.Elapsed += (sender, e) =>
    {
      output.WriteLine(@$"{sender} {e.SignalTime:ss}");
      eventCount++;
      cd.Signal();
    };

    t.Start();
    cd.Wait();

    Assert.Equal(eventNumber, eventCount);
    Assert.True(t.AutoReset);

    // Output:
    // System.Timers.Timer 04
    // System.Timers.Timer 05
    // System.Timers.Timer 06
  }

  [Fact]
  public void AutoResetFalse()
  {
    var eventCount = 0;
    var cd = new CountdownEvent(5);
    var t = new Timer(100)
    {
      AutoReset = false,
    };

    t.Elapsed += (_, _) =>
    {
      eventCount++;
      cd.Signal();
    };

    t.Start();
    var cdWasSet = cd.Wait((int)t.Interval * 3);

    Assert.False(cdWasSet);
    Assert.Equal(cd.InitialCount - 1, cd.CurrentCount);
    Assert.Equal(1, eventCount);
  }
}
