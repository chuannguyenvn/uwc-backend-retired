using Newtonsoft.Json;

namespace Communications.Authentication
{
    public class LoginRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Password")] public string Password { get; set; }
    }
}