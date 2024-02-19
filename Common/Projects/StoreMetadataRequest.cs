using System.Text.Json.Serialization;

namespace YatraBackend.Common.Projects;

public class StoreMetadataRequest
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
   
    [JsonPropertyName("domainId")]
    public Guid DomainId { get; set; }
}