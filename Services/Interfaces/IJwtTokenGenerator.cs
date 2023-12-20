namespace YatraBackend.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string fullName, string email);
}