using Models;
using Models.Types;
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

    public async Task<(bool success, string message)> AddEmployeeProfile(string firstName, string lastName, Gender gender,
        DateTime dateOfBirth, EmployeeRole role)
    {
        var employeeInformation = new EmployeeProfile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = role
        };

        _unitOfWork.EmployeeProfiles.Add(employeeInformation);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> DeleteEmployeeProfile(int employeeId)
    {
        var employee = _unitOfWork.EmployeeProfiles.GetById(employeeId);
        if (employee == null)
            return (false, Prompts.EMPLOYEE_NOT_EXIST);

        _unitOfWork.EmployeeProfiles.Remove(employee);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> UpdateEmployeeProfile(int employeeId, string firstName, string lastName,
        Gender gender, DateTime dateOfBirth, EmployeeRole role)
    {
        var employee = _unitOfWork.EmployeeProfiles.GetById(employeeId);
        if (employee == null) return (false, Prompts.EMPLOYEE_NOT_EXIST);

        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Gender = gender;
        employee.DateOfBirth = dateOfBirth;
        employee.Role = role;

        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message, List<EmployeeProfile> result)> GetAllEmployeeProfiles()
    {
        var employees = _unitOfWork.EmployeeProfiles.GetAll();

        return (true, Prompts.SUCCESS, employees.ToList());
    }

    public async Task<(bool success, string message, EmployeeProfile result)> GetEmployeeById(int id)
    {
        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(id)) return (false, Prompts.EMPLOYEE_NOT_EXIST, null);
        var employee = _unitOfWork.EmployeeProfiles.GetById(id);

        return (true, Prompts.SUCCESS, employee);
    }

    public async Task<(bool success, string message, List<EmployeeProfile> result)> GetAllEmployeesWithRole(EmployeeRole role)
    {
        var employees = _unitOfWork.EmployeeProfiles.Find(profile => profile.Role == role);
        return (true, Prompts.SUCCESS, employees.ToList());
    }
}