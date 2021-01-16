namespace OrderingSystem.Api.ViewModels.Products
{
  public class CreateProductViewModel
  {
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ClientId { get; set; }
  }
}