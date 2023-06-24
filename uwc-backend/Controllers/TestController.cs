using Microsoft.AspNetCore.Mvc;
using Services.LiveData;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly McpCapacityService _mcpCapacityService;

    public TestController(McpCapacityService mcpCapacityService)
    {
        _mcpCapacityService = mcpCapacityService;
    }

    [HttpPost("test")]
    public IActionResult Login()
    {
        return Ok(_mcpCapacityService.GetMcpsWithHighLoadCount());
    }
}