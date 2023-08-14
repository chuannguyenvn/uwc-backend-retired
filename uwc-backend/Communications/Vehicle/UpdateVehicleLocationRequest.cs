using Newtonsoft.Json;
using Types;

namespace Communications.Vehicle
{
    public class UpdateVehicleLocationRequest
    {
        [JsonProperty("Coordinate")] public Coordinate Coordinate { get; set; }
    }
}