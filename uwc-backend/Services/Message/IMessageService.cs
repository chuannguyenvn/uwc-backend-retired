namespace Services.Message;

public interface IMessageService
{
    public (bool success, object result) AddMessage(int sender, int receiver, DateTime textTime, string textContent);
    public List<Models.Message> GetAllMessagesOfTwoUsers(int thisUserId, int otherUserId);
    public List<Models.Message> GetLatestMessagesOf(int userId);
}