using YatraBackend.Database.Models;

namespace YatraBackend.Common.Projects;

public class GetProjectDetailResponse
{
    public GetProjectDetailResponse()
    {
        
    }
    public GetProjectDetailResponse(Project project)
    {
        Id = project.Id;
        Title = project.Title;
        Description = project.Description;
        DomainId = project.DomainId;
        Domain = project.Domain.Name;
        Duration = project.Duration;
        TeamSize = project.TeamSize;
        SkillLevel = project.SkillLevel;
        Complexity = project.Complexity;
        ProjectYear = project.ProjectYear;
        Platform = project.Platform;
        UiDesignLink = project.UiDesignLink;
        GithubLink = project.GithubLink;
        ScreenshotUrl = project.ScreenshotUrl;
        DocumentationUrl = project.DocumentationUrl;
        FavoriteCount = project.FavoriteCount;
        ViewCount = project.ViewCount;
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid DomainId { get; set; }
    public string Domain { get; set; }
    public int Duration { get; set; }
    public int TeamSize { get; set; }
    public string SkillLevel { get; set; }
    public string Complexity { get; set; }
    public DateTime ProjectYear { get; set; }
    public string Platform { get; set; }
    public string? UiDesignLink { get; set; }
    public string? GithubLink { get; set; }
    public string? ScreenshotUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public int FavoriteCount { get; set; }
    public int ViewCount { get; set; }
    public bool? IsFavorite { get; set; }
}