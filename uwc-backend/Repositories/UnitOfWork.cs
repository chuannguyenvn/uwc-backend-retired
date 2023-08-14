using Repositories.Implementations;

namespace Repositories;

public class UnitOfWork : IDisposable
{
    private readonly UwcDbContext _uwcDbContext;

    public UnitOfWork(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;

        Accounts = new AccountRepository(_uwcDbContext);
        SupervisorProfiles = new SupervisorProfileRepository(_uwcDbContext);
        DriverProfiles = new DriverProfileRepository(_uwcDbContext);
        CleanerProfiles = new CleanerProfileRepository(_uwcDbContext);
        Mcps = new McpRepository(_uwcDbContext);
        TaskEntries = new TaskEntryRepository(_uwcDbContext);
        Messages = new MessageRepository(_uwcDbContext);
        Vehicles = new VehicleRepository(_uwcDbContext);
        DrivingLicenses = new DrivingLicenseRepository(_uwcDbContext);
        DrivingHistories = new DrivingHistoryRepository(_uwcDbContext);
    }

    public AccountRepository Accounts { get; }
    public DriverProfileRepository DriverProfiles { get; }
    public CleanerProfileRepository CleanerProfiles { get; }
    public SupervisorProfileRepository SupervisorProfiles { get; }
    public McpRepository Mcps { get; }
    public TaskEntryRepository TaskEntries { get; }
    public MessageRepository Messages { get; }
    public VehicleRepository Vehicles { get; }
    public DrivingHistoryRepository DrivingHistories { get; }
    public DrivingLicenseRepository DrivingLicenses { get; }

    public void Dispose()
    {
        _uwcDbContext.Dispose();
    }

    public int Complete()
    {
        return _uwcDbContext.SaveChanges();
    }

    public Task<int> CompleteAsync()
    {
        return _uwcDbContext.SaveChangesAsync();
    }
}