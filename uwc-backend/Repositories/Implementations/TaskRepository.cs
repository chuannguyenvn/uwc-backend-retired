using Task = Models.Task;

namespace Repositories.Implementations;

public class TaskRepository : GenericRepository<Task>
{
    public TaskRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Task> GetTasksOfEmployee(int employeeId)
    {
        return _context.Tasks.Where(task => task.Worker.Id == employeeId);
    }
}