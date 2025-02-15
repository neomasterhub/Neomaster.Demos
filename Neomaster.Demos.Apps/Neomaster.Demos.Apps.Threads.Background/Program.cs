var foreground = new Thread(() =>
{
  var i = 1;
  while (i++ <= 3)
  {
    Thread.Sleep(1000);
    Console.Write("*");
  }
});
var background = new Thread(() =>
{
  while (true)
  {
    Thread.Sleep(100);
    Console.Write("_");
  }
})
{
  IsBackground = true,
};

Console.WriteLine($"* is background: {foreground.IsBackground}");
Console.WriteLine($"_ is background: {background.IsBackground}");

foreground.Start();
background.Start();

// Output:
// * is background: False
// _ is background: True
// ________*__________*_________*
