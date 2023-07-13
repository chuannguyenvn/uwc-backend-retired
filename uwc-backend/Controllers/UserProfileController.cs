using Communications.Employee;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Types;
using Services.Profile;
using Services.Task;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService, ITaskService taskService)
    {
        _userProfileService = userProfileService;
    }

    [HttpPost("add/supervisor")]
    public async Task<IActionResult> AddSupervisorProfile(AddUserProfileRequest addUserProfileRequest)
    {
        var (success, result) = await _userProfileService.AddSupervisorProfile(addUserProfileRequest.FirstName,
            addUserProfileRequest.LastName,
            addUserProfileRequest.Gender,
            addUserProfileRequest.DateOfBirth);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("add/cleaner")]
    public async Task<IActionResult> AddCleanerProfile(AddUserProfileRequest addUserProfileRequest)
    {
        var (success, result) = await _userProfileService.AddCleanerProfile(addUserProfileRequest.FirstName,
            addUserProfileRequest.LastName,
            addUserProfileRequest.Gender,
            addUserProfileRequest.DateOfBirth);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("add/driver")]
    public async Task<IActionResult> AddDriverProfile(AddUserProfileRequest addUserProfileRequest)
    {
        var (success, result) = await _userProfileService.AddDriverProfile(addUserProfileRequest.FirstName,
            addUserProfileRequest.LastName,
            addUserProfileRequest.Gender,
            addUserProfileRequest.DateOfBirth);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{userProfileId}")]
    public async Task<IActionResult> DeleteUserProfile([FromRoute] int userProfileId)
    {
        var (success, result) = await _userProfileService.DeleteUserProfile(userProfileId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserProfile(UpdateUserProfile updateUserProfile)
    {
        var (success, result) = await _userProfileService.UpdateUserProfile(updateUserProfile.UserProfileId,
            updateUserProfile.FirstName,
            updateUserProfile.LastName,
            updateUserProfile.Gender,
            updateUserProfile.DateOfBirth,
            updateUserProfile.Role);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUserProfiles()
    {
        var result = _userProfileService.GetAllUserProfiles();
        return Ok(result);
    }

    [HttpGet("info/{userProfileId}")]
    public async Task<IActionResult> GetEmployeeById([FromRoute] int userProfileId)
    {
        var (success, message, result) = await _userProfileService.GetUserProfileById(userProfileId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("with-role/{role}")]
    public async Task<IActionResult> GetAllEmployeesWithRole([FromRoute] UserRole role)
    {
        var result = await _userProfileService.GetAllEmployeesWithRole(role);
        return Ok(result.result);
    }
}