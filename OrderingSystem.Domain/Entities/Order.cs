using System;
using OrderingSystem.Shared.Entities;

namespace OrderingSystem.Domain.Entities
{
  public class Order : Entity
  {
    public Client Client { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public decimal Discount { get; private set; }
    public DateTime? CanceledAt { get; private set; }
  }
}