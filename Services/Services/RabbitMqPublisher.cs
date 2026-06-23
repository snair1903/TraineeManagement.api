// File path: MyWebApi/Messaging/RabbitMqPublisher.cs
using System.Text;
using RabbitMQ.Client;

namespace TraineeManagement.api.Services;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;

    // The connection is injected here from Program.cs
    public RabbitMqPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishMessageAsync(string queueName, string message)
    {
        // 1. Create a lightweight, short-lived channel
        using var channel = await _connection.CreateChannelAsync();

        // 2. Declare the DURABLE queue
        await channel.QueueDeclareAsync(
            queue: queueName, 
            durable: true,          // <-- Queue survives restarts
            exclusive: false, 
            autoDelete: false, 
            arguments: null
        );

        var body = Encoding.UTF8.GetBytes(message);

        // 3. Define the PERSISTENT properties
        var properties = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent // <-- Message survives restarts
        };

        // 4. Publish to the queue
        await channel.BasicPublishAsync(
            exchange: string.Empty, 
            routingKey: queueName, 
            mandatory: true,
            basicProperties: properties, 
            body: body
        );
    }
}
