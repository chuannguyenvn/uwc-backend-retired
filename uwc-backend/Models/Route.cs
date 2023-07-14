namespace Models
{
    public class Route : IndexedEntity
    {
        public string RouteName { get; set; }
        public double TotalLength { get; set; }
        public string RouteDetails { get; set; }
    }
}