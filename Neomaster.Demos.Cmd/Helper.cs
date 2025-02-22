namespace Neomaster.Demos.Cmd;

internal static class Helper
{
  public static void PressAnyKey(string action = "to continue...")
  {
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"Press any key {action}");
    Console.ResetColor();
    Console.ReadKey();
  }

  public static void Do(this Task task)
  {
    try
    {
      task.Wait();
    }
    catch (Exception e)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine(e.Message);
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine(e.StackTrace);
      PressAnyKey();
    }
  }
}
