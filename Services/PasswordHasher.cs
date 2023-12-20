using System.Security.Cryptography;
using System.Text;
using YatraBackend.Services.Interfaces;

namespace YatraBackend.Services;

public class PasswordHasher : IPasswordHasher
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    
    public (string, string) HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256, 
            KeySize);
        
        return (Convert.ToHexString(hash), Convert.ToHexString(salt));
    }

    public bool VerifyPassword(string hashedPassword, string checkPassword, string salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            checkPassword,
            Convert.FromHexString(salt), 
            Iterations, HashAlgorithmName.SHA256,
            KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashedPassword));
    }
}