using Newtonsoft.Json;

namespace Commons.Communications.Account
{
    public class LoginRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Password")] public string Password { get; set; }
    }
}