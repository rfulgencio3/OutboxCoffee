using OutboxCoffee.Core.Domain.Entities;

namespace OutboxCoffee.Core.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}