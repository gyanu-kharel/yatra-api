using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YatraBackend.Common.Configs;
using YatraBackend.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace YatraBackend.Services;

public class JwtTokenGenerator(IOptions<JwtConfigs> jwtConfigs) : IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string fullName, string email)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfigs.Value.Secret!)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, fullName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, userId.ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: jwtConfigs.Value.Issuer,
            audience: jwtConfigs.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(jwtConfigs.Value.ExpiryInMinutes),
            signingCredentials: signingCredentials,
            claims: claims);


        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}