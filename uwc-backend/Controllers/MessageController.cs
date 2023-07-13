using Communications.Message;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Message;
using Utilities;

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

    [HttpPost("add")]
    public IActionResult AddMessage(AddMessageRequest addMessageRequest)
    {
        var (success, result) = _messageService.AddMessage(addMessageRequest.Sender,
            addMessageRequest.Receiver,
            addMessageRequest.TextTime,
            addMessageRequest.TextContent);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("inbox/{userId}")]
    public List<Message> GetAllMessagesWith([FromRoute] int userId)
    {
        var result = _messageService.GetAllMessagesOfTwoUsers(User.GetLoggedInUserId(), userId);
        return result;
    }
}