var mt = new Mutex(true, "Mutex_Neomaster.Demos.Apps.Threads.MutexSingletonApp", out var createdNew);

if (!createdNew)
{
  Console.WriteLine("The app is already running.");

  return;
}

try
{
  for (var i = 0; i < 10; i++)
  {
    Console.WriteLine("Working...");
    Thread.Sleep(50);
  }
}
finally
{
  mt.ReleaseMutex();
}
