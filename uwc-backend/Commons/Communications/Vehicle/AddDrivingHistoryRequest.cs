using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class AddDrivingHistoryRequest
    {
        [JsonProperty("Date")] public DateTime Date { get; set; }
        [JsonProperty("Driver")] public int Driver { get; set; }
        [JsonProperty("Vehicle")] public int Vehicle { get; set; }
    }
}