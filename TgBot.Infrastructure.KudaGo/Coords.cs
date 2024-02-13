using System.Text.Json.Serialization;

namespace Infrastructure.KudaGo;

public class Coords
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}