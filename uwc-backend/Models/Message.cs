namespace Models;

public class Message : IndexedEntity
{
    public int SenderAccountId { get; set; }
    public Account SenderAccount { get; set; }

    public int ReceiverAccountId { get; set; }
    public Account ReceiverAccount { get; set; }

    public DateTime TextTime { get; set; }
    public string TextContent { get; set; }
}