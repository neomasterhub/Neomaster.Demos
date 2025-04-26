namespace Neomaster.Demos.Cmd.Menus;

internal class MenuMain(MenuRabbitMQ menuRabbitMQ)
{
  public void Show()
  {
    ShowCommands();
    var menuItem = Console.ReadLine();

    while (menuItem != "0")
    {
      switch (menuItem)
      {
        case "1":
          menuRabbitMQ.Show();
          break;
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
    Console.WriteLine("DEMOS");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(
      """
      0     - Exit
      1     - RabbitMQ
      Other - Clear
      """);
    Console.ResetColor();
  }
}
