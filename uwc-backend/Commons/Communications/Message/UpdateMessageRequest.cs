using Newtonsoft.Json;

namespace Commons.Communications.Message
{
    public class UpdateMessageRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("TextContent")] public string TextContent { get; set; }
    }
}