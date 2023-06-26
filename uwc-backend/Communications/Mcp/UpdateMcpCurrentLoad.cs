using Newtonsoft.Json;

namespace uwc_backend.Communications.Mcp;

public class UpdateMcpCurrentLoad
{
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("CurrentLoad")] public double CurrentLoad { get; set; }
}