using OutboxCoffee.Core.Domain.Entities;

namespace OutboxCoffee.Core.Domain.Interfaces;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message);
    Task<List<OutboxMessage>> GetUnprocessedMessagesAsync();
    Task MarkAsProcessedAsync(Guid messageId);
}
