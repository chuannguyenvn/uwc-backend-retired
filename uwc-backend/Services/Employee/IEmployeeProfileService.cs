using Models.Types;

namespace Services.Employee;

public interface IEmployeeProfileService
{
    public Task<(bool success, string message)> AddEmployeeProfile(string firstName, string lastName, Gender gender, DateTime dateOfBirth,
        EmployeeRole employeeRole);

    public Task<(bool success, string message)> DeleteEmployeeProfile(int employeeId);

    public Task<(bool success, string message)> UpdateEmployeeProfile(int employeeId, string firstname, string lastname, Gender gender,
        DateTime dateOfBirth, EmployeeRole employeeRole);

    public Task<(bool success, string message, List<Models.EmployeeProfile> result)> GetAllEmployeeProfiles();

    public Task<(bool success, string message, Models.EmployeeProfile result)> GetEmployeeById(int id);

    public Task<(bool success, string message, List<Models.EmployeeProfile> result)> GetAllEmployeesWithRole(EmployeeRole employeeRole);
}