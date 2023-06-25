using Repositories;

namespace Services.Mcp;

public interface IMcpService
{
    public (bool success, object result) AddMcp(double capacity, double currentLoad, double latitude,
        double longitude);
}

public class McpService : IMcpService
{
    private readonly UnitOfWork _unitOfWork;

    public McpService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddMcp(double capacity, double currentLoad, double latitude, double longitude)
    {
        var mcpInformation = new Models.Mcp()
        {
            Capacity = capacity,
            CurrentLoad = currentLoad,
            Latitude = latitude,
            Longitude = longitude,
        };
        
        _unitOfWork.Mcps.Add(mcpInformation);
        _unitOfWork.Complete();
        
        return (true, "Add new mcp successfully.");
    }
}