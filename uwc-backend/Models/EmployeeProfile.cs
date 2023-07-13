using Models.Types;

namespace Models;

public class EmployeeProfile : IndexedEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public EmployeeRole Role { get; set; }
}