using OutboxCoffee.Application.Interfaces;
using OutboxCoffee.Core.Domain.Entities;
using OutboxCoffee.Core.Domain.Interfaces;
using System.Text.Json;

namespace OutboxCoffee.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOutboxRepository _outboxRepository;

    public OrderService(IOrderRepository orderRepository, IOutboxRepository outboxRepository)
    {
        _orderRepository = orderRepository;
        _outboxRepository = outboxRepository;
    }

    public async Task CreateOrderAsync(string customerName, string coffeeType)
    {
        var order = new Order(customerName, coffeeType);
        await _orderRepository.AddAsync(order);

        var orderCreatedMessage = new OutboxMessage
        {
            Type = "OrderCreated",
            Payload = JsonSerializer.Serialize(order)
        };
    }
}