namespace YatraBackend.Database.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsActive { get; set; } = true;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}