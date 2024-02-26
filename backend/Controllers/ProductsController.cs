using System.Net;
using backend.Data;
using backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController(StoreContext storeContext): ControllerBase
{
    [HttpGet("")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> products = await storeContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product? product = await storeContext.Products.FindAsync(id);

        if (product is null) return NotFound();
        
        return Ok(product);
    }
}