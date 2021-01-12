using System;
using Flunt.Validations;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Product : Entity
  {
    private readonly Contract _nameContract, _priceContract;

    private Product() : base()
    {
      _nameContract = new Contract()
        .Requires()
        .HasMinLen(
          Name,
          3,
          "Product.Name",
          "O nome deve ter no mínimo 3 caracteres.");

      _priceContract = new Contract()
        .Requires()
        .IsGreaterThan(
          Price,
          0,
          "Product.Price",
          "O preço deve ser maior do que zero."
        );
    }

    public Product(string name, decimal price, Client client) : this()
    {
      Name = name;
      Price = price;
      Client = client;

      AddNotifications(client, new Contract().Requires().Join(
        _nameContract,
        _priceContract
      ));
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    public Product SetName(string name)
    {
      SetUpdatedAt();
      Name = name;

      AddNotifications(_nameContract);
      return this;
    }
    public Product SetPrice(decimal price)
    {
      SetUpdatedAt();
      Price = price;

      AddNotifications(_priceContract);
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