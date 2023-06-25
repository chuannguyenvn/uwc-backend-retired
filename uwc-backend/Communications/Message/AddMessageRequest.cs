using Newtonsoft.Json;

namespace uwc_backend.Communications.Message;

public class AddMessageRequest
{
    [JsonProperty("Sender")] public int Sender { get; set; }
    [JsonProperty("Receiver")] public int Receiver { get; set; }
    [JsonProperty("TextTime")] public DateTime TextTime { get; set; }
    [JsonProperty("TextContent")] public string TextContent { get; set; }
}