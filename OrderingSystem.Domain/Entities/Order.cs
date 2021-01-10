using System;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Order : Entity
  {
    public Order(
      Client client,
      Product product,
      int amount,
      decimal discount,
      DateTime? canceledAt) : base()
    {
      Client = client;
      Product = product;
      Amount = amount;
      Discount = discount;
      CanceledAt = null;
    }

    public Client Client { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public decimal Discount { get; private set; }
    public DateTime? CanceledAt { get; private set; }

    public Order UpdateAmount(int amount)
    {
      SetUpdatedAt();

      Amount = amount;

      return this;
    }

    public Order UpdateDiscount(int discount)
    {
      SetUpdatedAt();

      Discount = discount;

      return this;
    }
  }
}