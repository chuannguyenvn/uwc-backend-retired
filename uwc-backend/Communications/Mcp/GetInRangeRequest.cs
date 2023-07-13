using Newtonsoft.Json;

namespace Communications.Mcp
{
    public class GetInRangeRequest
    {
        [JsonProperty("Latitude")] public double Latitude { get; set; }
        [JsonProperty("Longitude")] public double Longitude { get; set; }
        [JsonProperty("Radius")] public double Radius { get; set; }
    }
}