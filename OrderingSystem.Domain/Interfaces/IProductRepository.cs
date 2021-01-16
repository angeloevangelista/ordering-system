using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Domain.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    Task<List<Product>> ListByName(string productName);
    Task<List<Product>> ListByClient(string clientId);
  }
}