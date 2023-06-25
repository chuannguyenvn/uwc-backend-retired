using Newtonsoft.Json;

namespace uwc_backend.Communications.Task;

public class AddTaskRequest
{
    [JsonProperty("Date")] public DateTime Date { get; set; }
    [JsonProperty("Supervisor")] public int Supervisor { get; set; }
    [JsonProperty("Worker")] public int Worker { get; set; }
    [JsonProperty("Route")] public int Route { get; set; }
}