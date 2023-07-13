using Repositories;

namespace Services.Mcp;

public class McpService : IMcpService
{
    private readonly UnitOfWork _unitOfWork;

    public McpService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddMcp(float capacity, float currentLoad, double latitude, double longitude)
    {
        var mcpInformation = new Models.Mcp {Capacity = capacity, CurrentLoad = currentLoad, Latitude = latitude, Longitude = longitude};

        _unitOfWork.Mcps.Add(mcpInformation);
        _unitOfWork.Complete();

        return (true, "Add new mcp successfully.");
    }

    public (bool success, object result) EmptyMcp(int mcpId)
    {
        var mcpList = _unitOfWork.Mcps.Find(mcp => mcp.Id == mcpId);
        if (mcpList.Count() == 0) return (false, "Mcp does not exist.");

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
        var mcpList = _unitOfWork.Mcps.Find(mcp =>
            Math.Pow(mcp.Longitude - longitude, 2) + Math.Pow(mcp.Latitude - latitude, 2) <= Math.Pow(radius, 2));
        return mcpList.ToList();
    }

    public List<Models.Mcp> GetAllMcps()
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.ToList();
    }

    public (bool success, object result) DeleteMcp(int id)
    {
        if (!_unitOfWork.Mcps.DoesIdExist(id)) return (false, "Mcp Id does not exist.");

        _unitOfWork.Mcps.RemoveById(id);
        _unitOfWork.Complete();
        return (true, "Mcp deleted successfully.");
    }

    public (bool success, object result) UpdateMcpCurrentLoad(int id, float currentLoad)
    {
        if (!_unitOfWork.Mcps.DoesIdExist(id)) return (false, "Mcp Id does not exist.");

        var mcp = _unitOfWork.Mcps.Find(mcp => mcp.Id == id).First();
        mcp.CurrentLoad = currentLoad;

        _unitOfWork.Complete();
        return (true, "Mcp current load updated successfully");
    }

    public List<Models.Mcp> SortByCurrentLoadDescendingly()
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.OrderByDescending(mcp => mcp.CurrentLoad).ToList();
    }

    public List<Models.Mcp> SortByCurrentLoadAscendingly()
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.OrderBy(mcp => mcp.CurrentLoad).ToList();
    }

    public List<Models.Mcp> SortByDistanceDescendingly(double latitude, double longitude)
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.OrderByDescending(mcp => Math.Pow(latitude - mcp.Latitude, 2) + Math.Pow(longitude - mcp.Longitude, 2)).ToList();
    }

    public List<Models.Mcp> SortByDistanceAscendingly(double latitude, double longitude)
    {
        var mcpList = _unitOfWork.Mcps.GetAll();
        return mcpList.OrderBy(mcp => Math.Pow(latitude - mcp.Latitude, 2) + Math.Pow(longitude - mcp.Longitude, 2)).ToList();
    }
}