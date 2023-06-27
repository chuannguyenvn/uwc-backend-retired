using Newtonsoft.Json;

namespace uwc_backend.Communications.Task;

public class AddTaskIncludeMcpRequest
{
    [JsonProperty("Task")] public int Task { get; set; }
    [JsonProperty("Mcp")] public int Mcp { get; set; }
}