﻿namespace Services.Mcp;

public interface IMcpService
{
    public (bool success, object result) AddMcp(float capacity, float currentLoad, double latitude, double longitude);

    public (bool success, object result) EmptyMcp(int mcpId);

    public List<Models.Mcp> GetFullMcps();

    public List<Models.Mcp> GetMcpsInRange(double latitude, double longitude, double radius);

    public List<Models.Mcp> GetAllMcps();

    public (bool success, object result) DeleteMcp(int id);

    public (bool success, object result) UpdateMcpCurrentLoad(int id, float currentLoad);
}