using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Task;
using uwc_backend.Communications.Task;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TaskIncludeMcpController : ControllerBase
{
    private readonly ITaskIncludeMcpSerivce _taskIncludeMcpSerivce;

    public TaskIncludeMcpController(ITaskIncludeMcpSerivce taskIncludeMcpSerivce)
    {
        _taskIncludeMcpSerivce = taskIncludeMcpSerivce;
    }

    [HttpPost("add-task-include-mcp")]
    public IActionResult AddTaskIncludeMcp(AddTaskIncludeMcpRequest addTaskIncludeMcpRequest)
    {
        var (success, result) =
            _taskIncludeMcpSerivce.AddTaskIncludeMcp(addTaskIncludeMcpRequest.Task, addTaskIncludeMcpRequest.Mcp);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-task-include-mcp/{taskIncludeMcpId}")]
    public IActionResult DeleteTaskIncludeMcp([FromRoute] int taskIncludeMcpId)
    {
        var (success, result) = _taskIncludeMcpSerivce.DeleteTaskIncludeMcp(taskIncludeMcpId);
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}