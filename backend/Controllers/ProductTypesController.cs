using backend.Controllers.Interfaces;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductTypesController (IRepository<ProductType> repository): ControllerBase, ICrudBase<ProductType>
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAll()
    {
        IReadOnlyList<ProductType> productTypes = await repository.GetAllAsync();

        return Ok(productTypes);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductType dto)
    {
        await repository.CreateAsync(dto);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> Get(int id)
    {
        ProductType? productType = await repository.GetByIdAsync(id);
        return Ok(productType);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ProductType>> Update(int id, [FromBody] ProductType productDto)
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