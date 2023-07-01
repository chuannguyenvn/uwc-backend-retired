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

    [HttpPost("add-task")]
    public IActionResult AddTask(AddTaskRequest addTaskRequest)
    {
        var (success, result) = _taskService.AddTask(addTaskRequest.Date,
            addTaskRequest.Supervisor,
            addTaskRequest.Worker,
            addTaskRequest.Route);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-all-tasks/{employeeId}")]
    public List<Task> GetTasksOfEmployee([FromRoute] int employeeId)
    {
        var result = _taskService.GetTasksOfEmployee(employeeId);
        return result;
    }

    [HttpDelete("delete-all-task/{employeeId}")]
    public IActionResult DeleteTasksOfEmployee([FromRoute] int employeeId)
    {
        var (success, result) = _taskService.DeleteTasksOfEmployee(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete-task/{taskId}")]
    public IActionResult DeleteTask([FromRoute] int taskId)
    {
        var (success, result) = _taskService.DeleteTask(taskId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-task")]
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

    [HttpGet("get-tasks-in-time-range")]
    public List<Task> GetTasksInTimeRange(GetTasksInTimeRangeRequest getTasksInTimeRangeRequest)
    {
        var result = _taskService.GetTasksInTimeRange(getTasksInTimeRangeRequest.StartTime,
            getTasksInTimeRangeRequest.EndTime);

        return result;
    }
}