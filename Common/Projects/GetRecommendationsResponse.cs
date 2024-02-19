namespace YatraBackend.Common.Projects;

public class GetRecommendationsResponse
{
    public GetProjectDetailResponse Data { get; set; }
    public List<GetProjectDetailResponse> Recommendations { get; set; }
}