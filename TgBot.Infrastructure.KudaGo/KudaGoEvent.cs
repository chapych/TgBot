using System.Text.Json.Serialization;

namespace Infrastructure.KudaGo;

public class KudaGoEvent
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")] 
    public string Name { get; set; } = null!;
    [JsonPropertyName("dates")]
    public List<KudaGoEventTime>? Dates { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("place")]
    public KudaGoPlace? Place { get; set; }

}