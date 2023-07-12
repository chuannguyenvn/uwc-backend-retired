using Models;

namespace Repositories.Implementations;

public class SupervisorProfileRepository : GenericRepository<SupervisorProfile>
{
    public SupervisorProfileRepository(UwcDbContext context) : base(context)
    {
    }
}