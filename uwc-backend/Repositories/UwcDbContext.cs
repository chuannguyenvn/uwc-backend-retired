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

    public DbSet<Mcp> Mcps { get; set; }
    public DbSet<DriverProfile> DriverProfiles { get; set; }
    public DbSet<CleanerProfile> CleanerProfiles { get; set; }
    public DbSet<SupervisorProfile> SupervisorProfiles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<TaskIncludeMcp> TaskIncludeMcps { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<DrivingHistory> DrivingHistories { get; set; }
    public DbSet<DrivingLicense> DrivingLicenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasOne(message => message.SenderAccount).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Message>().HasOne(message => message.ReceiverAccount).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}