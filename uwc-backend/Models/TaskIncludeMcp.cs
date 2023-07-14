namespace Models
{
    public class TaskIncludeMcp : IndexedEntity
    {
        public Task Task { get; set; }
        public Mcp Mcp { get; set; }
    }
}