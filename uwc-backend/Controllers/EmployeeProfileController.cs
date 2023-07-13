using Communications.Employee;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Types;
using Services.Employee;
using Services.Task;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeProfileController : ControllerBase
{
    private readonly IEmployeeProfileService _employeeProfileService;
    private readonly ITaskService _taskService;

    public EmployeeProfileController(IEmployeeProfileService employeeProfileService, ITaskService taskService)
    {
        _employeeProfileService = employeeProfileService;
        _taskService = taskService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
    {
        var (success, result) = await _employeeProfileService.AddEmployeeProfile(addEmployeeRequest.FirstName,
            addEmployeeRequest.LastName,
            addEmployeeRequest.Gender,
            addEmployeeRequest.DateOfBirth,
            addEmployeeRequest.Role);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{employeeId}")]
    public async Task<IActionResult> DeleteEmployeeId([FromRoute] int employeeId)
    {
        var (success, result) = await _employeeProfileService.DeleteEmployeeProfile(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateRoleEmployeeId(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var (success, result) = await _employeeProfileService.UpdateEmployeeProfile(updateEmployeeRequest.Employee,
            updateEmployeeRequest.FirstName,
            updateEmployeeRequest.LastName,
            updateEmployeeRequest.Gender,
            updateEmployeeRequest.DateOfBirth,
            updateEmployeeRequest.Role);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllEmployee()
    {
        var result = _employeeProfileService.GetAllEmployeeProfiles();
        return Ok(result);
    }

    [HttpGet("info/{employeeId}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] int employeeId)
    {
        var (success, message, result) = await _employeeProfileService.GetEmployeeById(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("with-role/{role}")]
    public async Task<IActionResult> GetAllEmployeesWithRole([FromRoute] EmployeeRole role)
    {
        var result = await _employeeProfileService.GetAllEmployeesWithRole(role);
        return Ok(result.result);
    }

    [HttpGet("free")]
    public List<EmployeeProfile> GetFreeEmployees()
    {
        var result = _taskService.GetFreeEmployees();
        return result;
    }
}