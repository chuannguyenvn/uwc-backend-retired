using Newtonsoft.Json;

namespace Communications.Task
{
    public class UpdateTaskRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Date")] public DateTime Date { get; set; }
        [JsonProperty("Supervisor")] public int Supervisor { get; set; }
        [JsonProperty("Worker")] public int Worker { get; set; }
        [JsonProperty("Route")] public int Route { get; set; }
    }
}