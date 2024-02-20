namespace YatraBackend.Common.Projects;

public record GetLatestProjectResponse(
    Guid Id,
    string Domain,
    string Title,
    DateTime ProjectYear,
    string Owner,
    int FavoriteCount,
    int ViewCount);