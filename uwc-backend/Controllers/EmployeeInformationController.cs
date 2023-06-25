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

    [HttpPut("update-role-employee-id")]
    public IActionResult UpdateRoleEmployeeId(UpdateRoleEmployeeRequest updateRoleEmployeeRequest)
    {
        var (success, result) =
            _employeeInformationService.UpdateRoleEmployee(updateRoleEmployeeRequest.Employee,
                updateRoleEmployeeRequest.Role);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}