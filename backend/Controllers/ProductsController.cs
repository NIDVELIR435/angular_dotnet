using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController
{
    [HttpGet("")]
    public string getProducts()
    {
        return "Here will be list of products";
    }

    [HttpGet("{id}")]
    public string getProduct(int id)
    {
        return $"Here will product with id: {id}";
    }
}