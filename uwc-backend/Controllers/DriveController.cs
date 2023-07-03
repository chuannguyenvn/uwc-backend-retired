using Communications.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Models;
using uwc_backend.Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class DriveController : ControllerBase
{
    private readonly IDriveService _driveService;

    public DriveController(IDriveService driveService)
    {
        _driveService = driveService;
    }

    [HttpPost("add-driving-history")]
    public IActionResult AddDrivingHistory(AddDriveRequest addDriveRequest)
    {
        var (success, result) = _driveService.AddDrive(addDriveRequest.Date, addDriveRequest.Driver, addDriveRequest.Vehicle);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-driving-history")]
    public List<Drive> GetDrivingHistory()
    {
        var result = _driveService.GetAllDrives();
        return result;
    }

    [HttpDelete("delete-driving-history/{driveId}")]
    public IActionResult DeleteDrivingHistory([FromRoute] int driveId)
    {
        var (success, result) = _driveService.DeleteDrive(driveId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("driver-full-vehicle-sort-by-name")]
    public List<Drive> GetDriverWithFullVehicleSortByName()
    {
        var result = _driveService.GetDriverFullVehicleSortByName();
        return result;
    }

    [HttpGet("driver-full-vehicle-sort-by-current-load")]
    public List<Drive> GetDriverWithFullVehicleSortByCurrentLoad()
    {
        var result = _driveService.GetDriverFullVehicleSortByCurrentLoad();
        return result;
    }
}