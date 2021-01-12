using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Data.Context
{
  public class OrderingDataContext : DbContext
  {
    public OrderingDataContext() : base()
    {
    }

    public OrderingDataContext(DbContextOptions<OrderingDataContext> options)
      : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Ignore<Notification>();

      modelBuilder
        .ApplyConfigurationsFromAssembly(typeof(OrderingDataContext).Assembly);
    }

    public DbSet<Client> Clients { get; private set; }
    public DbSet<Product> Products { get; private set; }
    public DbSet<Order> Orders { get; private set; }
  }
}