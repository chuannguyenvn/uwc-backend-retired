using Microsoft.AspNetCore.Mvc;
using uwc_backend.Communications.Vehicle;
using uwc_backend.Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController: ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpPost("add-vehicle")]
    public IActionResult AddVehicle(AddVehicleRequest addVehicleRequest)
    {
        var (success, result) = _vehicleService.AddVehicle(addVehicleRequest.Capacity, addVehicleRequest.CurrentLoad,
            addVehicleRequest.AverageSpeed);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-all-vehicles")]
    public List<Models.Vehicle> GetAllVehicles()
    {
        var result = _vehicleService.GetAllVehicles();
        return result;
    }

    [HttpPut("update-vehicle-info")]
    public IActionResult UpdateVehicleInformation(UpdateVehicleInformationRequest updateVehicleInformationRequest)
    {
        var (success, result) = _vehicleService.UpdateVehicleInformation(updateVehicleInformationRequest.Id, updateVehicleInformationRequest.Capacity,
            updateVehicleInformationRequest.CurrentLoad, updateVehicleInformationRequest.AverageSpeed);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-vehicle/{vehicleId}")]
    public IActionResult DeleteVehicle([FromRoute] int vehicleId)
    {
        var (success, result) = _vehicleService.DeleteVehicle(vehicleId);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}