using Repositories;

namespace Services.Mcp;

public interface IMcpService
{
    public (bool success, object result) AddMcp(double capacity, double currentLoad, double latitude,
        double longitude);

    public (bool success, object result) EmptyMcp(int mcpId);

    public List<Models.Mcp> GetFullMcps();

    public List<Models.Mcp> GetMcpsInRange(double latitude, double longitude, double radius);

    public List<Models.Mcp> GetAllMcps();
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

    public (bool success, object result) EmptyMcp(int mcpId)
    {
        var mcpList = _unitOfWork.Mcps.Find(mcp => mcp.Id == mcpId);
        if (mcpList.Count() == 0)
        {
            return (false, "Mcp does not exist.");
        }

        var mcp = mcpList.First();
        mcp.CurrentLoad = 0.0f;
        _unitOfWork.Complete();

        return (true, "Empty mcp successfully.");
    }

    public List<Models.Mcp> GetFullMcps()
    {
        var mcpList = _unitOfWork.Mcps.Find(mcp => Math.Abs(mcp.CurrentLoad - 100.0) < 0.01);
        return mcpList.ToList();
    }

    public List<Models.Mcp> GetMcpsInRange(double latitude, double longitude, double radius)
    {
        var mcpList = _unitOfWork.Mcps.Find(
            mcp => Math.Pow(mcp.Longitude - longitude, 2) + Math.Pow(mcp.Latitude - latitude, 2) <=
                   Math.Pow(radius, 2));
        return mcpList.ToList();
    }

    public List<Models.Mcp> GetAllMcps()
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.ToList();
    }
}