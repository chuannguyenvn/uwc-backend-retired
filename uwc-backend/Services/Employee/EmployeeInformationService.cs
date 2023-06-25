using Models;
using Repositories;

namespace Services.Employee;

public interface IEmployeeInformationService
{
    public (bool success, object result) AddEmployee(string firstName, string lastName, int gender,
        DateTime dateOfBirth, int role);
}

public class EmployeeInformationService : IEmployeeInformationService
{
    private readonly UnitOfWork _unitOfWork;

    public EmployeeInformationService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddEmployee(string firstName, string lastName, int gender,
        DateTime dateOfBirth, int role)
    {
        var employeeInformation = new Models.Employee()
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = role,
        };
        
        _unitOfWork.Employees.Add(employeeInformation);
        _unitOfWork.Complete();
        
        return (true, "Add new employee successfully");
    }
}