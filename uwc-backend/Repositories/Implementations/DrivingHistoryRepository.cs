using Models;

namespace Repositories.Implementations;

public class DrivingHistoryRepository : GenericRepository<DrivingHistory>
{
    public DrivingHistoryRepository(UwcDbContext context) : base(context)
    {
    }
}