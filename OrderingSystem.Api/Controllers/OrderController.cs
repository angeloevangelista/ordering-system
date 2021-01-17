using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Api.ViewModels;
using OrderingSystem.Api.ViewModels.Orders;
using OrderingSystem.Domain.Entities;
using OrderingSystem.Domain.Interfaces;

namespace OrderingSystem.Api.Controllers
{
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly IProductRepository _productRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderController(
      IProductRepository productRepository,
      IClientRepository clientRepository,
      IOrderRepository orderRepository)
    {
      _productRepository = productRepository;
      _clientRepository = clientRepository;
      _orderRepository = orderRepository;
    }

    [HttpGet]
    [Route("v1/orders/{order_id}")]
    public async Task<ResultViewModel> Show([FromRoute] string order_id)
    {
      bool orderIdIsValid = Guid.TryParse(order_id, out Guid output);

      if (!orderIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var order = await _orderRepository.Find(order_id);

      ListOrderViewModel data = order == null
        ? null
        : new ListOrderViewModel(order);

      return new ResultViewModel()
      {
        Success = true,
        Data = data
      };
    }

    [HttpGet]
    [Route("v1/orders/clients/{client_id}")]
    public async Task<ResultViewModel> Index([FromRoute] string client_id)
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

      var orders = await _orderRepository.ListByClient(client_id);

      var data = orders.Select(pre => new ListOrderViewModel(pre));

      return new ResultViewModel()
      {
        Success = true,
        Data = data
      };
    }

    [HttpPost]
    [Route("v1/orders")]
    public async Task<ResultViewModel> Create(
      [FromBody] CreateOrderViewModel orderModel)
    {
      bool clientIdIsValid = Guid.TryParse(
        orderModel.ClientId,
        out Guid clientGuid);

      if (!clientIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "O ID do cliente é inválido."
        };

      bool productIdIsValid = Guid.TryParse(
        orderModel.ProductId,
        out Guid productGuid);

      if (!productIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "O ID do produto é inválido."
        };

      var order = new Order(
        orderModel.Amount,
        orderModel.Discount
      );

      if (order.Invalid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Dados inválidos.",
          Data = order.Notifications
        };

      var client = await _clientRepository.Find(orderModel.ClientId);

      if (client == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Cliente não encontrado."
        };

      order.SetClientId(client.Id);

      var product = await _productRepository.Find(orderModel.ProductId);

      if (product == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Produto não encontrado."
        };

      order.SetProductId(product.Id);

      await _orderRepository.Save(order);

      return new ResultViewModel()
      {
        Success = true,
        Data = new ListOrderViewModel(order)
      };
    }

    [HttpDelete]
    [Route("v1/orders/{order_id}")]
    public async Task<ResultViewModel> Destroy([FromRoute] string order_id)
    {
      bool orderIdIsValid = Guid.TryParse(order_id, out Guid output);

      if (!orderIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var order = await _orderRepository.Find(order_id);

      if (order == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Pedido não encontrado."
        };

      await _orderRepository.Delete(order_id);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
      };
    }
  }
}