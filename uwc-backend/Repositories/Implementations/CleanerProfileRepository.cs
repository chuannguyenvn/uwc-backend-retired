using Models;
using Models.Types;

namespace Repositories.Implementations;

public class CleanerProfileRepository : GenericRepository<CleanerProfile>
{
    public CleanerProfileRepository(UwcDbContext context) : base(context)
    {
    }
}