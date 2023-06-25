using Models;

namespace Repositories.Implementations;

public class RouteRepository : GenericRepository<Models.Route>
{
    public RouteRepository(UwcDbContext context) : base(context)
    {
    }
}