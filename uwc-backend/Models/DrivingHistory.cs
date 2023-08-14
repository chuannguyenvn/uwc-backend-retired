namespace Models;

public class DrivingHistory : IndexedEntity
{
    public int DriverProfileId { get; set; }
    public DriverProfile DriverProfile { get; set; }

    public DateTime Timestamp { get; set; }

    public int VehicleUsedId { get; set; }
    public Vehicle VehicleUsed { get; set; }
}