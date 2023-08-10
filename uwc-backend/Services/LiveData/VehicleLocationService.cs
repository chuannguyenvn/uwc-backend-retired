namespace Services.LiveData;

public class VehicleLocationService : IHostedService, IDisposable
{
    private int executionCount;
    private Timer? _timer;

    public Task StartAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}