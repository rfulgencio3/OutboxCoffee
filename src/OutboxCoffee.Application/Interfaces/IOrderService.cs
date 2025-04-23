namespace OutboxCoffee.Application.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(string customerName, string coffeeType);
}
