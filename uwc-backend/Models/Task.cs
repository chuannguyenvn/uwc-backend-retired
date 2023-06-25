namespace Models;

public class Task : IndexedEntity
{
    public DateTime Date { get; set; }
    public Employee Worker { get; set; }
}