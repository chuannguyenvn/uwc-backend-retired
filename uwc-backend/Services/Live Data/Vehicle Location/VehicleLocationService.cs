using Newtonsoft.Json;
using Repositories;
using Commons;
using Commons.Types;

namespace Services.LiveData;

public class VehicleLocationService : IHostedService, IDisposable
{
    private const string MAPBOX_DIRECTION_API =
        "https://api.mapbox.com/directions/v5/mapbox/driving-traffic/{0};{1}?geometries=geojson&access_token=pk.eyJ1IjoiZGlnaXRhbGJveTAzMCIsImEiOiJjbGxqY2hpaGsxcDllM2VsZ3B1ajZyOGp4In0.DySADZ5B0wTt_dRRQ4CEAw";

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
        }
    }

    private void MoveBots(object? state)
    {
        Parallel.ForEach(_vehicleLocationDataById.Values,
            locationData =>
            {
                if (!locationData.IsBot) return;

                if (locationData.MapboxDirectionResponse == null ||
                    locationData.MapboxDirectionResponse.Routes[0].Geometry.Coordinates.Count == 0)
                {
                    var mostFullMcp = GetRandomMcp();
                    locationData.TargettingMcps = new List<Models.Mcp>() {mostFullMcp};

                    var mcpCoordinate = new Coordinate(mostFullMcp.Latitude, mostFullMcp.Longitude);
                    locationData.MapboxDirectionResponse = RequestMapboxDirection(locationData.CurrentLocation, mcpCoordinate);
                }

                var distanceLeft = 0.0005;
                while (distanceLeft > 0 && locationData.MapboxDirectionResponse.Routes[0].Geometry.Coordinates.Count > 0)
                {
                    var currentLocation = locationData.CurrentLocation;
                    var nextWaypointLocation = new Coordinate(locationData.MapboxDirectionResponse.Routes[0].Geometry.Coordinates[0]);

                    var distanceToNextWaypoint = currentLocation.DistanceTo(nextWaypointLocation);
                    if (distanceToNextWaypoint > distanceLeft)
                    {
                        var t = Math.Clamp(distanceLeft / distanceToNextWaypoint, 0, 1);
                        locationData.CurrentLocation = Coordinate.Lerp(currentLocation, nextWaypointLocation, t);
                        locationData.CurrentOrientationAngle = (float)locationData.CurrentLocation.AngleTo(nextWaypointLocation);
                        break;
                    }

                    locationData.CurrentLocation = nextWaypointLocation;
                    locationData.MapboxDirectionResponse.Routes[0].Geometry.Coordinates.RemoveAt(0);
                    distanceLeft -= distanceToNextWaypoint;
                }

                if (locationData.MapboxDirectionResponse.Routes[0].Geometry.Coordinates.Count == 0)
                {
                    EmptyMcp(locationData.TargettingMcps[0].Id);
                    locationData.TargettingMcps.RemoveAt(0);
                }
            });
    }

    private MapboxDirectionResponse RequestMapboxDirection(Coordinate fromLocation, Coordinate toLocation)
    {
        var client = new HttpClient();
        var httpResponse = client.GetStringAsync(ConstructMapboxDirectionRequest(fromLocation, toLocation)).Result;
        var mapboxDirectionResponse = JsonConvert.DeserializeObject<MapboxDirectionResponse>(httpResponse);
        return mapboxDirectionResponse;
    }

    private MapboxDirectionResponse RequestMapboxDirection(Coordinate fromLocation, List<Coordinate> toLocations)
    {
        var client = new HttpClient();
        var httpResponse = client.GetStringAsync(ConstructMapboxDirectionRequest(fromLocation, toLocations)).Result;
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
        return string.Format(MAPBOX_DIRECTION_API, currentLocation.ToStringAPI(), destinationLocation.ToStringAPI());
    }

    private string ConstructMapboxDirectionRequest(Coordinate currentLocation, List<Coordinate> destinationLocations)
    {
        return string.Format(MAPBOX_DIRECTION_API,
            currentLocation.ToStringAPI(),
            String.Join(',', destinationLocations.Select(location => location.ToStringAPI())));
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

    public void AddRoute(int vehicleId, List<int> mcpIds)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

        var remainingWaypoints = _vehicleLocationDataById[vehicleId]
            .MapboxDirectionResponse.Waypoints.Select(waypoint => new Coordinate(waypoint.Location));

        var newMcps = mcpIds.Select(mcpId => unitOfWork.Mcps.GetById(mcpId)).ToList();
        _vehicleLocationDataById[vehicleId].TargettingMcps.AddRange(newMcps);

        var newWaypoints = newMcps.Select(mcp => new Coordinate(mcp.Latitude, mcp.Longitude));
        var allWaypoints = remainingWaypoints.Concat(newWaypoints).ToList();

        _vehicleLocationDataById[vehicleId].MapboxDirectionResponse =
            RequestMapboxDirection(_vehicleLocationDataById[vehicleId].CurrentLocation, allWaypoints);
    }
}