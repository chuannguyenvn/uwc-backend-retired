﻿using Repositories.Implementations;

namespace Repositories;

public class UnitOfWork : IDisposable
{
    private readonly UwcDbContext _uwcDbContext;

    public UnitOfWork(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;

        Accounts = new AccountRepository(_uwcDbContext);
        SupervisorProfiles = new SupervisorProfileRepository(_uwcDbContext);
        EmployeeProfiles = new EmployeeProfileRepository(_uwcDbContext);
        Mcps = new McpRepository(_uwcDbContext);
        Tasks = new TaskRepository(_uwcDbContext);
        Messages = new MessageRepository(_uwcDbContext);
        Routes = new RouteRepository(_uwcDbContext);
        TaskIncludeMcps = new TaskIncludeMcpRepository(_uwcDbContext);
        Vehicles = new VehicleRepository(_uwcDbContext);
        DrivingLicenses = new DrivingLicenseRepository(_uwcDbContext);
        Drives = new DriveRepository(_uwcDbContext);
    }

    public AccountRepository Accounts { get; }
    public EmployeeProfileRepository EmployeeProfiles { get; }
    public SupervisorProfileRepository SupervisorProfiles { get; }
    public McpRepository Mcps { get; }
    public TaskRepository Tasks { get; }
    public MessageRepository Messages { get; }
    public RouteRepository Routes { get; }
    public TaskIncludeMcpRepository TaskIncludeMcps { get; }
    public VehicleRepository Vehicles { get; }
    public DriveRepository Drives { get; }
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