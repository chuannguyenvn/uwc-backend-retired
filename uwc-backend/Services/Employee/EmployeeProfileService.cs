using Repositories;
using Utilities;

namespace Services.Employee;

public class EmployeeProfileService : IEmployeeProfileService
{
    private readonly UnitOfWork _unitOfWork;

    public EmployeeProfileService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string message)> AddEmployeeProfile(string firstName, string lastName, int gender,
        DateTime dateOfBirth, int role)
    {
        var employeeInformation = new Models.EmployeeProfile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = role
        };

        _unitOfWork.EmployeesProfile.Add(employeeInformation);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> DeleteEmployeeProfile(int employeeId)
    {
        if (!_unitOfWork.EmployeesProfile.DoesIdExist(employeeId)) return (false, Prompts.EMPLOYEE_NOT_EXIST);

        _unitOfWork.EmployeesProfile.RemoveById(employeeId);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> UpdateEmployeeProfile(int employeeId, string firstname, string lastname, int gender,
        DateTime dateOfBirth, int role)
    {
        if (!_unitOfWork.EmployeesProfile.DoesIdExist(employeeId)) return (false, Prompts.EMPLOYEE_NOT_EXIST);

        if (role is < 0 or > 2) return (false, Prompts.INVALID_ROLE);
        if (gender != 0 && gender != 1) return (false, Prompts.INVALID_GENDER);

        var employeeList = _unitOfWork.EmployeesProfile.Find(employee => employee.Id == employeeId);
        var employee = employeeList.First();

        employee.FirstName = firstname;
        employee.LastName = lastname;
        employee.Gender = gender;
        employee.DateOfBirth = dateOfBirth;
        employee.Role = role;

        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message, List<Models.EmployeeProfile> result)> GetAllEmployeeProfiles()
    {
        return (true, Prompts.SUCCESS, _unitOfWork.EmployeesProfile.GetAll().ToList());
    }

    public async Task<(bool success, string message, Models.EmployeeProfile result)> GetEmployeeById(int id)
    {
        if (!_unitOfWork.EmployeesProfile.DoesIdExist(id)) return (false, Prompts.EMPLOYEE_NOT_EXIST, null);

        var employee = _unitOfWork.EmployeesProfile.GetById(id);

        return (true, Prompts.SUCCESS, employee);
    }

    public async Task<(bool success, string message, List<Models.EmployeeProfile> result)> GetAllEmployeesWithRole(int role)
    {
        var employeeList = _unitOfWork.EmployeesProfile.Find(employee => employee.Role == role);

        return (true, Prompts.SUCCESS, employeeList.ToList());
    }
}