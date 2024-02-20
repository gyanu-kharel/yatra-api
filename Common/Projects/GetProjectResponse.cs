namespace YatraBackend.Common.Projects;

public record GetProjectResponse(
    Guid Id,
    string Title,
    string Description,
    string Domain,
    int Duration,
    int TeamSize,
    string SkillLevel,
    string Complexity,
    int FavoriteCount,
    int ViewCount,
    bool? IsFavorite);