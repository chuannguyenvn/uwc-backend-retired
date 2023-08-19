using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Message;
using Utilities;
using Commons.Communications.Message;

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

    [HttpGet("inbox/{thisUser}/{otherUser}")]
    public List<Message> GetAllMessagesWith([FromRoute] int thisUser, [FromRoute] int otherUser)
    {
        var result = _messageService.GetAllMessagesOfTwoUsers(thisUser, otherUser);
        return result;
    }

    [HttpGet("inbox/latest/{senderId}")]
    public List<Message> GetLatestMessages([FromRoute] int senderId)
    {
        var result = _messageService.GetLatestMessagesOf(senderId);
        return result;
    }
}