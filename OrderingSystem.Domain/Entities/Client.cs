using System.Linq;
using System.Collections.Generic;
using OrderingSystem.Shared.Entities;
namespace OrderingSystem.Domain.Entities
{
  public class Client : Entity
  {
    private readonly IList<Order> _orders;
    private readonly IList<Product> _products;

    public Client() : base()
    {
      this._orders = new List<Order>();
      this._products = new List<Product>();
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
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Telephone { get; private set; }
    public IReadOnlyCollection<Order> Orders { get => _orders.ToArray(); }
    public IReadOnlyCollection<Product> Products { get => _products.ToArray(); }

    public Client SetName(string name)
    {
      SetUpdatedAt();

      Name = name;

      return this;
    }

    public Client SetEmail(string email)
    {
      SetUpdatedAt();

      Email = email;

      return this;
    }

    public Client SetPassword(string password)
    {
      SetUpdatedAt();

      if (CheckPassword(password))
        Password = password;

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