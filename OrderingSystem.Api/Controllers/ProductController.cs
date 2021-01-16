using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingSystem.Api.ViewModels;
using OrderingSystem.Api.ViewModels.Products;
using OrderingSystem.Domain.Entities;
using OrderingSystem.Domain.Interfaces;

namespace OrderingSystem.Api.Controllers
{
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _productRepository;
    private readonly IClientRepository _clientRepository;

    public ProductController(
      IProductRepository productRepository,
      IClientRepository clientRepository
      )
    {
      _productRepository = productRepository;
      _clientRepository = clientRepository;
    }

    [HttpGet]
    [Route("v1/products/{product_id}")]
    public async Task<ResultViewModel> Show([FromRoute] string product_id)
    {
      bool productIdIsValid = Guid.TryParse(product_id, out Guid output);

      if (!productIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var product = await _productRepository.Find(product_id);

      ListProductViewModel data = product == null
        ? null
        : new ListProductViewModel(product);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
        Data = data
      };
    }

    [HttpGet]
    [Route("v1/products")]
    public async Task<ResultViewModel> Index([FromQuery] string product_name)
    {
      IEnumerable<Product> products;

      products = product_name != null
        ? await _productRepository.ListByName(product_name)
        : await _productRepository.List();

      var data = products.Select(pre => new ListProductViewModel(pre));

      return new ResultViewModel()
      {
        Success = true,
        Data = data
      };
    }

    [HttpPost]
    [Route("v1/products")]
    public async Task<ResultViewModel> Create(
      [FromBody] CreateProductViewModel productModel)
    {
      bool clientIdIsValid = Guid.TryParse(productModel.ClientId, out Guid clientGuid);

      if (!clientIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "O ID do cliente é inválido."
        };

      var product = new Product(
        productModel.Name,
        productModel.Price,
        clientGuid
      );

      if (product.Invalid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Dados inválidos.",
          Data = product.Notifications
        };

      var clientExists = (await _clientRepository.Find(productModel.ClientId)) != null;

      if (!clientExists)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Cliente não encontrado."
        };

      await _productRepository.Save(product);

      return new ResultViewModel()
      {
        Success = true,
        Data = new ListProductViewModel(product)
      };
    }

    [HttpDelete]
    [Route("v1/products/{product_id}")]
    public async Task<ResultViewModel> Destroy([FromRoute] string product_id)
    {
      bool productIdIsValid = Guid.TryParse(product_id, out Guid output);

      if (!productIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var product = await _productRepository.Find(product_id);

      if (product == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Produto não encontrado."
        };

      await _productRepository.Delete(product_id);

      return new ResultViewModel()
      {
        Success = true,
        Message = null,
      };
    }

    [HttpPut]
    [Route("v1/products/{product_id}")]
    public async Task<ResultViewModel> Update(
      [FromRoute] string product_id,
      [FromBody] UpdateProductViewModel productModel)
    {
      bool productIdIsValid = Guid.TryParse(product_id, out Guid output);

      if (!productIdIsValid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "ID inválido."
        };

      var product = await _productRepository.Find(product_id);

      if (product == null)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Produto não encontrado."
        };

      if (productModel.Name != null)
        product.SetName(productModel.Name);

      if (productModel.Price != 0)
        product.SetPrice(productModel.Price);

      if (product.Invalid)
        return new ResultViewModel()
        {
          Success = false,
          Message = "Dados inválidos.",
          Data = product.Notifications
        };

      await _productRepository.Update(product);

      return new ResultViewModel()
      {
        Success = true,
        Data = new ListProductViewModel(product)
      };
    }
  }
}