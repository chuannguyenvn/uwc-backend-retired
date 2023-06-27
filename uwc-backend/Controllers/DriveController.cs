using Microsoft.AspNetCore.Mvc;
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
}