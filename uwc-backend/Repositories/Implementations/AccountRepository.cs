using System.Diagnostics.CodeAnalysis;
using Models;

namespace Repositories.Implementations;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(UwcDbContext context) : base(context)
    {
    }

    public bool DoesUsernameExist(string username)
    {
        return _context.Accounts.Any(account => account.Username == username);
    }
}