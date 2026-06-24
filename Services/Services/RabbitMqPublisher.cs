// File path: MyWebApi/Messaging/RabbitMqPublisher.cs
using System.Text;
using RabbitMQ.Client;

namespace TraineeManagement.api.Services;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;

    public RabbitMqPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishMessageAsync(string queueName, string message)
    {
        // 1. Create a lightweight, short-lived channel
        using var channel = await _connection.CreateChannelAsync();

        // 2. Define the DLQ arguments required to match the consumer's configuration
        var queueArguments = new Dictionary<string, object?>
        {
            { "x-dead-letter-exchange", "submission-processing-dlx" },
            { "x-dead-letter-routing-key", "submission-processing" }
        };

        // 3. Declare the DURABLE queue ONLY ONCE with matching arguments
        await channel.QueueDeclareAsync(
            queue: queueName, // Dynamically uses "submission-processing" passed from your controller
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: queueArguments // <-- Injected correctly 
        );

        var body = Encoding.UTF8.GetBytes(message);

        // 4. Define the PERSISTENT properties so messages survive broker crashes
        var properties = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent 
        };

        // 5. Publish to the queue
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            mandatory: true,
            basicProperties: properties,
            body: body
        );
    }
}
