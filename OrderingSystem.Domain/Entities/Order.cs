using System;
using Flunt.Validations;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Order : Entity
  {
    private readonly Contract _amountContract, _discountContract;

    private Order() : base()
    {
      _amountContract = new Contract()
        .Requires()
        .IsGreaterThan(
          Amount,
          0,
          "Order.Amount",
          "A quantidade deve ser maior do que zero."
        );

      _discountContract = new Contract()
        .Requires()
        .IsGreaterOrEqualsThan(
          Discount,
          0,
          "Order.Discount",
          "O desconto deve ser maior ou igual a zero."
        );
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

      AddNotifications(client, product, new Contract().Requires().Join(
        _amountContract,
        _discountContract
      ));
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

      AddNotifications(_amountContract);
      return this;
    }

    public Order UpdateDiscount(int discount)
    {
      SetUpdatedAt();
      Discount = discount;

      AddNotifications(_discountContract);
      return this;
    }
  }
}