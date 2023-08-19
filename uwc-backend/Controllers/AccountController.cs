using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using Commons.Communications.Account;

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
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var (success, message) = await _authenticationService.Register(registerRequest.Username,
            registerRequest.Password,
            registerRequest.Employee);

        if (!success) return BadRequest(message);

        return Ok(message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var (success, message, token) = await _authenticationService.Login(loginRequest.Username, loginRequest.Password);

        if (!success) return BadRequest(message);

        return Ok(token);
    }
}