using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutboxCoffee.Application.Interfaces;
using OutboxCoffee.Application.Services;
using OutboxCoffee.Core.Domain.Interfaces;
using OutboxCoffee.Infrastructure.Persistence;
using OutboxCoffee.Infrastructure.Producer;
using OutboxCoffee.Infrastructure.Repositories;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IEventPublisher>(sp =>
            new EventPublisher("rabbitmq", "coffee.exchange"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapPost("/orders", async ([FromServices] IOrderService service, [FromBody] CoffeeOrderRequest request) =>
        {
            await service.CreateOrderAsync(request.CustomerName, request.CoffeeType);
            return Results.Accepted();
        })
         .WithName("CreateOrder")
         .WithTags("Orders");


        app.Run();
    }
}

public record CoffeeOrderRequest(string CustomerName, string CoffeeType);
