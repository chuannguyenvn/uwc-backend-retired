namespace Models;

public class Drive : IndexedEntity
{
    public DateTime Date { get; set; }
    public Employee Driver { get; set; }
    public Vehicle Vehicle { get; set; }
}