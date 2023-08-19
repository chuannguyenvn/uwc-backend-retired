using Newtonsoft.Json;

namespace Commons.Communications.Account
{
    public class UpdateSettingsRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Settings")] public string Settings { get; set; }
    }
}