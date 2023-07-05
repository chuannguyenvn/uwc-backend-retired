using Models;

namespace Services.Task;

public interface ITaskIncludeMcpSerivce
{
    public (bool success, object result) AddTaskIncludeMcp(int taskId, int mcpId);
    public (bool success, object result) DeleteTaskIncludeMcp(int id);
    public List<TaskIncludeMcp> GetMcpsInTask(int taskId);
    public List<TaskIncludeMcp> GetTasksBypassMcp(int mcpId);
}