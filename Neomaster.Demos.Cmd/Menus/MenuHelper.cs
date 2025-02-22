namespace Neomaster.Demos.Cmd.Menus;

internal static class MenuHelper
{
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
      Console.ResetColor();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
