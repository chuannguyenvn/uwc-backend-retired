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

    [HttpPut("update-message-content")]
    public IActionResult UpdateMessageContent(UpdateMessageRequest updateMessageRequest)
    {
        var (success, result) =
            _messageService.UpdateMessageContent(updateMessageRequest.Id, updateMessageRequest.TextContent);

        if (!success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("get-message-in-24-hour")]
    public List<Models.Message> GetMessageIn24Hour()
    {
        var result = _messageService.GetMessagesIn24Hour();
        return result;
    }

    [HttpGet("get-all-messages")]
    public List<Models.Message> GetAllMessages()
    {
        var result = _messageService.GetAllMessages();
        return result;
    }

    [HttpGet("get-all-messages-between-2-users")]
    public List<Models.Message> GetAllMessagesBetween2Users(GetMessagesOfTwoUsersRequest getMessagesOfTwoUsersRequest)
    {
        var result = _messageService.GetAllMessagesOfTwoUsers(getMessagesOfTwoUsersRequest.Sender,
            getMessagesOfTwoUsersRequest.Receiver);
        return result;
    }
}