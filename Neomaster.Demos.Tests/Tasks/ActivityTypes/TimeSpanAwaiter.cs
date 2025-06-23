using System.Runtime.CompilerServices;

namespace Neomaster.Demos.Tests.Tasks;

public static class TimeSpanAwaiter
{
  public static TaskAwaiter GetAwaiter(this TimeSpan ts)
  {
    var task = ts.Ticks == 0
      ? Task.CompletedTask
      : Task.Delay(ts);

    return task.GetAwaiter();
  }
}
