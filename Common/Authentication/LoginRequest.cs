namespace YatraBackend.Common.Authentication;


public record LoginRequest(
    string Email,
    string Password);