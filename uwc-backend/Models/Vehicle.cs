namespace Models;

public class Vehicle : IndexedEntity
{
    public double Capacity { get; set; }
    public double CurrentLoad { get; set; }
    public double AverageSpeed { get; set; }
}