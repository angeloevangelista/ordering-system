using System;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Api.ViewModels.Products
{
  public class ListProductViewModel
  {
    public ListProductViewModel(Product product)
    {
      Id = product.Id;
      Name = product.Name;
      Price = product.Price;
      ClientId = product.ClientId;
      Client = product.Client;
      CreatedAt = product.CreatedAt;
      UpdatedAt = product.UpdatedAt;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}