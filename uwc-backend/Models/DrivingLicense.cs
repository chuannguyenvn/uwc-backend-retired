namespace Models;

public class DrivingLicense : IndexedEntity
{
    public DateTime IssueDate { get; set; }
    public string IssuePlace { get; set; }
    public EmployeeProfile Owner { get; set; }
    public string Type { get; set; }
}