using Communications.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.AccountManagement;
using Services.Authentication;
using Utilities;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountManagementService _accountManagementService;

    public AccountController(IAuthenticationService authenticationService, IAccountManagementService accountManagementService)
    {
        _authenticationService = authenticationService;
        _accountManagementService = accountManagementService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        var (success, result) = _authenticationService.Register(registerRequest.Username,
            registerRequest.Password,
            registerRequest.Employee,
            registerRequest.Settings);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var (success, result) = _authenticationService.Login(loginRequest.Username, loginRequest.Password);

        if (!success) return BadRequest(result);

        return Ok(result);
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