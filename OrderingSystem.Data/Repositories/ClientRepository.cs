using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.Data.Context;
using OrderingSystem.Domain.Entities;
using OrderingSystem.Domain.Interfaces;

namespace OrderingSystem.Data.Repositories
{
  public class ClientRepository : IClientRepository
  {
    private readonly OrderingDataContext _context;

    public ClientRepository(OrderingDataContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Client>> List()
    {
      var clients = await _context.Clients
        .Include(pre => pre.Orders)
        .AsNoTracking()
        .ToListAsync();

      return clients;
    }
  }
}