namespace Models;

public class DrivingHistory : IndexedEntity
{
    public DateTime Timestamp { get; set; }
    public EmployeeProfile Driver { get; set; }
    public Vehicle Vehicle { get; set; }
}