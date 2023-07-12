namespace Models;

public class Account : IndexedEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public Employee Employee { get; set; }
    public string Settings { get; set; }
    public string Salt { get; set; }
}