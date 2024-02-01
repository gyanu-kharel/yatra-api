namespace YatraBackend.Database.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid DomainId { get; set; }
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
    
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Domain Domain { get; set; }
}