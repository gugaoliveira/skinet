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

  private readonly IProductRepository _repository;

  public ProductsController(IProductRepository repository)
  {

    _repository = repository;
  }

  [HttpGet]
  public async Task<ActionResult<List<Product>>> GetProducts()
  {

    var products = await _repository.GetProductsAsync();

    return Ok(products);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetProduct(int id)
  {
    var product = await _repository.GetProductByIdAsync(id);
    return Ok(product);
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
  {
    var productBrands = await _repository.GetProductBrandsAsync();
    return Ok(productBrands);
  }

  [HttpGet("types")]
  public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
  {
    var productTypes = await _repository.GetProductTypesAsync();
    return Ok(productTypes);
  }

}
