using Newtonsoft.Json;
using Types;

namespace Communications.Vehicle
{
    public class GetAllVehicleLocationResponse
    {
        [JsonProperty("Result")] public Dictionary<int, Coordinate> Result { get; set; }
    }
}