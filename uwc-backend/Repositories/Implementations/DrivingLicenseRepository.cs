using Models;

namespace Repositories.Implementations;

public class DrivingLicenseRepository : GenericRepository<DrivingLicense>
{
    public DrivingLicenseRepository(UwcDbContext context) : base(context)
    {
    }
}