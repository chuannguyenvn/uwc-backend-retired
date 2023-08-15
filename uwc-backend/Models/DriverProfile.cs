namespace Models
{
    public class DriverProfile : UserProfile
    {
        public List<DrivingLicense> DrivingLicenses { get; set; }
        public List<DrivingHistory> DrivingHistories { get; set; }
    }
}