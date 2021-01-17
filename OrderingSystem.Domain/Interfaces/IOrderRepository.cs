using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Domain.Interfaces
{
  public interface IOrderRepository : IRepository<Order>
  {
    Task<IEnumerable<Order>> ListByClient(string clientId);  
  }
}