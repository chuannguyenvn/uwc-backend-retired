using Repositories;

namespace Services.Message;

public class MessageService : IMessageService
{
    private readonly UnitOfWork _unitOfWork;

    public MessageService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddMessage(int sender, int receiver, DateTime textTime, string textContent)
    {
        if (!_unitOfWork.Accounts.DoesIdExist(sender)) return (false, "Sender id does not exist.");

        if (!_unitOfWork.Accounts.DoesIdExist(receiver))
            return (false, "Receiver id does not exist.");

        var senderEmployee = _unitOfWork.Accounts.Find(employee => employee.Id == sender).First();
        var receiverEmployee = _unitOfWork.Accounts.Find(employee => employee.Id == receiver).First();
        var messageInformation = new Models.Message
        {
            SenderAccount = senderEmployee, ReceiverAccount = receiverEmployee, TextTime = textTime, TextContent = textContent
        };

        _unitOfWork.Messages.Add(messageInformation);
        _unitOfWork.Complete();
        return (true, "Add message successfully.");
    }

    public List<Models.Message> GetAllMessagesOfTwoUsers(int senderId, int receiverId)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(senderId)) return new List<Models.Message>();
        if (!_unitOfWork.DriverProfiles.DoesIdExist(receiverId)) return new List<Models.Message>();

        var messageList = _unitOfWork.Messages.Find(message =>
            (message.SenderAccount.Id == senderId && message.ReceiverAccount.Id == receiverId) ||
            (message.SenderAccount.Id == receiverId && message.ReceiverAccount.Id == senderId));

        var result = messageList.OrderBy(message => message.TextTime).ToList();
        return result;
    }

    public Dictionary<int, Models.Message> GetLatestMessagesOf(int userId)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(userId)) return new Dictionary<int, Models.Message>();

        var result = new Dictionary<int, Models.Message>();
        foreach (var account in _unitOfWork.Accounts.GetAll().ToList())
        {
            if (!_unitOfWork.Messages.GetAll()
                    .Any(message => message.SenderAccount.Id == userId || message.ReceiverAccount.Id == userId)) continue;

            result.Add(account.Id,
                _unitOfWork.Messages.GetAll()
                    .Where(message => message.SenderAccount.Id == userId || message.ReceiverAccount.Id == userId)
                    .OrderByDescending(message => message.TextTime)
                    .First());
        }

        return result;
    }
}