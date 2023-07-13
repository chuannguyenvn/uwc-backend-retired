using Newtonsoft.Json;

namespace Communications.Mcp
{
    public class UpdateLoadRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("CurrentLoad")] public float CurrentLoad { get; set; }
    }
}