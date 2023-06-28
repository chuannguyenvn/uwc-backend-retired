using Models;

namespace Repositories.Implementations;

public class VehicleRepository : GenericRepository<Vehicle>
{
    public VehicleRepository(UwcDbContext context) : base(context)
    {
    }
}