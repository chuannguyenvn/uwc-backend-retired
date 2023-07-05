using Models;
using Repositories;

namespace Services.Task;

public class TaskIncludeMcpService : ITaskIncludeMcpSerivce
{
    private readonly UnitOfWork _unitOfWork;

    public TaskIncludeMcpService(UnitOfWork _unitOfWork)
    {
        this._unitOfWork = _unitOfWork;
    }

    public (bool success, object result) AddTaskIncludeMcp(int taskId, int mcpId)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(taskId)) return (false, "Task Id does not exist.");

        if (!_unitOfWork.Mcps.DoesIdExist(mcpId)) return (false, "Mcp Id does not exist.");

        var task = _unitOfWork.Tasks.Find(task => task.Id == taskId).First();
        var mcp = _unitOfWork.Mcps.Find(mcp => mcp.Id == mcpId).First();
        var taskIncludeMcpInformation = new TaskIncludeMcp {Task = task, Mcp = mcp};

        _unitOfWork.TaskIncludeMcps.Add(taskIncludeMcpInformation);
        _unitOfWork.Complete();

        return (true, "Add task-include-mcp successfully.");
    }

    public (bool success, object result) DeleteTaskIncludeMcp(int id)
    {
        if (!_unitOfWork.TaskIncludeMcps.DoesIdExist(id))
            return (false, "Task Include Mcp Id does not exist.");

        var taskIncludeMcp = _unitOfWork.TaskIncludeMcps.Find(record => record.Id == id).First();
        _unitOfWork.TaskIncludeMcps.Remove(taskIncludeMcp);
        _unitOfWork.Complete();

        return (true, "Task Include Mcp deleted successfully.");
    }

    public List<TaskIncludeMcp> GetMcpsInTask(int taskId)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(taskId)) return new List<TaskIncludeMcp>();

        var mcpList = _unitOfWork.TaskIncludeMcps.Find(tim => tim.Task.Id == taskId);
        return mcpList.ToList();
    }

    public List<TaskIncludeMcp> GetTasksBypassMcp(int mcpId)
    {
        if (!_unitOfWork.Mcps.DoesIdExist(mcpId)) return new List<TaskIncludeMcp>();

        var taskList = _unitOfWork.TaskIncludeMcps.Find(tim => tim.Mcp.Id == mcpId);
        return taskList.ToList();
    }
}