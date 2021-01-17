using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderingSystem.Domain.Entities;
using OrderingSystem.Data.Context;
using OrderingSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OrderingSystem.Data.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly OrderingDataContext _context;

    public OrderRepository(OrderingDataContext context)
    {
      _context = context;
    }

    public async Task Delete(string orderId)
    {
      Guid orderGuid = new Guid(orderId);

      var order = await _context.Orders.FindAsync(orderGuid);

      order.Deactivate<Order>();

      await _context.SaveChangesAsync();
    }

    public async Task<Order> Find(string orderId)
    {
      Guid orderGuid = new Guid(orderId);

      var order = await _context.Orders
        .AsNoTracking()
        .FirstOrDefaultAsync(pre =>
          pre.Id == orderGuid
          && pre.Active
        );

      return order;
    }

    public async Task<IEnumerable<Order>> List()
    {
      var orders = await _context.Orders
        .AsNoTracking()
        .Where(pre => pre.Active)
        .ToListAsync();

      return orders;
    }

    public async Task<IEnumerable<Order>> ListByClient(string clientId)
    {
      Guid clientGuid = new Guid(clientId);

      var orders = await _context.Orders
        .AsNoTracking()
        .Where(pre => 
          pre.ClientId == clientGuid
          && pre.Active
        )
        .ToListAsync();

      return orders;
    }

    public async Task<Order> Save(Order order)
    {
      await _context.Set<Order>().AddAsync(order);
      await _context.SaveChangesAsync();

      return order;
    }

    public async Task<Order> Update(Order order)
    {
      _context.Entry<Order>(order).State = EntityState.Modified;

      await _context.SaveChangesAsync();

      return order;
    }
  }
}