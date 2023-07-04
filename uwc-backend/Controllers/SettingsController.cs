using Communications.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.AccountManagement;
using Utilities;

namespace Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    private readonly IAccountManagementService _accountManagementService;

    public SettingsController(IAccountManagementService accountManagementService)
    {
        _accountManagementService = accountManagementService;
    }

    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
    {
        var (success, result) =
            await _accountManagementService.UpdatePassword(User.GetLoggedInUserId(), request.OldPassword, request.NewPassword);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-settings")]
    public async Task<IActionResult> UpdateSettings(UpdateSettingsRequest request)
    {
        var (success, result) = await _accountManagementService.UpdateSettings(User.GetLoggedInUserId(), request.Settings);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}