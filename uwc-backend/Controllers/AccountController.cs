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
}