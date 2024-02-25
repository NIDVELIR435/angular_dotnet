using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("")]
public class IndexController(ILogger<IndexController> logger) : Controller
{
    [HttpGet()]
    public IActionResult Index()
    {
        return Ok("Ok");
    }
}
