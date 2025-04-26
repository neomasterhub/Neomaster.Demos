using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Neomaster.Demos.Cmd.Demos;

internal class RabbitMQDemos
{
  /// <summary>
  /// Queue: <c>test-queue-1</c>.
  /// </summary>
  public async Task ProduceMessageAsync()
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

    var message = $"Hello Rabbit [{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]";
    var messageBytes = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(
      exchange: string.Empty,
      routingKey: queueName,
      body: messageBytes);

    Console.WriteLine($"Sent message: {message}");
    Helper.PressAnyKey();
  }

  /// <summary>
  /// Queue: <c>test-queue-1</c>.
  /// </summary>
  public async Task ConsumeMessagesAsync()
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

    var consumer = new AsyncEventingBasicConsumer(channel);
    consumer.ReceivedAsync += (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);

      Console.WriteLine($"Received message: {message}");

      return Task.CompletedTask;
    };

    await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
    Console.WriteLine("The consumer is subscribed...");
    Helper.PressAnyKey("to unsubscribe...");
  }
}
