using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[ApiController]
[Route("")]
public class IndexController(ILogger<IndexController> logger) : Controller
{
    [HttpGet()]
    public IActionResult Index()
    {
        logger.LogDebug("Health Check invoked");
        return Ok("Ok");
    }
}
