using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class UwcDbContext : DbContext
{
    public UwcDbContext(DbContextOptions<UwcDbContext> options) : base(options)
    {
    }

    public DbSet<Mcp> Mcps { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Models.Route> Routes { get; set; }
    public DbSet<TaskIncludeMcp> TaskIncludeMcps { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Drive> Drives { get; set; }
    public DbSet<DrivingLicense> DrivingLicenses { get; set; }
}