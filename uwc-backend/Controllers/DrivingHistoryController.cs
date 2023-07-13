using Communications.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Vehicle;

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

    [HttpPost("add")]
    public IActionResult AddDrivingHistory(AddDriveRequest addDriveRequest)
    {
        var (success, result) = _driveService.AddDrivingHistory(addDriveRequest.Date, addDriveRequest.Driver, addDriveRequest.Vehicle);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("all")]
    public List<DrivingHistory> GetAllDrivingHistory()
    {
        var result = _driveService.GetAllDrivingHistory();
        return result;
    }

    [HttpDelete("delete/{driveId}")]
    public IActionResult DeleteDrivingHistory([FromRoute] int driveId)
    {
        var (success, result) = _driveService.DeleteDrivingHistory(driveId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}