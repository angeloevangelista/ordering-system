using System;
using System.Collections.Generic;
using OrderingSystem.Domain.Entities;

namespace OrderingSystem.Api.ViewModels.Clients
{
  public class UpdateClientViewModel
  {
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string Telephone { get; set; }
  }
}