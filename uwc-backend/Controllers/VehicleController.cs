using Communications.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services.LiveData;
using Services.Vehicle;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly VehicleLocationService _vehicleLocationService;

    public VehicleController(IVehicleService vehicleService, VehicleLocationService vehicleLocationService)
    {
        _vehicleService = vehicleService;
        _vehicleLocationService = vehicleLocationService;
    }

    [HttpPost("add")]
    public IActionResult AddVehicle(AddVehicleRequest addVehicleRequest)
    {
        var (success, result) = _vehicleService.AddVehicle(addVehicleRequest.Capacity,
            addVehicleRequest.CurrentLoad,
            addVehicleRequest.AverageSpeed);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/all")]
    public List<Vehicle> GetAllVehicles()
    {
        var result = _vehicleService.GetAllVehicles();
        return result;
    }

    [HttpPut("update")]
    public IActionResult UpdateVehicleInformation(UpdateVehicleInformationRequest updateVehicleInformationRequest)
    {
        var (success, result) = _vehicleService.UpdateVehicle(updateVehicleInformationRequest.Id,
            updateVehicleInformationRequest.Capacity,
            updateVehicleInformationRequest.CurrentLoad,
            updateVehicleInformationRequest.AverageSpeed);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{vehicleId}")]
    public IActionResult DeleteVehicle([FromRoute] int vehicleId)
    {
        var (success, result) = _vehicleService.DeleteVehicle(vehicleId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-location/{vehicleId}")]
    public IActionResult UpdateVehicleLocation([FromRoute] int vehicleId, UpdateVehicleLocationRequest request)
    {
        var success = _vehicleLocationService.UpdateVehicleLocation(vehicleId, request.Coordinate);

        if (!success) return BadRequest();

        return Ok();
    }

    [HttpGet("get/all/location")]
    public IActionResult GetAllVehicleLocations()
    {
        var (success, result) = _vehicleLocationService.GetAllVehicleLocations();
        return Ok(JsonConvert.SerializeObject(result));
    }
}