using backend.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using backend.Entities;
using backend.Interfaces;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController(IRepository<Product> repository) : ControllerBase, ICrudBase<Product>
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
    {
        IReadOnlyList<Product> products = await repository.GetAllAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Product dto)
    {
        await repository.CreateAsync(dto);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        Product? product = await repository.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] Product productDto)
    {
        await repository.UpdateByIdAsync(id, productDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await repository.DeleteByIdAsync(id);
        return Ok();
    }
}