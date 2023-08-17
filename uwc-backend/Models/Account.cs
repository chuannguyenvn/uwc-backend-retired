namespace Models
{
    public class Account : IndexedEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public UserProfile LinkedProfile { get; set; }
        public string Settings { get; set; }

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}