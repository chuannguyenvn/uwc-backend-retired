using Microsoft.AspNetCore.Mvc;
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
}