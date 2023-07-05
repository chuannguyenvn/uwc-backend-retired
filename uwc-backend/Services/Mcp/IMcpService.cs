namespace Services.Mcp;

public interface IMcpService
{
    public (bool success, object result) AddMcp(double capacity, double currentLoad, double latitude, double longitude);

    public (bool success, object result) EmptyMcp(int mcpId);

    public List<Models.Mcp> GetFullMcps();

    public List<Models.Mcp> GetMcpsInRange(double latitude, double longitude, double radius);

    public List<Models.Mcp> GetAllMcps();

    public (bool success, object result) DeleteMcp(int id);

    public (bool success, object result) UpdateMcpCurrentLoad(int id, double currentLoad);

    public List<Models.Mcp> SortByCurrentLoadDescendingly();

    public List<Models.Mcp> SortByCurrentLoadAscendingly();

    public List<Models.Mcp> SortByDistanceDescendingly(double latitude, double longitude);

    public List<Models.Mcp> SortByDistanceAscendingly(double latitude, double longitude);
}