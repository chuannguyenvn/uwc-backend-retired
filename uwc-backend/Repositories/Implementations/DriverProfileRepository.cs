using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Implementations;

public class DriverProfileRepository : GenericRepository<DriverProfile>
{
    public DriverProfileRepository(UwcDbContext context) : base(context)
    {
    }

    public DriverProfile GetDriverProfileById(int id)
    {
        return _context.DriverProfiles.Include(x => x.DrivingHistories).Include(x => x.DrivingLicenses).FirstOrDefault(x => x.Id == id);
    }
}