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

    public bool TryGetAccount(string username,
        [NotNullWhen(returnValue: true)] out Account? account, out string error)
    {
        account = null;
        error = "Success.";

        if (!_context.Accounts.Any())
        {
            error = "The database is empty.";
            return false;
        }

        var possibleAccounts = _context.Accounts.Where(account => account.Username == username);
        if (!possibleAccounts.Any())
        {
            error = "Account does not exist.";
            return false;
        }
        if (possibleAccounts.Count() > 1)
        {
            error = "There are multiple accounts with the same username";
            return false;
        }

        account = possibleAccounts.First();
        return true;
    }
}