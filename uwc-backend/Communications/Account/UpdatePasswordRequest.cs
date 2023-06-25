using Newtonsoft.Json;

namespace uwc_backend.Communications;

public class UpdatePasswordRequest
{
    [JsonProperty("Username")] public string Username { get; set; }
    [JsonProperty("OldPassword")] public string OldPassword { get; set; }
    [JsonProperty("NewPassword")] public string NewPassword { get; set; }
}