namespace YatraBackend.Database.Models;

public class Domain
{ 
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsActive { get; set; } = true;
}