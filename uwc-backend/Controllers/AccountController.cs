using Communications.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
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
    public IActionResult UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
    {
        var (success, result) = _authenticationService.UpdatePassword(updatePasswordRequest.Username,
            updatePasswordRequest.OldPassword,
            updatePasswordRequest.NewPassword);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete-account/{mcpId}")]
    public IActionResult DeleteAccount([FromRoute] int mcpId)
    {
        var (success, result) = _authenticationService.DeleteAccount(mcpId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-settings")]
    public IActionResult UpdateSettings(UpdateSettingsRequest updateSettingsRequest)
    {
        var (success, result) = _authenticationService.UpdateSettings(updateSettingsRequest.Username,
            updateSettingsRequest.Password,
            updateSettingsRequest.Settings);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}