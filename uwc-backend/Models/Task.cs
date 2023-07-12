using Repositories.Implementations;

namespace Models;

public class Task : IndexedEntity
{
    public DateTime Date { get; set; }
    public SupervisorProfile Supervisor { get; set; }
    public EmployeeProfile Worker { get; set; }
    public Route Route { get; set; }
}