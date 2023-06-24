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
}