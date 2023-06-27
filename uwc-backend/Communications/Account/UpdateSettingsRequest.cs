using Newtonsoft.Json;

namespace uwc_backend.Communications;

public class UpdateSettingsRequest
{
    [JsonProperty("Username")] public string Username { get; set; }
    [JsonProperty("Password")] public string Password { get; set; }
    [JsonProperty("Settings")] public string Settings { get; set; }
}