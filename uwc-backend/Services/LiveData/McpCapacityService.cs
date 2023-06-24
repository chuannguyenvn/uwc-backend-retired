using Repositories;
using Utilities;

namespace Services.LiveData;

public class McpCapacityService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public McpCapacityService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public int GetMcpsWithHighLoadCount()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        return unitOfWork.Mcps.GetMcpsWithMinimumLoad(Constants.MCP_NEARLY_FULL_LOAD).Count();
    }
}