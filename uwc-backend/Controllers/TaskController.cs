using Communications.Task;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.TaskEntry;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly IWorkService _workService;

    public TaskController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpPost("add")]
    public IActionResult AddTask(AddTaskRequest addTaskRequest)
    {
        var (success, result) = _workService.AddTask(addTaskRequest.Date,
            addTaskRequest.SupervisorId,
            addTaskRequest.WorkerId,
            addTaskRequest.RouteId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get/all/{employeeId}")]
    public List<TaskEntry> GetAllTasksOfEmployee([FromRoute] int employeeId)
    {
        var result = _workService.GetAllTasksOfEmployee(employeeId);
        return result;
    }

    [HttpDelete("delete/all/{employeeId}")]
    public IActionResult DeleteAllTasksOfEmployee([FromRoute] int employeeId)
    {
        var (success, result) = _workService.DeleteAllTasksOfEmployee(employeeId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("delete/{taskId}")]
    public IActionResult DeleteTask([FromRoute] int taskId)
    {
        var (success, result) = _workService.DeleteTask(taskId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public IActionResult UpdateTask(UpdateTaskRequest updateTaskRequest)
    {
        var (success, result) = _workService.UpdateTask(updateTaskRequest.Id,
            updateTaskRequest.Date,
            updateTaskRequest.SupervisorId,
            updateTaskRequest.WorkerId,
            updateTaskRequest.RouteId);

        if (!success) return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("free")]
    public List<UserProfile> GetFreeEmployees()
    {
        var result = _workService.GetFreeEmployees();
        return result;
    }
}