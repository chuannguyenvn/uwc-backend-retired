using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using uwc_backend.Communications;
using AuthenticationService = Services.Authentication.AuthenticationService;
using IAuthenticationService = Services.Authentication.IAuthenticationService;

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
    public IActionResult Register(AuthenticationRequest authenticationRequest)
    {
        var (success, result) =
            _authenticationService.Register(authenticationRequest.Username, authenticationRequest.Password, authenticationRequest.Employee);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
    {
        var (success, result) =
            await _authenticationService.Login(authenticationRequest.Username, authenticationRequest.Password);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}