using Microsoft.EntityFrameworkCore;
using OutboxCoffee.Core.Domain.Entities;
using OutboxCoffee.Core.Domain.Interfaces;
using OutboxCoffee.Infrastructure.Persistence;

namespace OutboxCoffee.Infrastructure.Repositories;

public class OutboxRepository : IOutboxRepository
{
    private readonly AppDbContext _context;

    public OutboxRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        _context.OutboxMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OutboxMessage>> GetUnprocessedMessagesAsync()
    {
        return await _context.OutboxMessages
            .Where(x => x.ProcessedAt == null)
            .ToListAsync();
    }

    public async Task MarkAsProcessedAsync(Guid messageId)
    {
        var message = await _context.OutboxMessages.FindAsync(messageId);
        if (message != null)
        {
            message.ProcessedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}