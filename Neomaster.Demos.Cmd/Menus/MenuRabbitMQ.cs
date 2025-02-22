namespace Neomaster.Demos.Cmd.Menus;

internal class MenuRabbitMQ
{
  public void Show()
  {
    ShowCommands();
    var menuItem = Console.ReadLine();

    while (menuItem != "0")
    {
      switch (menuItem)
      {
        default:
          break;
      }

      ShowCommands();

      menuItem = Console.ReadLine();
    }
  }

  private static void ShowCommands()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("DEMOS / RabbitMQ");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(
      """
      0     - Back
      Other - Clear
      """);
    Console.ResetColor();
  }
}
