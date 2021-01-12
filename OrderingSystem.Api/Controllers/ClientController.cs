using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Api.ViewModels;
using OrderingSystem.Domain.Interfaces;

namespace OrderingSystem.Api.Controllers
{
  [ApiController]
  public class ClientController : ControllerBase
  {
    private readonly IClientRepository _clientRepository;

    public ClientController(IClientRepository clientRepository)
    {
      _clientRepository = clientRepository;
    }

    [HttpGet]
    [Route("v1/clients")]
    public async Task<ResultViewModel> Index()
    {
      try
      {
        var clients = await _clientRepository.List();

        return new ResultViewModel()
        {
          Success = true,
          Message = null,
          Data = clients
        };
      }
      catch (Exception exception)
      {
        return new ResultViewModel()
        {
          Success = false,
          Message = exception.Message
        };
      }
    }
  }
}