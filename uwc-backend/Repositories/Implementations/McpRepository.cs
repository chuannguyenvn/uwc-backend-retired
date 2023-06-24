using Models;

namespace Repositories.Implementations;

public class McpRepository : GenericRepository<Mcp>
{
    public McpRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Mcp> GetMcpsWithMinimumLoad(int minimumLoad)
    {
        return _context.Mcps.Where(mcp => mcp.CurrentLoad >= minimumLoad);
    }
}