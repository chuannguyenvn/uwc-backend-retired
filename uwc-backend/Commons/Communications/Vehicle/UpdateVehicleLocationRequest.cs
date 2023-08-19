using Commons.Types;
using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class UpdateVehicleLocationRequest
    {
        [JsonProperty("Coordinate")] public Coordinate Coordinate { get; set; }
    }
}