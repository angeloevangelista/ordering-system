using System.Collections.Generic;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Client : Entity
  {
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Telephone { get; private set; }
    public List<Order> Orders { get; private set; }
    public List<Product> Products { get; private set; }
  }
}