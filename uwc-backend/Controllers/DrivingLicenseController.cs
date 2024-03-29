using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Vehicle;
using Commons.Communications.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class DrivingLicenseController : ControllerBase
{
    private readonly IDrivingLicenseService _drivingLicenseService;

    public DrivingLicenseController(IDrivingLicenseService drivingLicenseService)
    {
        _drivingLicenseService = drivingLicenseService;
    }

    [HttpPost("add")]
    public IActionResult AddDrivingLicense(AddDrivingLicenseRequest addDrivingLicenseRequest)
    {
        var (success, result) = _drivingLicenseService.AddDrivingLicense(addDrivingLicenseRequest.IssueDate,
            addDrivingLicenseRequest.IssuePlace,
            addDrivingLicenseRequest.OwnerDriverId,
            addDrivingLicenseRequest.Type);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/{driverId}")]
    public List<DrivingLicense> GetDrivingLicenses([FromRoute] int driverId)
    {
        var result = _drivingLicenseService.GetDrivingLicenseOfDriver(driverId);
        return result;
    }

    [HttpPut("update")]
    public IActionResult UpdateDrivingLicense(UpdateDrivingLicenseRequest updateDrivingLicenseRequest)
    {
        var (success, result) = _drivingLicenseService.UpdateDrivingLicense(updateDrivingLicenseRequest.Id,
            updateDrivingLicenseRequest.IssueDate,
            updateDrivingLicenseRequest.IssuePlace,
            updateDrivingLicenseRequest.OwnerDriverId,
            updateDrivingLicenseRequest.Type);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{drivingLicenseId}")]
    public IActionResult DeleteDrivingLicense([FromRoute] int drivingLicenseId)
    {
        var (success, result) = _drivingLicenseService.DeleteDrivingLicense(drivingLicenseId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete-outdated")]
    public IActionResult DeleteOutdatedDrivingLicenses()
    {
        var (success, result) = _drivingLicenseService.DeleteAllOutdatedDrivingLicenses();

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}