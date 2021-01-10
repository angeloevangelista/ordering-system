using System;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Product : Entity
  {
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Client Client { get; private set; }
  }
}