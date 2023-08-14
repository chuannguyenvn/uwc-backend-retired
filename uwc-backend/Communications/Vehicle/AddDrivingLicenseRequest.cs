using Newtonsoft.Json;

namespace Communications.Vehicle
{
    public class AddDrivingLicenseRequest
    {
        [JsonProperty("Owner")] public int OwnerDriverId { get; set; }
        [JsonProperty("IssueDate")] public DateTime IssueDate { get; set; }
        [JsonProperty("IssuePlace")] public string IssuePlace { get; set; }
        [JsonProperty("Type")] public string Type { get; set; }
    }
}