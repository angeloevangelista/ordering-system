using System;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Product : Entity
  {
    public Product(string name, decimal price, Client client) : base()
    {
      Name = name;
      Price = price;
      Client = client;
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Client Client { get; private set; }

    public Product SetName(string name)
    {
      SetUpdatedAt();
      Name = name;

      return this;
    }
    public Product SetPrice(decimal price)
    {
      SetUpdatedAt();
      Price = price;

      return this;
    }
    public Product SetClient(Client client)
    {
      SetUpdatedAt();
      Client = client;

      return this;
    }
  }
}