using Communications.Employee;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Employee;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeInformationController : ControllerBase
{
    private readonly IEmployeeInformationService _employeeInformationService;

    public EmployeeInformationController(IEmployeeInformationService employeeInformationService)
    {
        _employeeInformationService = employeeInformationService;
    }

    [HttpPost("add-employee")]
    public IActionResult AddEmployee(AddEmployeeRequest addEmployeeRequest)
    {
        var (success, result) = _employeeInformationService.AddEmployee(
            addEmployeeRequest.FirstName,
            addEmployeeRequest.LastName,
            addEmployeeRequest.Gender,
            addEmployeeRequest.DateOfBirth,
            addEmployeeRequest.Role);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete-employee-id/{employeeId}")]
    public IActionResult DeleteEmployeeId([FromRoute] int employeeId)
    {
        var (success, result) = _employeeInformationService.DeleteEmployee(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-employee-id")]
    public IActionResult UpdateRoleEmployeeId(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var (success, result) = _employeeInformationService.UpdateRoleEmployee(
            updateEmployeeRequest.Employee,
            updateEmployeeRequest.FirstName,
            updateEmployeeRequest.LastName,
            updateEmployeeRequest.Gender,
            updateEmployeeRequest.DateOfBirth,
            updateEmployeeRequest.Role);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-all-employee")]
    public List<Employee> GetAllEmployee()
    {
        var result = _employeeInformationService.GetAllEmployee();
        return result;
    }

    [HttpGet("get-employee-id/{employeeId}")]
    public IActionResult GetEmployeeById([FromRoute] int employeeId)
    {
        var (success, result) = _employeeInformationService.GetEmployeeById(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-employee-by-role/{role}")]
    public List<Employee> GetEmployeeByRole([FromRoute] int role)
    {
        var result = _employeeInformationService.GetEmployeeByRole(role);
        return result;
    }

    [HttpGet("get-free-employees")]
    public List<Employee> GetFreeEmployees()
    {
        var result = _employeeInformationService.GetFreeEmployees();
        return result;
    }

    [HttpGet("sort-employees-tasks-descendingly")]
    public List<Employee> GetEmployeesByTaskDescendingly()
    {
        var result = _employeeInformationService.SortByTasksDescendingly();
        return result;
    }

    [HttpGet("sort-employees-tasks-ascendingly")]
    public List<Employee> GetEmployeesByTaskAscendingly()
    {
        var result = _employeeInformationService.SortByTasksAscendingly();
        return result;
    }
}