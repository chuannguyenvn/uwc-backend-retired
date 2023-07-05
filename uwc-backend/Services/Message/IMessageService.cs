namespace Services.Message;

public interface IMessageService
{
    public (bool success, object result) AddMessage(int sender, int receiver, DateTime textTime, string textContent);

    public (bool success, object result) UpdateMessageContent(int id, string textContent);
    public List<Models.Message> GetMessagesIn24Hour();
    public List<Models.Message> GetAllMessages();
    public List<Models.Message> GetAllMessagesOfTwoUsers(int senderId, int receiverId);
    public List<Models.Message> GetMessagesContainWord(int senderId, int receiverId, string word);
}