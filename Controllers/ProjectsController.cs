using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Common.Projects;
using YatraBackend.Database;
using YatraBackend.Database.Models;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var projects = await dbContext.Projects
            .Select(x => new GetProjectResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Domain.Name,
                    x.Duration,
                    x.TeamSize,
                    x.SkillLevel,
                    x.Complexity
                )
            )
            .ToListAsync();

        return Ok(projects);
    }

    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
    {
        var project = new Project()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DomainId = request.DomainId,
            Duration = request.Duration,
            TeamSize = request.TeamSize,
            SkillLevel = request.SkillLevel,
            Complexity = request.Complexity
        };

        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        return Ok(project.Id);
    }
}