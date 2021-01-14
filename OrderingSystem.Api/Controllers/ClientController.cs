using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Api.ViewModels;
using OrderingSystem.Api.ViewModels.Clients;
using OrderingSystem.Domain.Entities;
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

    [HttpPost]
    [Route("v1/clients")]
    public async Task<ResultViewModel> Create(
      [FromBody] CreateClientViewModel clientModel)
    {
      var client = new Client(
        clientModel.Name,
        clientModel.Email,
        clientModel.Password,
        clientModel.Telephone
      );

      if (client.Invalid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Dados inválidos.",
          Data = client.Notifications
        };

      bool emailAlreadyInUse =
        (await _clientRepository.FindByEmail(client.Email)) != null;

      if (emailAlreadyInUse)
        return new ResultViewModel()
        {
          Success = false,
          Message = "O E-mail já está em uso."
        };

      await _clientRepository.Save(client);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
        Data = new ListClientViewModel(client)
      };
    }

    [HttpPut]
    [Route("v1/clients/{client_id}")]
    public async Task<ResultViewModel> Update(
      [FromRoute] string client_id,
      [FromBody] UpdateClientViewModel clientModel)
    {
      bool clientIdIsValid = Guid.TryParse(client_id, out Guid output);

      if (!clientIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var client = await _clientRepository.Find(client_id);

      if (client == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Cliente não encontrado."
        };

      if (clientModel.OldPassword != null)
        client.SetPassword(clientModel.OldPassword, clientModel.NewPassword);

      if (clientModel.Telephone != null)
        client.SetTelephone(clientModel.Telephone);


      if (client.Invalid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Dados inválidos.",
          Data = client.Notifications
        };

      await _clientRepository.Update(client);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
        Data = new ListClientViewModel(client)
      };
    }

    [HttpGet]
    [Route("v1/clients")]
    public async Task<ResultViewModel> Index()
    {
      var clients = await _clientRepository
        .List();

      var data = clients.Select(pre => new ListClientViewModel(pre));

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
        Data = data
      };
    }

    [HttpGet]
    [Route("v1/clients/{client_id}")]
    public async Task<ResultViewModel> Show([FromRoute] string client_id)
    {
      bool clientIdIsValid = Guid.TryParse(client_id, out Guid output);

      if (!clientIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var client = await _clientRepository.Find(client_id);

      ListClientViewModel data = client == null
        ? null
        : new ListClientViewModel(client);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
        Data = data
      };
    }

    [HttpDelete]
    [Route("v1/clients/{client_id}")]
    public async Task<ResultViewModel> Destroy([FromRoute] string client_id)
    {
      bool clientIdIsValid = Guid.TryParse(client_id, out Guid output);

      if (!clientIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var client = await _clientRepository.Find(client_id);

      if (client == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Cliente não encontrado."
        };

      await _clientRepository.Delete(client_id);

      return new ResultViewModel()
      {
        Success = true,
        Message = "Cliente removido."
      };
    }
  }
}