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
    public virtual Domain Domain { get; set; }
}