namespace Neomaster.Demos.Tests.Tasks;

public class ExternalEventSource
{
  public event Action<bool> Completed;

  public void Do(int duration)
  {
    Task.Run(async () =>
    {
      await Task.Delay(duration);
      Completed?.Invoke(true);
    });
  }
}
