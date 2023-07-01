using Newtonsoft.Json;

namespace Communications.Authentication
{
    public class AuthenticationRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Password")] public string Password { get; set; }
        [JsonProperty("Employee")] public int Employee { get; set; }
        [JsonProperty("Settings")] public string Settings { get; set; }
    }
}