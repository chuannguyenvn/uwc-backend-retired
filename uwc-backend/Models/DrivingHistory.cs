namespace Models
{
    public class DrivingHistory : IndexedEntity
    {
        public DriverProfile DriverProfile { get; set; }
        public DateTime Timestamp { get; set; }
        public Vehicle VehicleUsed { get; set; }
    }
}