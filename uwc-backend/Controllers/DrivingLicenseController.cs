using Microsoft.AspNetCore.Mvc;
using Models;
using uwc_backend.Communications.Vehicle;
using uwc_backend.Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class DrivingLicenseController: ControllerBase
{
    private readonly IDrivingLicenseService _drivingLicenseService;

    public DrivingLicenseController(IDrivingLicenseService drivingLicenseService)
    {
        _drivingLicenseService = drivingLicenseService;
    }

    [HttpPost("add-driving-license")]
    public IActionResult AddDrivingLicense(AddDrivingLicenseRequest addDrivingLicenseRequest)
    {
        var (success, result) = _drivingLicenseService.AddDrivingLicense(addDrivingLicenseRequest.IssueDate,
            addDrivingLicenseRequest.IssuePlace, addDrivingLicenseRequest.Owner, addDrivingLicenseRequest.Type);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-driving-licenses/{driverId}")]
    public List<DrivingLicense> GetDrivingLicenses([FromRoute] int driverId)
    {
        var result = _drivingLicenseService.GetDrivingLicenseDriver(driverId);
        return result;
    }

    [HttpPut("update-driving-license")]
    public IActionResult UpdateDrivingLicense(UpdateDrivingLicenseRequest updateDrivingLicenseRequest)
    {
        var (success, result) = _drivingLicenseService.UpdateDrivingLicenseInformation(updateDrivingLicenseRequest.Id,
            updateDrivingLicenseRequest.IssueDate, updateDrivingLicenseRequest.IssuePlace,
            updateDrivingLicenseRequest.Owner, updateDrivingLicenseRequest.Type);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-driving-license/{drivingLicenseId}")]
    public IActionResult DeleteDrivingLicense([FromRoute] int drivingLicenseId)
    {
        var (success, result) = _drivingLicenseService.DeleteDrivingLicense(drivingLicenseId);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}