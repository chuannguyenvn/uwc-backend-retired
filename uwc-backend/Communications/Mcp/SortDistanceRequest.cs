using Newtonsoft.Json;

namespace uwc_backend.Communications.Mcp;

public class SortDistanceRequest
{
    [JsonProperty("Latitude")] public double Latitude { get; set; }
    [JsonProperty("Longitude")] public double Longitude { get; set; }
}