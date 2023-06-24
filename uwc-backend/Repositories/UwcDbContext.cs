using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UwcDbContext : DbContext
{
    public UwcDbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
}