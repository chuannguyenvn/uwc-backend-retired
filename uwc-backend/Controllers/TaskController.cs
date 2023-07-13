using Communications.Task;
using Microsoft.AspNetCore.Mvc;
using Services.Task;
using Task = Models.Task;

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

    [HttpPost("add")]
    public IActionResult AddTask(AddTaskRequest addTaskRequest)
    {
        var (success, result) = _taskService.AddTask(addTaskRequest.Date,
            addTaskRequest.Supervisor,
            addTaskRequest.Worker,
            addTaskRequest.Route);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/all/{employeeId}")]
    public List<Task> GetAllTasksOfEmployee([FromRoute] int employeeId)
    {
        var result = _taskService.GetAllTasksOfEmployee(employeeId);
        return result;
    }

    [HttpDelete("delete/all/{employeeId}")]
    public IActionResult DeleteAllTasksOfEmployee([FromRoute] int employeeId)
    {
        var (success, result) = _taskService.DeleteAllTasksOfEmployee(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{taskId}")]
    public IActionResult DeleteTask([FromRoute] int taskId)
    {
        var (success, result) = _taskService.DeleteTask(taskId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult UpdateTask(UpdateTaskRequest updateTaskRequest)
    {
        var (success, result) = _taskService.UpdateTask(updateTaskRequest.Id,
            updateTaskRequest.Date,
            updateTaskRequest.Supervisor,
            updateTaskRequest.Worker,
            updateTaskRequest.Route);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
}