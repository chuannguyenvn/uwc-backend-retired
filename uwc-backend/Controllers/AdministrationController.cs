using Microsoft.AspNetCore.Mvc;
using Services.Administration;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly IAdministrationService _administrationService;

    public AdministrationController(IAdministrationService administrationService)
    {
        _administrationService = administrationService;
    }

    [HttpDelete("delete-account/{accountId}")]
    public async Task<IActionResult> DeleteAccount([FromRoute] int accountId)
    {
        var (success, result) = await _administrationService.DeleteAccount(accountId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}