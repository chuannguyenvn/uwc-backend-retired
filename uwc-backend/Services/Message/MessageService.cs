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
            Sender = senderEmployee, Receiver = receiverEmployee, TextTime = textTime, TextContent = textContent
        };

        _unitOfWork.Messages.Add(messageInformation);
        _unitOfWork.Complete();
        return (true, "Add message successfully.");
    }

    public List<Models.Message> GetAllMessagesOfTwoUsers(int senderId, int receiverId)
    {
        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(senderId)) return new List<Models.Message>();

        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(receiverId)) return new List<Models.Message>();

        var messageList = _unitOfWork.Messages.Find(message =>
            (message.Sender.Id == senderId && message.Receiver.Id == receiverId) ||
            (message.Sender.Id == receiverId && message.Receiver.Id == senderId));

        var result = messageList.OrderBy(message => message.TextTime).ToList();
        return result;
    }

}