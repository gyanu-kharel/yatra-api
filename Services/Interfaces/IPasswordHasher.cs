namespace YatraBackend.Services.Interfaces;

public interface IPasswordHasher
{
    (string, string) HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string checkPassword, string salt);
}