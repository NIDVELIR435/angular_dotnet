using System.Net;
using backend.Controllers.Interfaces;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class BrandsController(IRepository<Brand> repository) : ControllerBase, ICrudBase<Brand>
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Brand>>> GetAll()
    {
        IReadOnlyList<Brand> brands = await repository.GetAllAsync();

        return Ok(brands);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Brand dto)
    {
        await repository.CreateAsync(dto);

        return Created();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> Get(int id)
    {
        Brand? brand = await repository.GetByIdAsync(id);

        if (brand is null) throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);

        return Ok(brand);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<Brand>> Update(int id, Brand dto)
    {
        await repository.UpdateByIdAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await repository.DeleteByIdAsync(id);
        return Ok();
    }
}