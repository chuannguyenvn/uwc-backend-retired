using Newtonsoft.Json;

namespace Communications.Mcp
{
    public class AddMcpRequest
    {
        [JsonProperty("Capacity")] public float Capacity { get; set; }
        [JsonProperty("CurrentLoad")] public float CurrentLoad { get; set; }
        [JsonProperty("Latitude")] public double Latitude { get; set; }
        [JsonProperty("Longitude")] public double Longitude { get; set; }
    }
}