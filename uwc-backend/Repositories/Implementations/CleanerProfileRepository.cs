using Models;

namespace Repositories.Implementations;

public class CleanerProfileRepository : GenericRepository<CleanerProfile>
{
    public CleanerProfileRepository(UwcDbContext context) : base(context)
    {
    }
}