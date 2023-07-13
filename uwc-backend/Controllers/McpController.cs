using Communications.Mcp;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Mcp;

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

    [HttpPost("add")]
    public IActionResult AddMcp(AddRequest addRequest)
    {
        var (success, result) = _mcpService.AddMcp(addRequest.Capacity,
            addRequest.CurrentLoad,
            addRequest.Latitude,
            addRequest.Longitude);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("empty/{mcpId}")]
    public IActionResult EmptyMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpService.EmptyMcp(mcpId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/full")]
    public List<Mcp> GetFullMcps()
    {
        var result = _mcpService.GetFullMcps();
        return result;
    }

    [HttpGet("get/in-range")]
    public List<Mcp> GetMcpsInRange(GetInRangeRequest getInRangeRequest)
    {
        var result = _mcpService.GetMcpsInRange(getInRangeRequest.Latitude,
            getInRangeRequest.Longitude,
            getInRangeRequest.Radius);
        return result;
    }

    [HttpGet("get/all")]
    public List<Mcp> GetAllMcps()
    {
        var result = _mcpService.GetAllMcps();
        return result;
    }

    [HttpDelete("delete/{mcpId}")]
    public IActionResult DeleteMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpService.DeleteMcp(mcpId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-load")]
    public IActionResult UpdateMcpCurrentLoad(UpdateLoadRequest updateLoadRequest)
    {
        var (success, result) = _mcpService.UpdateMcpCurrentLoad(updateLoadRequest.Id, updateLoadRequest.CurrentLoad);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}