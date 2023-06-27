using Repositories;

namespace Repositories.Implementations;

public class TaskIncludeMcpRepository : GenericRepository<Models.TaskIncludeMcp>
{
    public TaskIncludeMcpRepository(UwcDbContext context) : base(context)
    {
    }
}