namespace Models
{
    public class TaskEntry : IndexedEntity
    {
        public SupervisorProfile Supervisor { get; set; }
        public DateTime Date { get; set; }
        public UserProfile Worker { get; set; }
        public Mcp Mcp { get; set; }
    }
}