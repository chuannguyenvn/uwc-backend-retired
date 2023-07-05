using Communications.Employee;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Employee;
using Services.Task;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeInformationController : ControllerBase
{
    private readonly IEmployeeProfileService _employeeProfileService;
    private readonly ITaskService _taskService;

    public EmployeeInformationController(IEmployeeProfileService employeeProfileService, ITaskService taskService)
    {
        _employeeProfileService = employeeProfileService;
        _taskService = taskService;
    }

    [HttpPost("add-employee")]
    public IActionResult AddEmployee(AddEmployeeRequest addEmployeeRequest)
    {
        var (success, result) = _employeeProfileService.AddEmployeeProfile(addEmployeeRequest.FirstName,
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
        var (success, result) = _employeeProfileService.DeleteEmployeeProfile(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-employee-id")]
    public IActionResult UpdateRoleEmployeeId(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var (success, result) = _employeeProfileService.UpdateEmployeeProfile(updateEmployeeRequest.Employee,
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
        var result = _employeeProfileService.GetAllEmployeeProfiles();
        return result;
    }

    [HttpGet("get-employee-id/{employeeId}")]
    public IActionResult GetEmployeeById([FromRoute] int employeeId)
    {
        var (success, result) = _employeeProfileService.GetEmployeeById(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-employee-by-role/{role}")]
    public List<Employee> GetEmployeeByRole([FromRoute] int role)
    {
        var result = _employeeProfileService.GetAllEmployeesWithRole(role);
        return result;
    }

    [HttpGet("get-free-employees")]
    public List<Employee> GetFreeEmployees()
    {
        var result = _taskService.GetFreeEmployees();
        return result;
    }

    [HttpGet("sort-employees-tasks-descendingly")]
    public List<Employee> GetEmployeesByTaskDescendingly()
    {
        var result = _taskService.SortByTasksDescendingly();
        return result;
    }

    [HttpGet("sort-employees-tasks-ascendingly")]
    public List<Employee> GetEmployeesByTaskAscendingly()
    {
        var result = _taskService.SortByTasksAscendingly();
        return result;
    }
}