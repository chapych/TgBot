using System.Text.Json.Serialization;

namespace Infrastructure.KudaGo;

public class ResponseData
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("next")]
    public string? Next { get; set; }
    [JsonPropertyName("previous")]
    public string? Previous { get; set;}
    [JsonPropertyName("results")]
    public List<KudaGoEvent> Events { get; set; } = null!;
}