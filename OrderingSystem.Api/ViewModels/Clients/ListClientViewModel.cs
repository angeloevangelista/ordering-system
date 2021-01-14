using System;
using System.Collections.Generic;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Api.ViewModels.Clients
{
  public class ListClientViewModel
  {
    public ListClientViewModel(Client client)
    {
      Id = client.Id;
      Name = client.Name;
      Email = client.Email;
      Telephone = client.Telephone;
      CreatedAt = client.CreatedAt;
      UpdatedAt = client.UpdatedAt;
      Orders = client.Orders;
      Products = client.Products;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    public IEnumerable<Product> Products { get; set; }
  }
}