using Models;
using Repositories.Implementations;

namespace Repositories;

public class UnitOfWork : IDisposable
{
    private readonly UwcDbContext _uwcDbContext;

    public AccountRepository Accounts { get; private set; }
    public EmployeeRepository Employees { get; private set; }
    public McpRepository Mcps { get; private set; }
    public TaskRepository Tasks { get; private set; }
    public MessageRepository Messages { get; private set; }
    public RouteRepository Routes { get; private set; }

    public UnitOfWork(UwcDbContext uwcDbContext)
    {
        _uwcDbContext = uwcDbContext;

        Accounts = new AccountRepository(_uwcDbContext);
        Employees = new EmployeeRepository(_uwcDbContext);
        Mcps = new McpRepository(_uwcDbContext);
        Tasks = new TaskRepository(_uwcDbContext);
        Messages = new MessageRepository(_uwcDbContext);
        Routes = new RouteRepository(_uwcDbContext);
    }

    public int Complete()
    {
        return _uwcDbContext.SaveChanges();
    }

    public Task<int> CompleteAsync()
    {
        return _uwcDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _uwcDbContext.Dispose();
    }
}