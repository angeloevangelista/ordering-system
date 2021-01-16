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
  public class ProductRepository : IProductRepository
  {
    private readonly OrderingDataContext _context;

    public ProductRepository(OrderingDataContext context)
    {
      _context = context;
    }

    public async Task Delete(string productId)
    {
      Guid productGuid = new Guid(productId);

      var product = await _context.Products.FindAsync(productGuid);

      product.Deactivate<Product>();

      await _context.SaveChangesAsync();
    }

    public async Task<Product> Find(string productId)
    {
      Guid productGuid = new Guid(productId);

      var product = await _context.Products
        .AsNoTracking()
        .FirstOrDefaultAsync(pre =>
          pre.Id == productGuid
          && pre.Active
        );

      return product;
    }

    public async Task<IEnumerable<Product>> List()
    {
      var products = await _context.Products
        .AsNoTracking()
        .Where(pre => pre.Active)
        .ToListAsync();

      return products;
    }

    public async Task<List<Product>> ListByClient(string clientId)
    {
      Guid clientGuid = new Guid(clientId);

      var products = await _context.Products
        .AsNoTracking()
        .Where(pre =>
          pre.ClientId == clientGuid
          && pre.Active
        )
        .ToListAsync();

      return products;
    }

    public async Task<List<Product>> ListByName(string productName)
    {
      var products = await _context.Products
        .AsNoTracking()
        .Where(pre => 
          pre.Name.ToLower().Contains(productName.ToLower())
          && pre.Active
        )
        .ToListAsync();

      return products;
    }

    public async Task<Product> Save(Product product)
    {
      await _context.Set<Product>().AddAsync(product);
      await _context.SaveChangesAsync();

      return product;
    }

    public async Task<Product> Update(Product product)
    {
      _context.Entry<Product>(product).State = EntityState.Modified;

      await _context.SaveChangesAsync();

      return product;
    }
  }
}