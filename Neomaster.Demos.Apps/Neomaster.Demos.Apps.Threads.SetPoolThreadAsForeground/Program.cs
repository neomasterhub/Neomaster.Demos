var foreground = new Thread(() =>
{
  var i = 1;
  while (i++ <= 3)
  {
    Thread.Sleep(1000);
    Console.Write("*");
  }
});

var cts = new CancellationTokenSource();
bool? poolThreadIsBackground = null;
ThreadPool.QueueUserWorkItem(_ =>
{
  Thread.CurrentThread.IsBackground = false;
  poolThreadIsBackground = Thread.CurrentThread.IsBackground;

  while (!cts.IsCancellationRequested)
  {
    Thread.Sleep(100);
    Console.Write("_");
  }

  Console.Write("x");
});

Console.WriteLine($"* is background: {foreground.IsBackground}");
Console.WriteLine($"_ is background: {poolThreadIsBackground}");

foreground.Start();
Thread.Sleep(4000);
cts.Cancel();

// Output:
// * is background: False
// _ is background: False
// _________*_________*_________*__________x
