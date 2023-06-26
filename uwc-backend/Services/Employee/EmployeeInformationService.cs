using Models;
using Repositories;

namespace Services.Employee;

public interface IEmployeeInformationService
{
    public (bool success, object result) AddEmployee(string firstName, string lastName, int gender,
        DateTime dateOfBirth, int role);

    public (bool success, object result) DeleteEmployee(int id);

    public (bool success, object result) UpdateRoleEmployee(int employeeId, string firstname, string lastname,
        int gender, DateTime dateOfBirth, int role);

    public List<Models.Employee> GetAllEmployee();

    public (bool success, object result) GetEmployeeById(int id);

    public List<Models.Employee> GetEmployeeByRole(int role);

    public List<Models.Employee> GetFreeEmployees();

    public List<Models.Employee> SortByTasksDescendingly();

    public List<Models.Employee> SortByTasksAscendingly();
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

        return (true, "Add new employee successfully.");
    }

    public (bool success, object result) DeleteEmployee(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id))
        {
            return (false, "Employee does not exist.");
        }

        _unitOfWork.Employees.RemoveById(id);
        _unitOfWork.Complete();
        return (true, "Delete employee successfully.");
    }

    public (bool success, object result) UpdateRoleEmployee(int employeeId, string firstname, string lastname,
        int gender, DateTime dateOfBirth, int role)
    {
        if (role < 0 || role > 2)
        {
            return (false, "Invalid role");
        }

        if (gender != 0 && gender != 1)
        {
            return (false, "Invalid gender");
        }

        if (!_unitOfWork.Employees.DoesIdExist(employeeId))
        {
            return (false, "Employee does not exist.");
        }

        var employeeList = _unitOfWork.Employees.Find(employee => employee.Id == employeeId);
        var employee = employeeList.First();

        employee.FirstName = firstname;
        employee.LastName = lastname;
        employee.Gender = gender;
        employee.DateOfBirth = dateOfBirth;
        employee.Role = role;

        _unitOfWork.Complete();
        return (true, "Update employee information successfully.");
    }

    public List<Models.Employee> GetAllEmployee()
    {
        var employeeList = _unitOfWork.Employees.GetAll();
        return employeeList.ToList();
    }

    public (bool success, object result) GetEmployeeById(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id))
        {
            return (false, "Employee Id does not exist.");
        }

        var employee = _unitOfWork.Employees.Find(employee => employee.Id == id).First();
        return (true, employee);
    }

    public List<Models.Employee> GetEmployeeByRole(int role)
    {
        var employeeList = _unitOfWork.Employees.Find(employee => employee.Role == role);
        return employeeList.ToList();
    }

    public List<Models.Employee> GetFreeEmployees()
    {
        var taskList =
            _unitOfWork.Tasks.Find(task => task.Date >= DateTime.Now && task.Date <= DateTime.Now.AddHours(24));
        var employeeList = _unitOfWork.Employees.Find(employee =>
            employee.Role != 2 && taskList.All(task => task.Worker.Id != employee.Id));
        return employeeList.ToList();
    }

    public List<Models.Employee> SortByTasksDescendingly()
    {
        var employeeList = _unitOfWork.Employees.GetAll().Where(employee => employee.Role != 0)
            .OrderBy<Models.Employee, int>(employee =>
                _unitOfWork.Tasks.Count(task => task.Worker.Id == employee.Id));

        return employeeList.ToList();
    }

    public List<Models.Employee> SortByTasksAscendingly()
    {
        var employeeList = _unitOfWork.Employees.GetAll().Where(employee => employee.Role != 0)
            .OrderByDescending<Models.Employee, int>(employee =>
                _unitOfWork.Tasks.Count(task => task.Worker.Id == employee.Id));

        return employeeList.ToList();
    }
}