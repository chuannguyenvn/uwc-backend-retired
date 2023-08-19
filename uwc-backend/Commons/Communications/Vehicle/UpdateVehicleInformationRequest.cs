using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class UpdateVehicleInformationRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Capacity")] public double Capacity { get; set; }
        [JsonProperty("CurrentLoad")] public double CurrentLoad { get; set; }
        [JsonProperty("AverageSpeed")] public double AverageSpeed { get; set; }
    }
}