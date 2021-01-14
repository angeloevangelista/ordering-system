using System;
using System.Collections.Generic;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Api.ViewModels.Clients
{
  public class CreateClientViewModel
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Telephone { get; set; }
  }
}