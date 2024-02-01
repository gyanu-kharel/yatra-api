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
            Complexity = request.Complexity,
            ProjectYear = request.ProjectYear.ToUniversalTime(),
            Platform = request.Platform,
            UiDesignLink = request.UiDesignLink,
            GithubLink = request.GithubLink,
            UserId = request.CreatedBy
        };
        try
        {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }
        return Ok(project.Id);
    }

    [HttpGet("latest")]
    public async Task<IActionResult> GetLatest()
    {
        var latest = await dbContext
            .Projects
            .OrderByDescending(x => x.ProjectYear)
            .Select(y => new GetLatestProjectResponse(
                y.Id,
                y.Domain.Name,
                y.Title,
                y.ProjectYear,
                y.User.FullName))
            .Take(5)
            .ToListAsync();

        return Ok(latest);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var project = await dbContext.Projects
            .Where(x => x.Id == id)
            .Select(y => new GetProjectDetailResponse(
                y.Id,
                y.Title,
                y.Description,
                y.DomainId,
                y.Domain.Name,
                y.Duration,
                y.TeamSize,
                y.SkillLevel,
                y.Complexity,
                y.ProjectYear,
                y.User.FullName
            ))
            .FirstOrDefaultAsync();
            
        return Ok(project);
    }
}