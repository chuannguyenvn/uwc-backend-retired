using Newtonsoft.Json;

namespace Communications.Vehicle
{
    public class AddVehicleRequest
    {
        [JsonProperty("Capacity")] public double Capacity { get; set; }
        [JsonProperty("CurrentLoad")] public double CurrentLoad { get; set; }
        [JsonProperty("AverageSpeed")] public double AverageSpeed { get; set; }
    }
}