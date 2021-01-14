using System.Linq;
using System.Collections.Generic;
using OrderingSystem.Shared.Entities;
using Flunt.Validations;

namespace OrderingSystem.Domain.Entities
{
  public class Client : Entity
  {
    private readonly IList<Order> _orders;
    private readonly IList<Product> _products;

    private Client() : base()
    {
      _orders = new List<Order>();
      _products = new List<Product>();
    }

    public Client(
      string name,
      string email,
      string password,
      string telephone) : this()
    {
      Name = name;
      Email = email;
      Telephone = telephone;
      Password = BCrypt.Net.BCrypt.HashPassword(password, 8);

      AddNotifications(new Contract()
        .Requires()
        .HasMinLen(
          Name,
          3,
          "Client.Name",
          "O nome deve ter no mínimo 3 caracteres."
        )
        .IsEmail(Email, "Client.Email", "E-mail inválido.")
        .HasMinLen(
          Password,
          6,
          "Client.Password",
          "A senha deve conter no mínimo 6 caracteres."
        )
      );
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Telephone { get; private set; }
    public IEnumerable<Order> Orders { get => _orders.ToArray(); }
    public IEnumerable<Product> Products { get => _products.ToArray(); }

    public Client SetPassword(string oldPassword, string newPassword)
    {
      SetUpdatedAt();

      if (!CheckPassword(oldPassword))
        AddNotification("Client.Password", "As senhas não combinam.");

      Password = BCrypt.Net.BCrypt.HashPassword(newPassword, 8);

      if (newPassword.Length < 6)
        AddNotification(
          "Client.Password",
          "A senha deve conter no mínimo 6 caracteres."
        );

      return this;
    }

    public Client SetTelephone(string telephone)
    {
      SetUpdatedAt();
      Telephone = telephone;

      return this;
    }

    public Client AddOrder(Order order)
    {
      SetUpdatedAt();
      AddNotifications(order);

      _orders.Add(order);

      return this;
    }

    public Client AddOrders(params Order[] orders)
    {
      SetUpdatedAt();

      foreach (var order in orders)
        _orders.Add(order);

      return this;
    }

    public Client AddProduct(Product product)
    {
      SetUpdatedAt();
      AddNotifications(product);

      _products.Add(product);

      return this;
    }

    public Client AddProducts(params Product[] products)
    {
      SetUpdatedAt();

      foreach (var product in products)
        _products.Add(product);

      return this;
    }

    public bool CheckPassword(string password)
    {
      bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, this.Password);

      return passwordMatch;
    }
  }
}