using Newtonsoft.Json;

namespace Commons.Communications.Route
{
    public class UpdateRouteRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("RouteName")] public string RouteName { get; set; }
        [JsonProperty("TotalLength")] public double TotalLength { get; set; }
        [JsonProperty("RouteDetails")] public string RouteDetails { get; set; }
    }
}