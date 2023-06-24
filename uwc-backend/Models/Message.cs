namespace Models;

public class Message
{
    public int Id { get; set; }
    public Employee Sender { get; set; }
    public Employee Receiver { get; set; }
    public DateTime TextTime { get; set; }
    public string TextContent { get; set; }
}