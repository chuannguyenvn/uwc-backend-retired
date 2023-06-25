namespace Models;

public class Task : IndexedEntity
{
    public DateTime Date { get; set; }
    public Employee Supervisor { get; set; }
    public Employee Worker { get; set; }
    public Route Route { get; set; }
}