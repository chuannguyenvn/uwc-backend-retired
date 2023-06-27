using Models;
using Repositories;

namespace Services.Task;

public interface ITaskIncludeMcpSerivce
{
    public (bool success, object result) AddTaskIncludeMcp(int taskId, int mcpId);
}

public class TaskIncludeMcpService : ITaskIncludeMcpSerivce
{
    private readonly UnitOfWork _unitOfWork;

    public TaskIncludeMcpService(UnitOfWork _unitOfWork)
    {
        this._unitOfWork = _unitOfWork;
    }

    public (bool success, object result) AddTaskIncludeMcp(int taskId, int mcpId)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(taskId))
        {
            return (false, "Task Id does not exist.");
        }

        if (!_unitOfWork.Mcps.DoesIdExist(mcpId))
        {
            return (false, "Mcp Id does not exist.");
        }

        var task = _unitOfWork.Tasks.Find(task => task.Id == taskId).First();
        var mcp = _unitOfWork.Mcps.Find(mcp => mcp.Id == mcpId).First();
        var taskIncludeMcpInformation = new TaskIncludeMcp()
        {
            Task = task,
            Mcp = mcp,
        };
        
        _unitOfWork.TaskIncludeMcps.Add(taskIncludeMcpInformation);
        _unitOfWork.Complete();

        return (true, "Add task-include-mcp successfully.");
    }
}