using Neomaster.Demos.Cmd.Demos;

namespace Neomaster.Demos.Cmd.Menus;

internal class MenuRabbitMQ(RabbitMQDemos rabbitMQDemos)
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
          rabbitMQDemos.ProduceMessageAsync().Do();
          break;
        case "2":
          rabbitMQDemos.ConsumeMessagesAsync().Do();
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
    Console.WriteLine("DEMOS / RabbitMQ");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine(
      """
      0     - Back
      1     - Produce message
      2     - Consume messages
      Other - Clear
      """);
    Console.ResetColor();
  }
}
