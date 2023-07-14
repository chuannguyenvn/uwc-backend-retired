namespace Models
{
    public class Task : IndexedEntity
    {
        public SupervisorProfile Supervisor { get; set; }
        public DateTime Date { get; set; }
        public UserProfile Worker { get; set; }
        public Route Route { get; set; }
    }
}