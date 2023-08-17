namespace Models
{
    public class Message : IndexedEntity
    {
        public Account SenderAccount { get; set; }
        public Account ReceiverAccount { get; set; }
        public DateTime TextTime { get; set; }
        public string TextContent { get; set; }
    }
}