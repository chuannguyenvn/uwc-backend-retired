using Repositories;
using Utilities;

namespace Services.LiveData;

public class McpCapacityService : IHostedService, IDisposable
{
    private readonly Random _random = new();
    private readonly IServiceProvider _serviceProvider;
    private List<Models.Mcp> _allMcps;
    private Timer? _databasePersistTimer;
    private Timer? _fillTimer;

    public McpCapacityService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispose()
    {
        _fillTimer?.Dispose();
        _databasePersistTimer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        RetrieveMcps();

        _fillTimer = new Timer(FillMcps, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        _databasePersistTimer = new Timer(PersistMcpStates, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void RetrieveMcps()
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        _allMcps = unitOfWork.Mcps.GetAll().ToList();
    }

    private void FillMcps(object? state)
    {
        foreach (var mcp in _allMcps)
        {
            if (mcp.CurrentLoad / mcp.Capacity > 1.2f) return;
            mcp.CurrentLoad += _random.Next(30, 70);
        }

        Console.WriteLine("Filled.");
    }

    private void PersistMcpStates(object? state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

            var mcps = unitOfWork.Mcps.GetAll().ToList();
            for (var i = 0; i < mcps.Count; i++) mcps[i].CurrentLoad = _allMcps[i].CurrentLoad;

            unitOfWork.Complete();
        }

        Console.WriteLine("Persisted.");
    }

    public (bool success, object result) EmptyMcp(int mcpId)
    {
        _allMcps.First(mcp => mcp.Id == mcpId).CurrentLoad = 0;
        return (true, "Success.");
    }

    public Models.Mcp GetMostFullMcp()
    {
        return _allMcps.MaxBy(mcp => mcp.CurrentLoad);
    }

    public Models.Mcp GetRandomMcp()
    {
        return _allMcps.GetRandomElement();
    }
}