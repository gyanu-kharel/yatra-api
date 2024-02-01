namespace YatraBackend.Common.Projects;

public record GetProjectDetailResponse(
    Guid Id,
    string Title,
    string Description,
    Guid DomainId,
    string Domain,
    int Duration,
    int TeamSize,
    string SkillLevel,
    string Complexity,
    DateTime ProjectYear,
    string Owner);