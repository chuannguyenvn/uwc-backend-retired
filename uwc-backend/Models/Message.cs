using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Message : IndexedEntity
{
    public Account Sender { get; set; }
    public Account Receiver { get; set; }
    public DateTime TextTime { get; set; }
    public string TextContent { get; set; }
}