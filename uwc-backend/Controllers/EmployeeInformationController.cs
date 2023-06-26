using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using uwc_backend.Communications;
using IEmployeeInformationService = Services.Employee.IEmployeeInformationService;

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
        var (success, result) = _employeeInformationService.AddEmployee(addEmployeeRequest.FirstName,
            addEmployeeRequest.LastName, addEmployeeRequest.Gender, addEmployeeRequest.DateOfBirth,
            addEmployeeRequest.Role);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-employee-id/{employeeId}")]
    public IActionResult DeleteEmployeeId([FromRoute] int employeeId)
    {
        var (success, result) = _employeeInformationService.DeleteEmployee(employeeId);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("update-employee-id")]
    public IActionResult UpdateRoleEmployeeId(UpdateEmployeeRequest updateEmployeeRequest)
    {
        var (success, result) =
            _employeeInformationService.UpdateRoleEmployee(updateEmployeeRequest.Employee,
                updateEmployeeRequest.FirstName, updateEmployeeRequest.LastName,
                updateEmployeeRequest.Gender, updateEmployeeRequest.DateOfBirth,
                updateEmployeeRequest.Role);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-all-employee")]
    public List<Models.Employee> GetAllEmployee()
    {
        var result = _employeeInformationService.GetAllEmployee();
        return result;
    }

    [HttpGet("get-employee-id/{employeeId}")]
    public IActionResult GetEmployeeById([FromRoute] int employeeId)
    {
        var (success, result) = _employeeInformationService.GetEmployeeById(employeeId);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-employee-by-role/{role}")]
    public List<Models.Employee> GetEmployeeByRole([FromRoute] int role)
    {
        var result = _employeeInformationService.GetEmployeeByRole(role);
        return result;
    }

    [HttpGet("get-free-employees")]
    public List<Models.Employee> GetFreeEmployees()
    {
        var result = _employeeInformationService.GetFreeEmployees();
        return result;
    }
}