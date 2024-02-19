using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YatraBackend.Common.Configs;
using YatraBackend.Common.Ideas;
using YatraBackend.Database;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdeasController(
    IOptions<OpenAiConfigs> openAiConfigs,
    ApplicationDbContext dbContext
    ) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> GenerateIdea(GenerateIdeaRequest data)
    {
        var domain = await dbContext.Domains
            .FirstOrDefaultAsync(x => x.Id == data.DomainId);

        var request = $"Give 5 project ideas for '{domain?.Name}' ,which can be completed in {data.Duration} months " +
                      $"for team size of around {data.TeamSize} members, with skill level of {data.SkillLevel}." +
                      $"The complexity of project should be {data.Complexity}.";
                      
        return Ok("This is response from ChatGPT");
    }
}