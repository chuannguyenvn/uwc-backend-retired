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

    public List<Models.Message> GetAllMessagesOfTwoUsers(int thisUserId, int otherUserId)
    {
        if (!_unitOfWork.Accounts.DoesIdExist(thisUserId)) return new List<Models.Message>();
        if (!_unitOfWork.Accounts.DoesIdExist(otherUserId)) return new List<Models.Message>();

        return _unitOfWork.Messages.GetMessagesOfTwoUsers(thisUserId, otherUserId).ToList();
    }

    public List<Models.Message> GetLatestMessagesOf(int userId)
    {
        if (!_unitOfWork.Accounts.DoesIdExist(userId)) return new ();

        return _unitOfWork.Messages.GetMessagesOfUser(userId).ToList();
    }
}