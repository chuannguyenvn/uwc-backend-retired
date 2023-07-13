namespace Services.Message;

public interface IMessageService
{
    public (bool success, object result) AddMessage(int sender, int receiver, DateTime textTime, string textContent);
    public List<Models.Message> GetAllMessagesOfTwoUsers(int senderId, int receiverId);
}