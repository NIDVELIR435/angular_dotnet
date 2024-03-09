using System.Net;
using backend.Data;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController(StoreContext storeContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> products = await storeContext.Products.ToListAsync();
        
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product productDto)
    {
        Product? product = await storeContext.Products
            .FirstOrDefaultAsync(p => p.Name == productDto.Name);
        if (product is not null) return Conflict("Product already exist");

        storeContext.Products.Add(productDto);
        await storeContext.SaveChangesAsync();

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await storeContext.Products.FindAsync(id);
        if (product is null) return NotFound();

        return Ok(product);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product productDto)
    {
        Product? product = await storeContext.Products.FindAsync(id);
        if (product is null) return NotFound($"Product with id {id} does not exist.");

        product.Name = productDto.Name;
        await storeContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product? product = await storeContext.Products.FindAsync(id);
        if (product is null) return NotFound($"Product with id {id} does not exist.");

        storeContext.Products.Remove(product);
        await storeContext.SaveChangesAsync();

        return Ok();
    }
}