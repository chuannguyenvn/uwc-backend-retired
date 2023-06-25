using Newtonsoft.Json;

namespace uwc_backend.Communications.Message;

public class UpdateMessageRequest
{
    [JsonProperty("Id")] public int Id { get; set; }
    [JsonProperty("TextContent")] public string TextContent { get; set; }
}