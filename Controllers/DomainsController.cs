using Microsoft.AspNetCore.Mvc;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/domains")]
public class DomainsController() : ControllerBase
{
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok("ok");
    }
}