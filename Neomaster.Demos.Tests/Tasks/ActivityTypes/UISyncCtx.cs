namespace Neomaster.Demos.Tests.Tasks;

public class UISyncCtx : SynchronizationContext
{
  private readonly int _mainThreadId = Thread.CurrentThread.ManagedThreadId;

  private int _postCallCount;
  private int _sendCallCount;

  public int PostCallCount => _postCallCount;
  public int SendCallCount => _sendCallCount;

  public override void Post(SendOrPostCallback d, object state)
  {
    if (Thread.CurrentThread.ManagedThreadId == _mainThreadId)
    {
      Send(d, state);
      return;
    }

    Interlocked.Increment(ref _postCallCount);
    ThreadPool.QueueUserWorkItem(_ => d(state));
  }

  public override void Send(SendOrPostCallback d, object state)
  {
    Interlocked.Increment(ref _sendCallCount);
    d(state);
  }
}
