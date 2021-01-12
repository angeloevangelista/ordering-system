using Microsoft.EntityFrameworkCore;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Data.Context
{
  public class OrderingDataContext : DbContext
  {
    public OrderingDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; private set; }
    public DbSet<Product> Products { get; private set; }
    public DbSet<Order> Orders { get; private set; }
  }
}