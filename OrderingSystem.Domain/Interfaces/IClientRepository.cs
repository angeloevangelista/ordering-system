using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Domain.Interfaces
{
  public interface IClientRepository : IRepository<Client>
  {
  }
}