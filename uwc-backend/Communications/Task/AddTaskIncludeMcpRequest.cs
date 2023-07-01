using Newtonsoft.Json;

namespace Communications.Task
{
    public class AddTaskIncludeMcpRequest
    {
        [JsonProperty("Task")] public int Task { get; set; }
        [JsonProperty("Mcp")] public int Mcp { get; set; }
    }
}