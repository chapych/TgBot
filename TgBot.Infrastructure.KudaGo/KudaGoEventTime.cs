using System.Text.Json.Serialization;

namespace Infrastructure.KudaGo;

public class KudaGoEventTime
{
    [JsonPropertyName("start")]
    public long? Start { get; set; }

    [JsonPropertyName("end")]
    public long? End { get; set; }
}