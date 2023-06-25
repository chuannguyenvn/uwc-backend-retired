using Microsoft.AspNetCore.Mvc;
using Services.Routing;
using uwc_backend.Communications.Route;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService _routeService)
    {
        this._routeService = _routeService;
    }

    [HttpPost("add-route")]
    public IActionResult AddRoute(AddRouteRequest addRouteRequest)
    {
        var (success, result) = _routeService.AddRoute(addRouteRequest.RouteName, addRouteRequest.TotalLength,
            addRouteRequest.RouteDetails);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}