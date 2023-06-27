using Newtonsoft.Json;

namespace uwc_backend.Communications.Vehicle;

public class AddDrivingLicenseRequest
{
    [JsonProperty("IssueDate")] public DateTime IssueDate { get; set; }
    [JsonProperty("IssuePlace")] public string IssuePlace { get; set; }
    [JsonProperty("Owner")] public int Owner { get; set; }
    [JsonProperty("Type")] public string Type { get; set; }
}