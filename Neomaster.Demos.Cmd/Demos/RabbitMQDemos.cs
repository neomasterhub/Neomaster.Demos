using System.Text;
using RabbitMQ.Client;

namespace Neomaster.Demos.Cmd.Demos;

internal class RabbitMQDemos
{
  public async Task CreateChannelAndProduceMessageAsync()
  {
    var factory = new ConnectionFactory()
    {
      HostName = "localhost",
      UserName = "rabbit",
      Password = "rabbit",
      VirtualHost = "demos",
    };

    using var connection = await factory.CreateConnectionAsync();
    using var channel = await connection.CreateChannelAsync();

    var queueName = "test-queue-1";
    await channel.QueueDeclareAsync(
      queue: queueName,
      durable: false,
      exclusive: false,
      autoDelete: false,
      arguments: null);

    var message = $"Hello Rabbit {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}";
    var messageBytes = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(
      exchange: string.Empty,
      routingKey: queueName,
      body: messageBytes);

    Console.WriteLine("Pushed message:");
    Console.WriteLine(message);
    Console.ReadKey();
  }
}
