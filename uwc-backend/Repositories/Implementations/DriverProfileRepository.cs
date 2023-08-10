using Models;

namespace Repositories.Implementations;

public class DriverProfileRepository : GenericRepository<DriverProfile>
{
    public DriverProfileRepository(UwcDbContext context) : base(context)
    {
    }
}