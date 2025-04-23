using OutboxCoffee.Core.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace OutboxCoffee.Infrastructure.Producer;

public class EventPublisher : IEventPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchange;

    public EventPublisher(string host, string exchange)
    {
        var factory = new ConnectionFactory() { HostName = host };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _exchange = exchange;

        _channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);
    }

    public Task PublishAsync(string messageType, string payload)
    {
        var body = Encoding.UTF8.GetBytes(payload);
        _channel.BasicPublish(exchange: _exchange, routingKey: "", body: body);
        return Task.CompletedTask;
    }
}