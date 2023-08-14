using Newtonsoft.Json;

namespace Communications.Task;

public class UpdateTaskRequest
{
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("Date")] public DateTime Date { get; set; }
    [JsonProperty("Supervisor")] public int SupervisorId { get; set; }
    [JsonProperty("Worker")] public int WorkerId { get; set; }
    [JsonProperty("Route")] public int RouteId { get; set; }
}