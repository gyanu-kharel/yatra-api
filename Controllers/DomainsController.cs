using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Database;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/domains")]
public class DomainsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var domains = await dbContext
        .Domains
        .Select(x => new AllDomainsResponse(x.Id, x.Name))
        .ToListAsync();

        return Ok(domains);
    }
}