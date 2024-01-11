namespace YatraBackend.Common.Projects;

public record CreateProjectRequest(
    Guid Id,
    string Title,
    string Description,
    Guid DomainId,
    int Duration,
    int TeamSize,
    string SkillLevel,
    string Complexity);
