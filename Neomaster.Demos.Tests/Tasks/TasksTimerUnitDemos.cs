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

    // Output:
    // System.Timers.Timer 04
    // System.Timers.Timer 05
    // System.Timers.Timer 06
  }
}
