namespace TraineeManagement.api.Services;

public interface IRabbitMqPublisher
{
    Task PublishMessageAsync(string queueName, string message);
}
