using System;
using Flunt.Validations;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Product : Entity
  {
    private Product() : base()
    {
    }

    public Product(string name, decimal price, Client client) : this()
    {
      Name = name;
      Price = price;
      Client = client;

      AddNotifications(client, new Contract()
        .Requires()
        .HasMinLen(
          Name,
          3,
          "Product.Name",
          "O nome deve ter no mínimo 3 caracteres."
        )
        .IsGreaterThan(
          Price,
          0,
          "Product.Price",
          "O preço deve ser maior do que zero."
        )
      );
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public Product SetName(string name)
    {
      SetUpdatedAt();
      Name = name;

      AddNotifications(new Contract()
        .Requires()
        .HasMinLen(
          Name,
          3,
          "Product.Name",
          "O nome deve ter no mínimo 3 caracteres."
        )
      );
      return this;
    }
    public Product SetPrice(decimal price)
    {
      SetUpdatedAt();
      Price = price;

      AddNotifications(new Contract()
        .Requires()
        .IsGreaterThan(
          Price,
          0,
          "Product.Price",
          "O preço deve ser maior do que zero."
        ));

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