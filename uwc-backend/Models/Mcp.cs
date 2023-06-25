namespace Models;

public class Mcp : IndexedEntity
{
    public int Capacity { get; set; }
    public int CurrentLoad { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}