var smName = args[0];
var smInitialCount = int.Parse(args[1]);
var smMaxCount = int.Parse(args[2]);
var signal = args[3];
var signalsNumber = int.Parse(args[4]);
var sm = new Semaphore(smInitialCount, smMaxCount, smName);

sm.WaitOne();

for (int i = 0; i < signalsNumber; i++)
{
  Console.WriteLine(signal);
  Thread.Sleep(50);
}

sm.Release();
