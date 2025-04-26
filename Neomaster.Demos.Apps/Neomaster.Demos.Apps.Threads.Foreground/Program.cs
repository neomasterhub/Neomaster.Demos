var foreground = new Thread(() =>
{
  var i = 1;
  while (i <= 3)
  {
    Thread.Sleep(1000);
    Console.Write(i++);
  }
});

Console.WriteLine($"Is background: {foreground.IsBackground}");

foreground.Start();

// Output:
// Is background: False
// 123
