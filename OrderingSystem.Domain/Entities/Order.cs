using System;
using Flunt.Validations;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Order : Entity
  {
    private Order() : base()
    {
    }

    public Order(
      int amount,
      decimal discount) : this()
    {
      Amount = amount;
      Discount = discount;
      CanceledAt = null;

      AddNotifications(new Contract()
        .Requires()
        .IsGreaterThan(
          Amount,
          0,
          "Order.Amount",
          "A quantidade deve ser maior do que zero."
        )
        .IsGreaterOrEqualsThan(
          Discount,
          0,
          "Order.Discount",
          "O desconto deve ser maior ou igual a zero."
        )
      );
    }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public decimal Discount { get; private set; }
    public DateTime? CanceledAt { get; private set; }

    public Order SetClient(Client client)
    {
      SetUpdatedAt();

      ClientId = client.Id;
      Client = client;

      AddNotifications(client);
      return this;
    }

    public Order SetClientId(Guid clientId)
    {
      SetUpdatedAt();

      ClientId = clientId;
      return this;
    }

    public Order SetProduct(Product product)
    {
      SetUpdatedAt();

      ProductId = product.Id;
      Product = product;

      AddNotifications(product);
      return this;
    }

    public Order SetProductId(Guid productId)
    {
      SetUpdatedAt();

      ProductId = productId;
      return this;
    }
    public Order UpdateAmount(int amount)
    {
      SetUpdatedAt();

      Amount = amount;

      AddNotifications(new Contract()
        .Requires()
        .IsGreaterThan(
          Amount,
          0,
          "Order.Amount",
          "A quantidade deve ser maior do que zero."
        ));
      return this;
    }

    public Order UpdateDiscount(int discount)
    {
      SetUpdatedAt();
      Discount = discount;

      AddNotifications(new Contract()
        .Requires()
        .IsGreaterOrEqualsThan(
          Discount,
          0,
          "Order.Discount",
          "O desconto deve ser maior ou igual a zero."
        )
      );
      return this;
    }
  }
}