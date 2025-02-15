namespace Neomaster.Demos.Cmd;

internal class Menu()
{
  public void Show()
  {
    ShowCommands();
    var menuItem = Console.ReadLine();

    while (menuItem != "0")
    {
      ShowCommands();

      switch (menuItem)
      {
        default:
          break;
      }

      menuItem = Console.ReadLine();
    }
  }

  private static void ShowCommands()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("DEMOS");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(
      """
      0     - Exit
      Other - Clear
      """);
    Console.ResetColor();
  }
}
