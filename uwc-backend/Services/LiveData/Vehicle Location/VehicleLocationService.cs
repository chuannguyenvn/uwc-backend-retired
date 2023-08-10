using Newtonsoft.Json;
using Repositories;

namespace Services.LiveData;

public class VehicleLocationService : IHostedService, IDisposable
{
    private class VehicleLocationData
    {
        public Coordinate CurrentLocation = new();
        public bool IsBot = true;
        public List<Coordinate> Waypoints = new();
        [JsonIgnore] public Models.Mcp TargettingMcp;
    }

    private const string MAPBOX_DIRECTION_API =
        "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/{0},{1};{2},{3}?geometries=geojson&access_token=pk.eyJ1IjoiY2h1YW5wcm8wMzAiLCJhIjoiY2xhcG51ZWg5MDFqbTNwb2FlaW52MXNvciJ9.kFN5GOg3C8cGlk2PN4Tleg";

    private Dictionary<int, VehicleLocationData> _vehicleLocationDataById = new();
    private Timer? _botMovementTimer;
    private readonly IServiceProvider _serviceProvider;

    public VehicleLocationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        RetrieveVehicleIds();

        _botMovementTimer = new Timer(MoveBots, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void RetrieveVehicleIds()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        foreach (var vehicle in unitOfWork.Vehicles.GetAll())
        {
            var data = new VehicleLocationData {CurrentLocation = new Coordinate(10.770486, 106.658127), IsBot = true};
            _vehicleLocationDataById.Add(vehicle.Id, data);
        }
    }

    private void MoveBots(object? state)
    {
        Parallel.ForEach(_vehicleLocationDataById.Values,
            locationData =>
            {
                if (!locationData.IsBot) return;

                if (locationData.Waypoints.Count == 0)
                {
                    var mostFullMcp = GetRandomMcp();
                    locationData.TargettingMcp = mostFullMcp;

                    var mcpCoordinate = new Coordinate(mostFullMcp.Latitude, mostFullMcp.Longitude);
                    var newRoute = RequestMapboxDirection(locationData.CurrentLocation, mcpCoordinate);
                    locationData.Waypoints = ProcessDirectionResponse(newRoute);
                }

                var distanceLeft = 0.00005;
                while (distanceLeft > 0 && locationData.Waypoints.Count > 0)
                {
                    var currentLocation = locationData.CurrentLocation;
                    var nextWaypointLocation = locationData.Waypoints[0];
                    locationData.Waypoints.RemoveAt(0);

                    var distanceToNextWaypoint = currentLocation.DistanceTo(nextWaypointLocation);
                    if (distanceToNextWaypoint > distanceLeft)
                    {
                        var t = Math.Clamp(distanceLeft / distanceToNextWaypoint, 0, 1);
                        locationData.CurrentLocation = Coordinate.Lerp(currentLocation, nextWaypointLocation, t);
                        break;
                    }

                    distanceLeft -= distanceToNextWaypoint;
                }
            });
    }

    private MapboxDirectionResponse RequestMapboxDirection(Coordinate fromLocation, Coordinate toLocation)
    {
        HttpClient client = new HttpClient();
        var httpResponse = client.GetStringAsync(ConstructMapboxDirectionRequest(fromLocation, toLocation)).Result;
        var mapboxDirectionResponse = JsonConvert.DeserializeObject<MapboxDirectionResponse>(httpResponse);
        return mapboxDirectionResponse;
    }

    private List<Coordinate> ProcessDirectionResponse(MapboxDirectionResponse response)
    {
        var coordinates = new List<Coordinate>();

        foreach (var waypoint in response.Waypoints)
        {
            coordinates.Add(new Coordinate(waypoint.Location[1], waypoint.Location[0]));
        }

        return coordinates;
    }

    private Models.Mcp GetRandomMcp()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var mcpCapacityService = scope.ServiceProvider.GetRequiredService<McpCapacityService>();
        return mcpCapacityService.GetRandomMcp();
    }

    private string ConstructMapboxDirectionRequest(Coordinate currentLocation, Coordinate destinationLocation)
    {
        return String.Format(MAPBOX_DIRECTION_API,
            currentLocation.Longitude,
            currentLocation.Latitude,
            destinationLocation.Longitude,
            destinationLocation.Latitude);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public bool UpdateVehicleLocation(int vehicleId, Coordinate newCoordinate)
    {
        _vehicleLocationDataById[vehicleId].CurrentLocation = newCoordinate;
        _vehicleLocationDataById[vehicleId].IsBot = false;
        return true;
    }

    public (bool success, Dictionary<int, Coordinate> result) GetAllVehicleLocations()
    {
        var coordinatesByVehicleId = new Dictionary<int, Coordinate>();

        foreach (var (id, data) in _vehicleLocationDataById)
        {
            coordinatesByVehicleId.Add(id, data.CurrentLocation);
        }

        return (true, coordinatesByVehicleId);
    }

    public void Dispose()
    {
        _botMovementTimer?.Dispose();
    }
}