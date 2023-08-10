using Repositories;

namespace Services.LiveData;

public class McpCapacityService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private List<Models.Mcp> _allMcps = new();
    private Timer? _fillTimer;
    private Timer? _databasePersistTimer;

    public McpCapacityService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _fillTimer = new Timer(FillMcps, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        _databasePersistTimer = new Timer(PersistMcpStates, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private void FillMcps(object? state)
    {
    }

    private void PersistMcpStates(object? state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

            foreach (var mcp in _allMcps)
            {
                unitOfWork.Mcps.GetById(mcp.Id).CurrentLoad = mcp.CurrentLoad;
            }

            unitOfWork.Complete();
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _fillTimer?.Dispose();
    }
}