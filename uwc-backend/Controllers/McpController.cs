using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Mcp;
using uwc_backend.Communications.Mcp;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class McpController : ControllerBase
{
    private readonly IMcpService _mcpService;

    public McpController(IMcpService mcpService)
    {
        _mcpService = mcpService;
    }

    [HttpPost("add-mcp")]
    public IActionResult AddMcp(AddMcpRequest addMcpRequest)
    {
        var (success, result) = _mcpService.AddMcp(addMcpRequest.Capacity, addMcpRequest.CurrentLoad,
            addMcpRequest.Latitude, addMcpRequest.Longitude);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("empty-mcp/{mcpId}")]
    public IActionResult EmptyMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpService.EmptyMcp(mcpId);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-full-mcps")]
    public List<Models.Mcp> GetFullMcps()
    {
        var result = _mcpService.GetFullMcps();
        return result;
    }
}