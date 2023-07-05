namespace Services.Routing;

public interface IRouteService
{
    public (bool success, object result) AddRoute(string name, double totalLength, string routeDetails);

    public (bool success, object result) UpdateRoute(int id, string name, double totalLength, string routeDetails);
}