using System.Runtime.InteropServices.JavaScript;
using Models;
using Newtonsoft.Json;

namespace uwc_backend.Communications
{
    public class AuthenticationRequest
    {
        [JsonProperty("Username")] public string Username { get; set; }
        [JsonProperty("Password")] public string Password { get; set; }
        [JsonProperty("Employee")] public int Employee { get; set; }
    }
}