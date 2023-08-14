using Newtonsoft.Json;
using Repositories;
using Types;

namespace Services.LiveData;

public class VehicleLocationService : IHostedService, IDisposable
{
    private const string MAPBOX_DIRECTION_API =
        "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/{0},{1};{2},{3}?geometries=geojson&access_token=pk.eyJ1IjoiY2h1YW4tbmd1eWVudm4iLCJhIjoiY2xsYTkycjJoMGg1MjNxbGhhcW5mMzNuOCJ9.tpAt14HVH_j1IKuKxsK31A";

    private readonly IServiceProvider _serviceProvider;
    private Timer? _botMovementTimer;

    private readonly Dictionary<int, VehicleLocationData> _vehicleLocationDataById = new();

    public VehicleLocationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispose()
    {
        _botMovementTimer?.Dispose();
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        RetrieveVehicleIds();

        _botMovementTimer = new Timer(MoveBots, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    private void RetrieveVehicleIds()
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        foreach (var vehicle in unitOfWork.Vehicles.GetAll())
        {
            var data = new VehicleLocationData {CurrentLocation = new Coordinate(10.770486, 106.658127), IsBot = true};
            _vehicleLocationDataById.Add(vehicle.Id, data);
        }
    }

    private void MoveBots(object? state)
    {
        var random = new Random();

        Parallel.ForEach(_vehicleLocationDataById.Values,
            locationData =>
            {
                // if (!locationData.IsBot) return;
                //
                // if (locationData.Waypoints.Count == 0)
                // {
                //     var mostFullMcp = GetRandomMcp();
                //     locationData.TargettingMcp = mostFullMcp;
                //
                //     var mcpCoordinate = new Coordinate(mostFullMcp.Latitude, mostFullMcp.Longitude);
                //     var newRoute = RequestMapboxDirection(locationData.CurrentLocation, mcpCoordinate);
                //     locationData.Waypoints = ProcessDirectionResponse(newRoute);
                // }
                //
                // var distanceLeft = 0.0000005;
                // while (distanceLeft > 0 && locationData.Waypoints.Count > 0)
                // {
                //     var currentLocation = locationData.CurrentLocation;
                //     var nextWaypointLocation = locationData.Waypoints[0];
                //     locationData.Waypoints.RemoveAt(0);
                //
                //     var distanceToNextWaypoint = currentLocation.DistanceTo(nextWaypointLocation);
                //     if (distanceToNextWaypoint > distanceLeft)
                //     {
                //         var t = Math.Clamp(distanceLeft / distanceToNextWaypoint, 0, 1);
                //         locationData.CurrentLocation = Coordinate.Lerp(currentLocation, nextWaypointLocation, t);
                //         EmptyMcp(locationData.TargettingMcp.Id);
                //         break;
                //     }
                //
                //     distanceLeft -= distanceToNextWaypoint;
                // }

                locationData.CurrentLocation.Latitude += (random.NextDouble() - 0.5) * 0.0001;
                locationData.CurrentLocation.Longitude += (random.NextDouble() - 0.5) * 0.0001;
            });
    }

    private MapboxDirectionResponse RequestMapboxDirection(Coordinate fromLocation, Coordinate toLocation)
    {
        var client = new HttpClient();
        var httpResponse = client.GetStringAsync(ConstructMapboxDirectionRequest(fromLocation, toLocation)).Result;
        var mapboxDirectionResponse = JsonConvert.DeserializeObject<MapboxDirectionResponse>(httpResponse);
        return mapboxDirectionResponse;
    }

    private List<Coordinate> ProcessDirectionResponse(MapboxDirectionResponse response)
    {
        var coordinates = new List<Coordinate>();

        foreach (var waypoint in response.Waypoints) coordinates.Add(new Coordinate(waypoint.Location[1], waypoint.Location[0]));

        return coordinates;
    }

    private Models.Mcp GetRandomMcp()
    {
        using var scope = _serviceProvider.CreateScope();
        var mcpCapacityService = scope.ServiceProvider.GetRequiredService<McpCapacityService>();
        return mcpCapacityService.GetRandomMcp();
    }

    private void EmptyMcp(int mcpId)
    {
        using var scope = _serviceProvider.CreateScope();
        var mcpCapacityService = scope.ServiceProvider.GetRequiredService<McpCapacityService>();
        mcpCapacityService.EmptyMcp(mcpId);
    }

    private string ConstructMapboxDirectionRequest(Coordinate currentLocation, Coordinate destinationLocation)
    {
        return string.Format(MAPBOX_DIRECTION_API,
            currentLocation.Longitude,
            currentLocation.Latitude,
            destinationLocation.Longitude,
            destinationLocation.Latitude);
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

        foreach (var (id, data) in _vehicleLocationDataById) coordinatesByVehicleId.Add(id, data.CurrentLocation);

        return (true, coordinatesByVehicleId);
    }

    private class VehicleLocationData
    {
        public Coordinate CurrentLocation;
        public bool IsBot = true;
        [JsonIgnore] public Models.Mcp TargettingMcp;
        public List<Coordinate> Waypoints = new();
    }
}