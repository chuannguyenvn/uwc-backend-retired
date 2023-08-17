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

        var newestMessages = _context.Messages.Where(message => message.SenderAccount.Id == userId || message.ReceiverAccount.Id == userId)
            .ToList()
            .GroupBy(message => new {message.SenderAccount, message.ReceiverAccount})
            .Select(group => group.OrderByDescending(message => message.TextTime).First());

        return newestMessages;
    }
}