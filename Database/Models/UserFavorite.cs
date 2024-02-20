namespace YatraBackend.Database.Models;

public class UserFavorite
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;
    
    public User User { get; set; }
    public Project Project { get; set; }
}