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
}