using Microsoft.EntityFrameworkCore;
using OutboxCoffee.Core.Domain.Entities;

namespace OutboxCoffee.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

