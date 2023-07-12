using Microsoft.EntityFrameworkCore;
using Models;
using Route = Models.Route;
using Task = Models.Task;

namespace Repositories;

public class UwcDbContext : DbContext
{
    public UwcDbContext(DbContextOptions<UwcDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasOne(message => message.Sender).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Message>().HasOne(message => message.Receiver).WithMany().OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Mcp> Mcps { get; set; }
    public DbSet<EmployeeProfile> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<TaskIncludeMcp> TaskIncludeMcps { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Drive> Drives { get; set; }
    public DbSet<DrivingLicense> DrivingLicenses { get; set; }
}