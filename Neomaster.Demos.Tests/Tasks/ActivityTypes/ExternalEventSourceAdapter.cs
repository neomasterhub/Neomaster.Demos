namespace Neomaster.Demos.Tests.Tasks;

public class ExternalEventSourceAdapter
{
  public Task<bool> DoAsync(int duration, CancellationToken cancellationToken = default)
  {
    var tcs = new TaskCompletionSource<bool>();
    var core = new ExternalEventSource();

    void OnCanceled(bool completedEvent)
    {
      core.Completed -= OnCanceled;
      tcs.SetResult(completedEvent);
    }

    core.Completed += OnCanceled;

    cancellationToken.Register(() =>
    {
      core.Completed -= OnCanceled;
      tcs.SetCanceled(cancellationToken);
    });

    core.Do(duration);

    return tcs.Task;
  }
}
