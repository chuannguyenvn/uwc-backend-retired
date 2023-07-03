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

    [HttpPost("add-mcp")]
    public IActionResult AddMcp(AddMcpRequest addMcpRequest)
    {
        var (success, result) = _mcpService.AddMcp(addMcpRequest.Capacity,
            addMcpRequest.CurrentLoad,
            addMcpRequest.Latitude,
            addMcpRequest.Longitude);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("empty-mcp/{mcpId}")]
    public IActionResult EmptyMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpService.EmptyMcp(mcpId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-full-mcps")]
    public List<Mcp> GetFullMcps()
    {
        var result = _mcpService.GetFullMcps();
        return result;
    }

    [HttpGet("get-mcps-in-range")]
    public List<Mcp> GetMcpsInRange(GetMcpsInRangeRequest getMcpsInRangeRequest)
    {
        var result = _mcpService.GetMcpsInRange(getMcpsInRangeRequest.Latitude,
            getMcpsInRangeRequest.Longitude,
            getMcpsInRangeRequest.Radius);
        return result;
    }

    [HttpGet("get-all-mcps")]
    public List<Mcp> GetAllMcps()
    {
        var result = _mcpService.GetAllMcps();
        return result;
    }

    [HttpDelete("delete-mcp-id/{mcpId}")]
    public IActionResult DeleteMcp([FromRoute] int mcpId)
    {
        var (success, result) = _mcpService.DeleteMcp(mcpId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-mcp-current-load")]
    public IActionResult UpdateMcpCurrentLoad(UpdateMcpCurrentLoad updateMcpCurrentLoad)
    {
        var (success, result) = _mcpService.UpdateMcpCurrentLoad(updateMcpCurrentLoad.Id, updateMcpCurrentLoad.CurrentLoad);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("sort-current-load-ascendingly")]
    public List<Mcp> SortMcpByCurrentLoadAscendingly()
    {
        var result = _mcpService.SortByCurrentLoadAscendingly();
        return result;
    }

    [HttpGet("sort-current-load-descendingly")]
    public List<Mcp> SortMcpByCurrentLoadDescendingly()
    {
        var result = _mcpService.SortByCurrentLoadDescendingly();
        return result;
    }

    [HttpGet("sort-distance-ascendingly")]
    public List<Mcp> SortMcpByDistanceAscendingly(SortDistanceRequest sortDistanceRequest)
    {
        var result = _mcpService.SortByDistanceAscendingly(sortDistanceRequest.Latitude, sortDistanceRequest.Longitude);
        return result;
    }

    [HttpGet("sort-distance-descendingly")]
    public List<Mcp> SortMcpByDistanceDescendingly(SortDistanceRequest sortDistanceRequest)
    {
        var result = _mcpService.SortByDistanceDescendingly(sortDistanceRequest.Latitude, sortDistanceRequest.Longitude);
        return result;
    }
}