using Repositories;

namespace Repositories.Implementations;

public class TaskRepository : GenericRepository<Models.Task>
{
    public TaskRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Models.Task> GetTasksOfEmployee(int employeeId)
    {
        return _context.Tasks.Where(task => task.Worker.Id == employeeId);
    }
}