using Microsoft.AspNetCore.Mvc;
using Models;
using Services.LiveData;
using Services.Mcp;
using Commons.Communications.Mcp;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class McpController : ControllerBase
{
    private readonly McpCapacityService _mcpCapacityService;
    private readonly IMcpService _mcpService;

    public McpController(IMcpService mcpService, McpCapacityService mcpCapacityService)
    {
        _mcpService = mcpService;
        _mcpCapacityService = mcpCapacityService;
    }

    [HttpPost("add")]
    public IActionResult AddMcp(AddMcpRequest addMcpRequest)
    {
        var (success, result) = _mcpService.AddMcp(addMcpRequest.Capacity,
            addMcpRequest.CurrentLoad,
            addMcpRequest.Latitude,
            addMcpRequest.Longitude);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("empty/{mcpId}")]
    public IActionResult EmptyMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpCapacityService.EmptyMcp(mcpId);

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
    public List<Mcp> GetMcpsInRange(GetMcpsInRangeRequest getMcpsInRangeRequest)
    {
        var result = _mcpService.GetMcpsInRange(getMcpsInRangeRequest.Latitude,
            getMcpsInRangeRequest.Longitude,
            getMcpsInRangeRequest.Radius);
        return result;
    }

    [HttpGet("get/all")]
    public List<Mcp> GetAllMcps()
    {
        var result = _mcpCapacityService.GetAllMcps();
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
    public IActionResult UpdateMcpCurrentLoad(UpdateMcpLoadRequest updateMcpLoadRequest)
    {
        var (success, result) = _mcpService.UpdateMcpCurrentLoad(updateMcpLoadRequest.Id, updateMcpLoadRequest.CurrentLoad);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}