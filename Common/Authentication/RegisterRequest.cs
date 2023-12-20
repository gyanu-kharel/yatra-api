namespace YatraBackend.Common.Authentication;

public record RegisterRequest(
    string FullName,
    string Email,
    string Password);