using Models;

namespace Repositories.Implementations;

public class TaskIncludeMcpRepository : GenericRepository<TaskIncludeMcp>
{
    public TaskIncludeMcpRepository(UwcDbContext context) : base(context)
    {
    }
}