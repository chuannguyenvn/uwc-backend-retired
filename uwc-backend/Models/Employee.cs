namespace Models;

public class Employee : IndexedEntity
{
    public string Name { get; set; }
    public int Gender { get; set; }
    public string Role { get; set; }
}