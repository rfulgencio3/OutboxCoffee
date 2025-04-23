using OutboxCoffee.Core.Domain.Entities;
using OutboxCoffee.Core.Domain.Interfaces;
using OutboxCoffee.Infrastructure.Persistence;

namespace OutboxCoffee.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}