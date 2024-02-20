using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Common.Projects;
using YatraBackend.Database;
using YatraBackend.Database.Models;
using YatraBackend.Services;
using YatraBackend.Services.Interfaces;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(ApplicationDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var token =  Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token is null)
            return BadRequest();

        var userId = jwtTokenGenerator.ParseToken(token);
        
        var projects = await dbContext.Projects
            .Select(x => new GetProjectResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Domain.Name,
                    x.Duration,
                    x.TeamSize,
                    x.SkillLevel,
                    x.Complexity,
                    x.FavoriteCount,
                    x.ViewCount,
                    null
                )
            )
            .ToListAsync();

        var userFavs = await dbContext.UserFavorites
            .Where(x => x.UserId == userId)
            .Select(y => y.ProjectId)
            .ToListAsync();

        var result = new List<GetProjectResponse>();
        foreach (var project in projects)
        {
            if (userFavs.Contains(project.Id))
            {
                result.Add(project with {IsFavorite = true});
            }
            else
            {
                result.Add((project with {IsFavorite = false}));
            }
        }
        return Ok(result);
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

        var domain = await dbContext.Domains
            .FirstOrDefaultAsync(x => x.Id == request.DomainId);

        var filteredKeywords = MLHelper.FilterKeywords(request.Description, domain.Metadata);
        
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        var metaData = new Metadata()
        {
            Id = project.Id,
            Content = filteredKeywords.ToList()
        };

        await dbContext.Metadatas.AddAsync(metaData);
        await dbContext.SaveChangesAsync();
            
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
                y.User.FullName,
                y.FavoriteCount,
                y.ViewCount)
            )
            .Take(5)
            .ToListAsync();
        
       
        return Ok(latest);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetail(Guid id)
    {  
        var token =  Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token is null)
            return BadRequest();

        var userId = jwtTokenGenerator.ParseToken(token);
        var result = new GetRecommendationsResponse();
        
        var project = await dbContext.Projects
            .Where(x => x.Id == id)
            .Include(x => x.Domain)
            .FirstOrDefaultAsync();

        var userFav = await dbContext.UserFavorites
            .Where(x => x.UserId == userId && x.ProjectId == project.Id)
            .ToListAsync();

        result.Data = new GetProjectDetailResponse(project);

        result.Data.IsFavorite = userFav.Count > 0;

        project.ViewCount += 1;

        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
        
        var query = await dbContext.Metadatas
            .FirstOrDefaultAsync(x => x.Id == id);

        var metadata = await dbContext.Metadatas
            .Where(x => x.Id != id)
            .ToListAsync();
        var recommendationIds = MLHelper.Recommend(query, metadata).Select(x => x.Id);

        var recommendations = await dbContext.Projects
            .Where(x => recommendationIds.Contains(x.Id))
            .Select(y => new GetProjectDetailResponse(y))
            .ToListAsync();

        result.Recommendations = recommendations;
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var project = await dbContext.Projects
            .FirstOrDefaultAsync(x => x.Id == id);

        if (project is null) return BadRequest();
        
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("favorite/{id:guid}")]
    public async Task<IActionResult> Favorite(Guid id)
    {
        var token =  Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token is null)
            return BadRequest();

        var userId = jwtTokenGenerator.ParseToken(token);

        var project = await dbContext.Projects
            .FirstOrDefaultAsync(x => x.Id == id);

        var isFavorited = (await dbContext.UserFavorites
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == id))!;

        if (isFavorited is null)
        {
            var userFav = new UserFavorite()
            {
                UserId = userId,
                ProjectId = id,
                Date = DateTime.Now.ToUniversalTime()
            };

            project.FavoriteCount += 1;
            dbContext.UserFavorites.Add(userFav);
            dbContext.Projects.Update(project);
        }
        else
        {
            project.FavoriteCount -= 1;
            dbContext.UserFavorites.Remove(isFavorited);
        }
        
        await dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Popular()
    {
        var popular = await dbContext
            .Projects
            .OrderByDescending(x => x.FavoriteCount + x.ViewCount)
            .Select(y => new GetLatestProjectResponse(
                y.Id,
                y.Domain.Name,
                y.Title,
                y.ProjectYear,
                y.User.FullName,
                y.FavoriteCount,
                y.ViewCount)
            )
            .Take(5)
            .ToListAsync();
       
        return Ok(popular);
    }
}