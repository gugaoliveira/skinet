using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

  private IProductRepository _repository;

  public ProductsController(IProductRepository repository)
  {

    _repository = repository;
  }

  [HttpGet]
  public async Task<ActionResult<List<Product>>> GetProducts()
  {

    var products = await _repository.GetProductAsync();

    return Ok(products);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await _repository.GetProductByIdAsync(id);
    return Ok(product);
  }

}
