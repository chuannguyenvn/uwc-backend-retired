using Newtonsoft.Json;

namespace uwc_backend.Communications;

public class UpdateRoleEmployeeRequest
{
    [JsonProperty("Employee")] public int Employee { get; set; }
    [JsonProperty("Role")] public int Role { get; set; }
}