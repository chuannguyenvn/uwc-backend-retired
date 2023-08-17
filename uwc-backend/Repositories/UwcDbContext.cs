using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class UwcDbContext : DbContext
{
    public UwcDbContext(DbContextOptions<UwcDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<SupervisorProfile> SupervisorProfiles { get; set; }
    public DbSet<DriverProfile> DriverProfiles { get; set; }
    public DbSet<CleanerProfile> CleanerProfiles { get; set; }

    public DbSet<DrivingHistory> DrivingHistories { get; set; }
    public DbSet<DrivingLicense> DrivingLicenses { get; set; }

    public DbSet<Mcp> Mcps { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<TaskEntry> Tasks { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Message>().HasOne(message => message.SenderAccount).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Message>().HasOne(message => message.ReceiverAccount).WithMany().OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<TaskEntry>().HasOne(task => task.Supervisor).WithMany().OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<TaskEntry>().HasOne(task => task.Worker).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}