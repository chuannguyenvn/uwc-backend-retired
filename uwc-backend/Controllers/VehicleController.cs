using Microsoft.AspNetCore.Mvc;
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
}