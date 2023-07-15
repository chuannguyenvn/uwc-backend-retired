using Communications.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class DrivingHistoryController : ControllerBase
{
    private readonly IDrivingHistoryService _drivingHistoryService;

    public DrivingHistoryController(IDrivingHistoryService drivingHistoryService)
    {
        _drivingHistoryService = drivingHistoryService;
    }

    [HttpPost("add")]
    public IActionResult AddDrivingHistory(AddDrivingHistoryRequest addDrivingHistoryRequest)
    {
        var (success, result) = _drivingHistoryService.AddDrivingHistory(addDrivingHistoryRequest.Date,
            addDrivingHistoryRequest.Driver,
            addDrivingHistoryRequest.Vehicle);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("all")]
    public List<DrivingHistory> GetAllDrivingHistory()
    {
        var result = _drivingHistoryService.GetAllDrivingHistory();
        return result;
    }

    [HttpDelete("delete/{drivingHistoryId}")]
    public IActionResult DeleteDrivingHistory([FromRoute] int drivingHistoryId)
    {
        var (success, result) = _drivingHistoryService.DeleteDrivingHistory(drivingHistoryId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}