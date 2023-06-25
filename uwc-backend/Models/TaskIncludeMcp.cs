namespace Models;

public class TaskIncludeMcp : IndexedEntity
{
    public Task Task { get; set; }
    public int McpId { get; set; }
}