using Newtonsoft.Json;

namespace Communications.Authentication
{
    public class UpdateSettingsRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Settings")] public string Settings { get; set; }
    }
}