namespace Repositories.Implementations;

public class VehicleRepository : GenericRepository<Models.Vehicle>
{
    public VehicleRepository(UwcDbContext context) : base(context)
    {
    }
}