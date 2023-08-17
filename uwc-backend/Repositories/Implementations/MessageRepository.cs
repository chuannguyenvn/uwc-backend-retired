using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories.Implementations;

public class MessageRepository : GenericRepository<Message>
{
    public MessageRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Message> GetMessageWithId(int id)
    {
        return _context.Messages.Where(employee => employee.Id == id);
    }

    public IEnumerable<Message> GetMessagesOfUser(int userId)
    {
        if (!_context.Accounts.Any(account => account.Id == userId)) return new List<Message>();

        var newestMessages = _context.Messages.Include(message => message.SenderAccount)
            .ThenInclude(account => account.LinkedProfile)
            .Include(message => message.ReceiverAccount)
            .ThenInclude(account => account.LinkedProfile)
            .Where(message => message.SenderAccount.Id == userId || message.ReceiverAccount.Id == userId)
            .ToList()
            .GroupBy(message => message.SenderAccount.Id > message.ReceiverAccount.Id
                ? new {account1 = message.SenderAccount, account2 = message.ReceiverAccount}
                : new {account1 = message.ReceiverAccount, account2 = message.SenderAccount})
            .Distinct()
            .Select(group => group.OrderByDescending(message => message.TextTime).First())
            .OrderByDescending(message => message.TextTime);

        return newestMessages;
    }
}