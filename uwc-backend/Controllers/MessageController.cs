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

    [HttpGet("inbox/{senderId}/{receiverId}")]
    public List<Message> GetAllMessagesWith([FromRoute] int senderId, [FromRoute] int receiverId)
    {
        var result = _messageService.GetAllMessagesOfTwoUsers(senderId, receiverId);
        return result;
    }

    [HttpGet("inbox/latest/{senderId}")]
    public Dictionary<int, Message> GetLatestMessages([FromRoute] int senderId)
    {
        var result = _messageService.GetLatestMessagesOf(senderId);
        return result;
    }
}