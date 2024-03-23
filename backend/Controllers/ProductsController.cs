using Microsoft.AspNetCore.Mvc;
using backend.Entities;
using backend.Interfaces;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        IReadOnlyList<Product> products = await productRepository.GetProductAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product productDto)
    {
        await productRepository.CreateProductAsync(productDto);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await productRepository.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product productDto)
    {
        await productRepository.UpdateProductAsync(id, productDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await productRepository.DeleteProductByIdAsync(id);
        return Ok();
    }
}