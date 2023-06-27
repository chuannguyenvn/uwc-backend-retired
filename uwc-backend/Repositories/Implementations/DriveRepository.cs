namespace Repositories.Implementations;

public class DriveRepository : GenericRepository<Models.Drive>
{
    public DriveRepository(UwcDbContext context) : base(context)
    {
    }
}