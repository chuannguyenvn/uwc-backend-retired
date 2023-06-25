namespace Models;

public class Employee : IndexedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Role { get; set; }
}