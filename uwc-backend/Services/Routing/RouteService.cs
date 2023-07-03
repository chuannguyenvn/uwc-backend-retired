using Repositories;
using Route = Models.Route;

namespace Services.Routing;

public interface IRouteService
{
    public (bool success, object result) AddRoute(string name, double totalLength, string routeDetails);

    public (bool success, object result) UpdateRoute(int id, string name, double totalLength, string routeDetails);
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
        var routeInformation = new Route {RouteName = name, TotalLength = totalLength, RouteDetails = routeDetails};

        _unitOfWork.Routes.Add(routeInformation);
        _unitOfWork.Complete();
        return (true, "Add route successfully");
    }

    public (bool success, object result) UpdateRoute(int id, string name, double totalLength, string routeDetails)
    {
        if (!_unitOfWork.Routes.DoesIdExist(id)) return (false, "Route Id does not exist.");

        var route = _unitOfWork.Routes.Find(route => route.Id == id).First();

        route.RouteName = name;
        route.TotalLength = totalLength;
        route.RouteDetails = routeDetails;

        _unitOfWork.Complete();
        return (true, "Update route details successfully");
    }
}