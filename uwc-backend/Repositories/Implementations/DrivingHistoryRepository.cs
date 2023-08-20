using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Implementations;

public class DrivingHistoryRepository : GenericRepository<DrivingHistory>
{
    public DrivingHistoryRepository(UwcDbContext context) : base(context)
    {
    }

    public DrivingHistory GetLatestDrivingHistoryOfDriver(int driverId)
    {
        return _context.DrivingHistories.Include(history => history.VehicleUsed)
            .Where(driver => driver.Id == driverId)
            .OrderByDescending(drivingHistory => drivingHistory.Timestamp)
            .FirstOrDefault();
    }
}