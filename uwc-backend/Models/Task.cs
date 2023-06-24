namespace Models;

public class Task
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Employee Worker { get; set; }
}