using Newtonsoft.Json;

namespace Communications.Authentication
{
    public class UpdatePasswordRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("OldPassword")] public string OldPassword { get; set; }
        [JsonProperty("NewPassword")] public string NewPassword { get; set; }
    }
}