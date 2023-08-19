using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class UpdateDriveInformationRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Date")] public DateTime Date { get; set; }
        [JsonProperty("Driver")] public int Driver { get; set; }
        [JsonProperty("Vehicle")] public int Vehicle { get; set; }
    }
}