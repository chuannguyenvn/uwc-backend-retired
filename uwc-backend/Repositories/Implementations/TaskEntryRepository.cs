using Models;

namespace Repositories.Implementations;

public class TaskEntryRepository : GenericRepository<TaskEntry>
{
    public TaskEntryRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<TaskEntry> GetTasksOfEmployee(int employeeId)
    {
        return _context.Tasks.Where(task => task.Worker.Id == employeeId);
    }
}