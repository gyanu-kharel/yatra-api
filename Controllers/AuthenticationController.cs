using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YatraBackend.Common.Authentication;
using YatraBackend.Common.Constants;
using YatraBackend.Database;
using YatraBackend.Database.Models;
using YatraBackend.Services.Interfaces;
using YatraBackend.Common.Exceptions;

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
        // check if email is already in use
        var existingEmail = await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));

        if (existingEmail is not null)
            throw new ValidationException("Email already exists");

        // create new user object
        var user = new User()
        {
            Email = request.Email,
            FullName = request.FullName,
        };
        
        // assign "User" role to the user
        var role = await dbContext
            .Roles
            .FirstOrDefaultAsync(x => x.Name == RoleConstants.User);

        user.RoleId = role!.Id;
        
        // hash the plain text password
        var hashResult = passwordHasher.HashPassword(request.Password);

        user.Password = hashResult.Item1;
        user.Salt = hashResult.Item2;

        // save to the database
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        
        //generate token upon successful registration
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
        // check if email is correct and user exists
        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));

        if (user is null)
            throw new ValidationException("Incorrect credentials");

        // check if the password provided by user is correct
        // check the hash
        var checkPassword = passwordHasher.VerifyPassword(
            user.Password,
            request.Password,
            user.Salt);

        if (!checkPassword)
        {
            throw new ValidationException("Incorrect credentials");
        }

        //generate token upon successful login
        var token = jwtTokenGenerator.GenerateToken(user.Id, user.FullName, user.Email);

        return Ok(new AuthenticationResponse(
            user.Id,
            user.FullName,
            user.Email,
            token));
    }
}