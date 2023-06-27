using Newtonsoft.Json;

namespace uwc_backend.Communications.Vehicle;

public class AddDriveRequest
{
    [JsonProperty("Date")] public DateTime Date { get; set; }
    [JsonProperty("Driver")] public int Driver { get; set; }
    [JsonProperty("Vehicle")] public int Vehicle { get; set; }
}