namespace Models
{
    public class Mcp : IndexedEntity
    {
        public float Capacity { get; set; }
        public float CurrentLoad { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}