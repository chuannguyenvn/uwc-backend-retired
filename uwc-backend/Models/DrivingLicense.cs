namespace Models
{
    public class DrivingLicense : IndexedEntity
    {
        public int DriverProfileId { get; set; }
        public DriverProfile DriverProfile { get; set; }

        public DateTime IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Type { get; set; }
    }
}