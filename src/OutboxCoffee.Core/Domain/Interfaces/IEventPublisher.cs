namespace OutboxCoffee.Core.Domain.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync(string messageType, string payload);
}
