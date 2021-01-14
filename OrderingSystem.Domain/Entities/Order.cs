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
      Client client,
      Product product,
      int amount,
      decimal discount,
      DateTime? canceledAt) : this()
    {
      Client = client;
      Product = product;
      Amount = amount;
      Discount = discount;
      CanceledAt = null;

      AddNotifications(client, product, new Contract()
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