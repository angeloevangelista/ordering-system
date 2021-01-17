using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task Delete(string clientId)
    {
      Guid clientGuid = new Guid(clientId);

      var client = await _context.Clients.FindAsync(clientGuid);

      client.Deactivate<Client>();

      await _context.SaveChangesAsync();
    }

    public async Task<Client> Find(string clientId)
    {
      Guid clientGuid = new Guid(clientId);

      var client = await _context.Clients
        .AsNoTracking()
        .FirstOrDefaultAsync(pre =>
          pre.Id.Equals(clientGuid)
          && pre.Active
        );

      return client;
    }

    public async Task<Client> FindByEmail(string email)
    {
      var client = await _context.Clients
        .AsNoTracking()
        .Include(pre => pre.Orders)
        .FirstOrDefaultAsync(pre =>
          pre.Email.Equals(email)
          && pre.Active
        );

      return client;
    }

    public async Task<IEnumerable<Client>> List()
    {
      var clients = await _context.Clients
        .AsNoTracking()
        .Where(pre => pre.Active)
        .ToListAsync();

      return clients;
    }

    public async Task<Client> Save(Client client)
    {
      await _context.Set<Client>().AddAsync(client);
      await _context.SaveChangesAsync();

      return client;
    }

    public async Task<Client> Update(Client client)
    {
      _context.Entry<Client>(client).State = EntityState.Modified;

      await _context.SaveChangesAsync();

      return client;
    }
  }
}