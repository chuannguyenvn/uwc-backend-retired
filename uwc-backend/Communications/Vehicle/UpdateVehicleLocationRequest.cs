using Newtonsoft.Json;
using Services.LiveData;

namespace Communications.Vehicle;

public class UpdateVehicleLocationRequest
{
    [JsonProperty("Coordinate")] public Coordinate Coordinate { get; set; }
}