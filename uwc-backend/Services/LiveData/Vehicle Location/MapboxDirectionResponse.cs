using Newtonsoft.Json;

public class Admin
{
    [JsonProperty("iso_3166_1")] public string Iso31661 { get; set; }
}

public class Geometry
{
    [JsonProperty("coordinates")] public List<List<double>> Coordinates { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class Leg
{
    [JsonProperty("via_waypoints")] public List<object> ViaWaypoints { get; set; }

    [JsonProperty("admins")] public List<Admin> Admins { get; set; }

    [JsonProperty("weight_typical")] public double WeightTypical { get; set; }

    [JsonProperty("duration_typical")] public double DurationTypical { get; set; }

    [JsonProperty("weight")] public double Weight { get; set; }

    [JsonProperty("duration")] public double Duration { get; set; }

    [JsonProperty("steps")] public List<object> Steps { get; set; }

    [JsonProperty("distance")] public double Distance { get; set; }

    [JsonProperty("summary")] public string Summary { get; set; }
}

public class MapboxDirectionResponse
{
    [JsonProperty("routes")] public List<Route> Routes { get; set; }

    [JsonProperty("waypoints")] public List<Waypoint> Waypoints { get; set; }

    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("uuid")] public string Uuid { get; set; }
}

public class Route
{
    [JsonProperty("weight_typical")] public double WeightTypical { get; set; }

    [JsonProperty("duration_typical")] public double DurationTypical { get; set; }

    [JsonProperty("weight_name")] public string WeightName { get; set; }

    [JsonProperty("weight")] public double Weight { get; set; }

    [JsonProperty("duration")] public double Duration { get; set; }

    [JsonProperty("distance")] public double Distance { get; set; }

    [JsonProperty("legs")] public List<Leg> Legs { get; set; }

    [JsonProperty("geometry")] public Geometry Geometry { get; set; }
}

public class Waypoint
{
    [JsonProperty("distance")] public double Distance { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("location")] public List<double> Location { get; set; }
}