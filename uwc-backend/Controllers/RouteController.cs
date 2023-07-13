using Communications.Route;
using Microsoft.AspNetCore.Mvc;
using Services.Routing;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpPost("add")]
    public IActionResult AddRoute(AddRouteRequest addRouteRequest)
    {
        var (success, result) = _routeService.AddRoute(addRouteRequest.RouteName,
            addRouteRequest.TotalLength,
            addRouteRequest.RouteDetails);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult UpdateRoute(UpdateRouteRequest updateRouteRequest)
    {
        var (success, result) = _routeService.UpdateRoute(updateRouteRequest.Id,
            updateRouteRequest.RouteName,
            updateRouteRequest.TotalLength,
            updateRouteRequest.RouteDetails);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}