﻿using Repositories;

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

    public (bool success, object result) UpdateMessageContent(int id, string textContent)
    {
        if (!_unitOfWork.Messages.DoesIdExist(id)) return (false, "Message Id does not exist.");

        var message = _unitOfWork.Messages.Find(message => message.Id == id).First();
        message.TextContent = textContent;
        _unitOfWork.Complete();

        return (true, "Message content updated successfully");
    }

    public List<Models.Message> GetMessagesIn24Hour()
    {
        var message = _unitOfWork.Messages.Find(text => text.TextTime >= DateTime.Today.AddDays(-1) && text.TextTime <= DateTime.Now)
            .ToList();

        return message;
    }

    public List<Models.Message> GetAllMessages()
    {
        var messageList = _unitOfWork.Messages.GetAll();
        return messageList.ToList();
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

    public List<Models.Message> GetMessagesContainWord(int senderId, int receiverId, string word)
    {
        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(senderId)) return new List<Models.Message>();

        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(receiverId)) return new List<Models.Message>();

        var messageList = _unitOfWork.Messages.Find(message =>
            (message.Sender.Id == senderId && message.Receiver.Id == receiverId) ||
            (message.Sender.Id == receiverId && message.Receiver.Id == senderId));

        return messageList.ToList().OrderBy(message => message.TextTime).ToList();
    }
}