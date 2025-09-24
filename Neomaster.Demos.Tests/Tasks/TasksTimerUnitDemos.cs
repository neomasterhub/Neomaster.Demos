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

  [Fact]
  public void Alarms()
  {
    var now = DateTime.Now;
    var alarms = new AlarmInfo[]
    {
      new() { Name = "1", Time = TimeOnly.FromDateTime(now.AddMilliseconds(500)), Enabled = true },
      new() { Name = "2", Time = TimeOnly.FromDateTime(now.AddMilliseconds(600)), Enabled = false },
      new() { Name = "3", Time = TimeOnly.FromDateTime(now.AddMilliseconds(700)), Enabled = true, LastTriggeredDate = DateOnly.FromDateTime(now) },
      new() { Name = "4", Time = TimeOnly.FromDateTime(now.AddMilliseconds(800)), Enabled = true },
    };
    var triggeredAlarmNames = new List<string>();
    var expectedTriggeredAlarmNames = new string[] { "1", "4" };
    var cd = new CountdownEvent(expectedTriggeredAlarmNames.Length);

    var t = new Timer(30);
    t.Elapsed += (s, e) =>
    {
      var now = DateTime.Now;
      var nowDate = DateOnly.FromDateTime(now);
      var nowTime = TimeOnly.FromDateTime(now);

      foreach (var a in alarms)
      {
        if (a.Enabled
          && nowDate != a.LastTriggeredDate
          && nowTime > a.Time)
        {
          a.LastTriggeredDate = nowDate;

          triggeredAlarmNames.Add(a.Name);
          output.WriteLine($"⏰{a.Name}");

          cd.Signal();
        }
      }
    };

    t.Start();

    Assert.True(cd.Wait(1000));
    Assert.Equal(expectedTriggeredAlarmNames, triggeredAlarmNames);
    Assert.All(alarms, a => a.LastTriggeredDate = DateOnly.FromDateTime(now));

    // Output:
    // ⏰1
    // ⏰4
  }
}
