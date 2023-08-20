using Newtonsoft.Json;

namespace Commons.Communications.Task
{
    public class AddTaskRequest
    {
        [JsonProperty("Date")] public DateTime Date { get; set; }
        [JsonProperty("Supervisor")] public int SupervisorId { get; set; }
        [JsonProperty("Worker")] public int WorkerId { get; set; }
        [JsonProperty("McpIds")] public List<int> McpIds { get; set; } = new();
    }
}