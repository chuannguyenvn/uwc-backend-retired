using Newtonsoft.Json;

namespace uwc_backend.Communications.Mcp;

public class GetMcpsInRangeRequest
{
    [JsonProperty("Latitude")] public double Latitude { get; set; }
    [JsonProperty("Longitude")] public double Longitude { get; set; }
    [JsonProperty("Radius")] public double Radius { get; set; }
}