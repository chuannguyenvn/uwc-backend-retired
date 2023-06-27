using Models;
using Repositories;

namespace Services.Task;

public interface ITaskIncludeMcpSerivce
{
    public (bool success, object result) AddTaskIncludeMcp(int taskId, int mcpId);
    public (bool success, object result) DeleteTaskIncludeMcp(int id);
    public List<Models.TaskIncludeMcp> GetMcpsInTask(int taskId);
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

    public (bool success, object result) DeleteTaskIncludeMcp(int id)
    {
        if (!_unitOfWork.TaskIncludeMcps.DoesIdExist(id))
        {
            return (false, "Task Include Mcp Id does not exist.");
        }

        var taskIncludeMcp = _unitOfWork.TaskIncludeMcps.Find(record => record.Id == id).First();
        _unitOfWork.TaskIncludeMcps.Remove(taskIncludeMcp);
        _unitOfWork.Complete();

        return (true, "Task Include Mcp deleted successfully.");
    }

    public List<Models.TaskIncludeMcp> GetMcpsInTask(int taskId)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(taskId)) return new List<Models.TaskIncludeMcp>();

        var mcpList = _unitOfWork.TaskIncludeMcps.Find(tim => tim.Task.Id == taskId);
        return mcpList.ToList();
    }
}