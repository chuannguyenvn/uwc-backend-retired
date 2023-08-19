using Commons.Types;

namespace Models
{
    public class Vehicle : IndexedEntity
    {
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public VehicleType VehicleType { get; set; }

        public double CurrentLoad { get; set; }
        public double Capacity { get; set; }
        public double AverageSpeed { get; set; }
    }
}