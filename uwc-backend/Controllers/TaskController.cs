using Microsoft.AspNetCore.Mvc;
using Services.Task;
using uwc_backend.Communications.Task;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("add-task")]
    public IActionResult AddTask(AddTaskRequest addTaskRequest)
    {
        var (success, result) = _taskService.AddTask(addTaskRequest.Date, addTaskRequest.Supervisor,
            addTaskRequest.Worker, addTaskRequest.Route);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}