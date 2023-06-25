namespace Models;

public class Mcp : IndexedEntity
{
    public double Capacity { get; set; }
    public double CurrentLoad { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}