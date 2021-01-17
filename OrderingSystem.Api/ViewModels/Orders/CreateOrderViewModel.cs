namespace OrderingSystem.Api.ViewModels.Orders
{
  public class CreateOrderViewModel
  {
    public string ClientId { get; set; }
    public string ProductId { get; set; }
    public int Amount { get; set; }
    public decimal Discount { get; set; }
  }
}