namespace OrderingSystem.Api.ViewModels
{
  public class ResultViewModel
  {
    public ResultViewModel()
    {
      Success = true;
      Message = null;
      Data = null;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
  }
}