using Newtonsoft.Json;

namespace Communications.Mcp
{
    public class UpdateMcpCurrentLoad
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("CurrentLoad")] public float CurrentLoad { get; set; }
    }
}