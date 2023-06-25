using Repositories;

namespace Services.Routing;

public interface IRouteService
{
    public (bool success, object result) AddRoute(string name, double totalLength, string routeDetails);
}

public class RouteService : IRouteService
{
    private readonly UnitOfWork _unitOfWork;

    public RouteService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public (bool success, object result) AddRoute(string name, double totalLength, string routeDetails)
    {
        var routeInformation = new Models.Route()
        {
            RouteName = name,
            TotalLength = totalLength,
            RouteDetails = routeDetails,
        };
        
        _unitOfWork.Routes.Add(routeInformation);
        _unitOfWork.Complete();
        return (true, "Add route successfully");
    }
}