namespace OutboxCoffee.Core.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; } = string.Empty;
    public string CoffeeType { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Order(string customerName, string coffeeType)
    {
        CustomerName = customerName;
        CoffeeType = coffeeType;
    }
}