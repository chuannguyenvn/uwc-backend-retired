using Commons.Types;
using Models;

namespace Commons
{
    public class VehicleMovementData
    {
        public DriverProfile DriverProfile { get; set; }
        public Coordinate CurrentLocation { get; set; }
        public float CurrentOrientationAngle { get; set; }
        public bool IsBot { get; set; } = true;
        public List<Mcp> TargettingMcps { get; set; }
        public MapboxDirectionResponse MapboxDirectionResponse { get; set; }
    }
}