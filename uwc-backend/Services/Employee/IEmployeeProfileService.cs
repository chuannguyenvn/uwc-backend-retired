namespace Services.Employee;

public interface IEmployeeProfileService
{
    public (bool success, object result) AddEmployeeProfile(string firstName, string lastName, int gender, DateTime dateOfBirth, int role);

    public (bool success, object result) DeleteEmployeeProfile(int employeeId);

    public (bool success, object result) UpdateEmployeeProfile(int employeeId, string firstname, string lastname, int gender,
        DateTime dateOfBirth, int role);

    public List<Models.Employee> GetAllEmployeeProfiles();

    public (bool success, object result) GetEmployeeById(int id);

    public List<Models.Employee> GetAllEmployeesWithRole(int role);
}