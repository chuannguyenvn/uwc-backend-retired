using Communications.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpPost("add")]
    public IActionResult AddVehicle(AddVehicleRequest addVehicleRequest)
    {
        var (success, result) = _vehicleService.AddVehicle(addVehicleRequest.Capacity,
            addVehicleRequest.CurrentLoad,
            addVehicleRequest.AverageSpeed);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/all")]
    public List<Vehicle> GetAllVehicles()
    {
        var result = _vehicleService.GetAllVehicles();
        return result;
    }

    [HttpPut("update")]
    public IActionResult UpdateVehicleInformation(UpdateVehicleInformationRequest updateVehicleInformationRequest)
    {
        var (success, result) = _vehicleService.UpdateVehicle(updateVehicleInformationRequest.Id,
            updateVehicleInformationRequest.Capacity,
            updateVehicleInformationRequest.CurrentLoad,
            updateVehicleInformationRequest.AverageSpeed);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{vehicleId}")]
    public IActionResult DeleteVehicle([FromRoute] int vehicleId)
    {
        var (success, result) = _vehicleService.DeleteVehicle(vehicleId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}