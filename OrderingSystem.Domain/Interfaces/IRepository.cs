using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Interfaces
{
  public interface IRepository<T> where T : Entity
  {
    Task<IEnumerable<T>> List();
  }
}