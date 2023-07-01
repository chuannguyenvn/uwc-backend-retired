using Newtonsoft.Json;

namespace Communications.Route
{
    public class AddRouteRequest
    {
        [JsonProperty("RouteName")] public string RouteName { get; set; }
        [JsonProperty("TotalLength")] public double TotalLength { get; set; }
        [JsonProperty("RouteDetails")] public string RouteDetails { get; set; }
    }
}