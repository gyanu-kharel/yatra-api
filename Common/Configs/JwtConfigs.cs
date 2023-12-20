namespace YatraBackend.Common.Configs;

public class JwtConfigs
{
    public const string SectionName = "JwtConfigs";
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpiryInMinutes { get; set; }
    public string? Secret { get; set; }
}