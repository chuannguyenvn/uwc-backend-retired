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
}