namespace YatraBackend.Common.Projects;

public record CreateProjectRequest(
    string Title,
    string Description,
    Guid DomainId,
    int Duration,
    int TeamSize,
    string SkillLevel,
    string Complexity,
    DateTime ProjectYear,
    string Platform,
    string? UiDesignLink,
    string? GithubLink,
    Guid CreatedBy);
