using Newtonsoft.Json;

namespace Communications.Task
{
    public class GetTasksInTimeRangeRequest
    {
        [JsonProperty("StartTime")] public DateTime StartTime { get; set; }
        [JsonProperty("EndTime")] public DateTime EndTime { get; set; }
    }
}