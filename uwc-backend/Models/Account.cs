namespace Models
{
    public class Account : IndexedEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public UserProfile LinkedProfile { get; set; }
        public string Settings { get; set; }
    }
}