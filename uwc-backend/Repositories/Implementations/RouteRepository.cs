using Route = Models.Route;

namespace Repositories.Implementations;

public class RouteRepository : GenericRepository<Route>
{
    public RouteRepository(UwcDbContext context) : base(context)
    {
    }
}