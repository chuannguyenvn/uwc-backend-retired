namespace Repositories.Implementations;

public class DrivingLicenseRepository : GenericRepository<Models.DrivingLicense>
{
    public DrivingLicenseRepository(UwcDbContext context) : base(context)
    {
    }
}