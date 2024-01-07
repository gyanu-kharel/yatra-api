namespace YatraBackend.Common.Ideas;

public record GenerateIdeaRequest(
    Guid DomainId,
    int Duration,
    int TeamSize,
    string SkillLevel,
    string Complexity);