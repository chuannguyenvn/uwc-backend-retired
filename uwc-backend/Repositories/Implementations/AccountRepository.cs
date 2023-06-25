using Models;

namespace Repositories.Implementations;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(UwcDbContext context) : base(context)
    {
    }

    public bool DoesAccountWithUsernameExist(string username)
    {
        return _context.Accounts.Any(account => account.Username == username);
    }

    public Account GetAccountWithId(int id)
    {
        return _context.Accounts.First(account => account.Id == id);
    }
}