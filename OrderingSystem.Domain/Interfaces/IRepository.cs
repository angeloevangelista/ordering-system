using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Interfaces
{
  public interface IRepository<T> where T : Entity
  {
    Task<T> Save(T entity);
    Task<IEnumerable<T>> List();
    Task<T> Find(string entityId);
    Task<T> Update(T entity);
    Task Delete(string entityId);
  }
}