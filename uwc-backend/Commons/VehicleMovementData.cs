using Commons.Types;

namespace Commons
{
    public class VehicleMovementData
    {
        public Coordinate CurrentLocation;
        public float CurrentOrientationAngle;
        public bool IsBot = true;
        public List<Models.Mcp> TargettingMcps;
        public MapboxDirectionResponse MapboxDirectionResponse;
    }
}