namespace Neomaster.Demos.Tests.Tasks;

public static class TaskExtensions
{
  public static async Task<T> WithTimeout<T>(this Task<T> task, int timeout)
  {
    using var cts = new CancellationTokenSource();
    var timeoutTask = Task.Delay(timeout, cts.Token);
    var completedTask = await Task.WhenAny(timeoutTask, task);

    if (completedTask == task)
    {
      cts.Cancel();
      return await task;
    }

    throw new TimeoutException();
  }
}
