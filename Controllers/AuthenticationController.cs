using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Common.Authentication;
using YatraBackend.Common.Constants;
using YatraBackend.Database;
using YatraBackend.Database.Models;
using YatraBackend.Services.Interfaces;

namespace YatraBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController(
    ApplicationDbContext dbContext,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingEmail = await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));

        if (existingEmail is not null)
            throw new ValidationException("Email already exists");

        var user = new User()
        {
            Email = request.Email,
            FullName = request.FullName,
        };
        
        var role = await dbContext
            .Roles
            .FirstOrDefaultAsync(x => x.Name == RoleConstants.User);

        user.RoleId = role!.Id;
        
        var hashResult = passwordHasher.HashPassword(request.Password);

        user.Password = hashResult.Item1;
        user.Salt = hashResult.Item2;

        await dbContext.Users.AddAsync(user);
        
        var token = jwtTokenGenerator.GenerateToken(user.Id, user.FullName, user.Email);

        return Ok(new AuthenticationResponse(
            user.Id,
            user.FullName,
            user.Email,
            token));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));

        if (user is null)
            throw new ValidationException("Incorrect credentials");

        var checkPassword = passwordHasher.VerifyPassword(
            user.Password,
            request.Password,
            user.Salt);

        if (!checkPassword)
        {
            throw new ValidationException("Incorrect credentials");
        }

        var token = jwtTokenGenerator.GenerateToken(user.Id, user.FullName, user.Email);

        return Ok(new AuthenticationResponse(
            user.Id,
            user.FullName,
            user.Email,
            token));
    }
}