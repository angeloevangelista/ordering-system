using System;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Api.ViewModels.Orders
{
  public class ListOrderViewModel
  {
    public ListOrderViewModel(Order order)
    {
      Id = order.Id;

      ClientId = order.ClientId;
      Client = order.Client;
      
      ProductId = order.ProductId;
      Product = order.Product;
      
      Amount = order.Amount;
      Discount = order.Discount;
      CanceledAt = order.CanceledAt;
      CreatedAt = order.CreatedAt;
      UpdatedAt = order.UpdatedAt;
    }

    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
    public decimal Discount { get; set; }
    public DateTime? CanceledAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}