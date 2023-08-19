using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class UpdateDrivingLicenseRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Owner")] public int OwnerDriverId { get; set; }
        [JsonProperty("IssueDate")] public DateTime IssueDate { get; set; }
        [JsonProperty("IssuePlace")] public string IssuePlace { get; set; }
        [JsonProperty("Type")] public string Type { get; set; }
    }
}