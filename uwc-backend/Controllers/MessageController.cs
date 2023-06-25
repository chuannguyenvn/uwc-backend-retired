using Microsoft.AspNetCore.Mvc;
using Services.Message;
using uwc_backend.Communications.Message;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("add-message")]
    public IActionResult AddMessage(AddMessageRequest addMessageRequest)
    {
        var (success, result) = _messageService.AddMessage(addMessageRequest.Sender, addMessageRequest.Receiver,
            addMessageRequest.TextTime, addMessageRequest.TextContent);
        
        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}