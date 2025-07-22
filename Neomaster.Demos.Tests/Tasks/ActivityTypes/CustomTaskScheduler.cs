using System.Collections.Concurrent;

namespace Neomaster.Demos.Tests.Tasks;

public class CustomTaskScheduler : TaskScheduler, IDisposable
{
  private readonly BlockingCollection<Task> _tasks = [];
  private readonly Thread _thread;

  public CustomTaskScheduler(string threadName)
  {
    _thread = new Thread(Run)
    {
      Name = threadName,
      IsBackground = true,
    };

    _thread.Start();
  }

  private void Run()
  {
    // Enumerates collection elements with automatic blocking
    // until new items become available
    // or adding is completed by a call to CompleteAdding().
    foreach (var task in _tasks.GetConsumingEnumerable())
    {
      TryExecuteTask(task);
    }
  }

  public void Dispose() => _tasks.CompleteAdding();

  protected override IEnumerable<Task> GetScheduledTasks() => _tasks;
  protected override void QueueTask(Task task) => _tasks.Add(task);
  protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) => false;
}
