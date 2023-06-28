using Models;

namespace Repositories.Implementations;

public class DriveRepository : GenericRepository<Drive>
{
    public DriveRepository(UwcDbContext context) : base(context)
    {
    }
}