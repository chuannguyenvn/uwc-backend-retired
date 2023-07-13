using Models;

namespace Repositories.Implementations;

public class DriveRepository : GenericRepository<DrivingHistory>
{
    public DriveRepository(UwcDbContext context) : base(context)
    {
    }
}