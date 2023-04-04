using Microsoft.AspNetCore.Mvc;

namespace Modsen.Books.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    
    public AuthorsController(ILogger<AuthorsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ActionResult TaskInboundConnection()
    {
        _logger.LogInformation("--> Inbound POST # Command service ");
        return Ok("Inbound test of from");
    }
}