using Commons.Types;
using Newtonsoft.Json;

namespace Commons.Communications.Employee
{
    public class UpdateUserProfile
    {
        [JsonProperty("UserProfileID")] public int UserProfileId { get; set; }
        [JsonProperty("FirstName")] public string FirstName { get; set; }
        [JsonProperty("LastName")] public string LastName { get; set; }
        [JsonProperty("Gender")] public Gender Gender { get; set; }
        [JsonProperty("DateOfBirth")] public DateTime DateOfBirth { get; set; }
        [JsonProperty("Role")] public UserRole Role { get; set; }
    }
}