using Newtonsoft.Json;
using Repositories;
using Commons;
using Commons.Types;

namespace Services.LiveData;

public partial class VehicleLocationService : IHostedService, IDisposable
{
    private const string MAPBOX_DIRECTION_API =
        "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/{0},{1};{2},{3}?geometries=geojson&access_token=pk.eyJ1IjoiY2h1YW4tbmd1eWVudm4iLCJhIjoiY2xsYTkycjJoMGg1MjNxbGhhcW5mMzNuOCJ9.tpAt14HVH_j1IKuKxsK31A";

    private readonly IServiceProvider _serviceProvider;
    private Timer? _botMovementTimer;

    private readonly Dictionary<int, VehicleMovementData> _vehicleLocationDataById = new();

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
            var data = new VehicleMovementData {CurrentLocation = new Coordinate(10.770486, 106.658127), IsBot = true};
            _vehicleLocationDataById.Add(vehicle.Id, data);
            break;
        }
    }

    private void MoveBots(object? state)
    {
        Parallel.ForEach(_vehicleLocationDataById.Values,
            locationData =>
            {
                if (!locationData.IsBot) return;

                if (locationData.MapboxDirectionResponse == null || locationData.MapboxDirectionResponse.Waypoints.Count == 0)
                {
                    var mostFullMcp = GetRandomMcp();
                    locationData.TargettingMcp = mostFullMcp;

                    var mcpCoordinate = new Coordinate(mostFullMcp.Latitude, mostFullMcp.Longitude);
                    locationData.MapboxDirectionResponse = RequestMapboxDirection(locationData.CurrentLocation, mcpCoordinate);
                }

                var distanceLeft = 0.001;
                while (distanceLeft > 0 && locationData.MapboxDirectionResponse.Waypoints.Count > 0)
                {
                    var currentLocation = locationData.CurrentLocation;
                    var nextWaypointLocation = new Coordinate(locationData.MapboxDirectionResponse.Waypoints[0].Location);

                    var distanceToNextWaypoint = currentLocation.DistanceTo(nextWaypointLocation);
                    if (distanceToNextWaypoint > distanceLeft)
                    {
                        var t = Math.Clamp(distanceLeft / distanceToNextWaypoint, 0, 1);
                        locationData.CurrentLocation = Coordinate.Lerp(currentLocation, nextWaypointLocation, t);
                        locationData.CurrentOrientationAngle = (float)(Math.Atan2(nextWaypointLocation.Latitude - currentLocation.Latitude,
                            nextWaypointLocation.Longitude - currentLocation.Longitude) * 180 / Math.PI);

                        EmptyMcp(locationData.TargettingMcp.Id);
                        break;
                    }

                    locationData.MapboxDirectionResponse.Waypoints.RemoveAt(0);
                    distanceLeft -= distanceToNextWaypoint;
                }
            });
    }

    private MapboxDirectionResponse RequestMapboxDirection(Coordinate fromLocation, Coordinate toLocation)
    {
        Console.WriteLine("RequestMapboxDirection");
        var client = new HttpClient();
        var httpResponse = client.GetStringAsync(ConstructMapboxDirectionRequest(fromLocation, toLocation)).Result;
        var mapboxDirectionResponse = JsonConvert.DeserializeObject<MapboxDirectionResponse>(httpResponse);
        return mapboxDirectionResponse;
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

    public (bool success, Dictionary<int, VehicleMovementData> result) GetAllVehicleLocations()
    {
        return (true, _vehicleLocationDataById);
    }
}