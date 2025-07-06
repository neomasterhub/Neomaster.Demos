namespace Neomaster.Demos.Tests.Tasks;

public class DefaultSyncCtx : SynchronizationContext
{
  private int _postCallCount;
  private int _sendCallCount;

  public int PostCallCount => _postCallCount;
  public int SendCallCount => _sendCallCount;

  public override void Post(SendOrPostCallback d, object state)
  {
    Interlocked.Increment(ref _postCallCount);
    base.Post(d, state);
  }

  public override void Send(SendOrPostCallback d, object state)
  {
    Interlocked.Increment(ref _sendCallCount);
    base.Send(d, state);
  }
}
