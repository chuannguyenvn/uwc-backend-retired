using System.Runtime.InteropServices.JavaScript;
using Models;
using Newtonsoft.Json;

namespace uwc_backend.Communications
{
    public class AddEmployeeRequest
    {
        [JsonProperty("FirstName")] public string FirstName { get; set; }
        [JsonProperty("LastName")] public string LastName { get; set; }
        [JsonProperty("Gender")] public int Gender { get; set; }
        [JsonProperty("DateOfBirth")] public DateTime DateOfBirth { get; set; }
        [JsonProperty("Role")] public int Role { get; set; }
    }
}