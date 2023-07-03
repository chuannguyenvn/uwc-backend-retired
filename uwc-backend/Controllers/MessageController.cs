using Communications.Message;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Message;

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
        var (success, result) = _messageService.AddMessage(addMessageRequest.Sender,
            addMessageRequest.Receiver,
            addMessageRequest.TextTime,
            addMessageRequest.TextContent);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("update-message-content")]
    public IActionResult UpdateMessageContent(UpdateMessageRequest updateMessageRequest)
    {
        var (success, result) = _messageService.UpdateMessageContent(updateMessageRequest.Id, updateMessageRequest.TextContent);

        if (!success) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("get-message-in-24-hour")]
    public List<Message> GetMessageIn24Hour()
    {
        var result = _messageService.GetMessagesIn24Hour();
        return result;
    }

    [HttpGet("get-all-messages")]
    public List<Message> GetAllMessages()
    {
        var result = _messageService.GetAllMessages();
        return result;
    }

    [HttpGet("get-all-messages-between-2-users")]
    public List<Message> GetAllMessagesBetween2Users(GetMessagesOfTwoUsersRequest getMessagesOfTwoUsersRequest)
    {
        var result = _messageService.GetAllMessagesOfTwoUsers(getMessagesOfTwoUsersRequest.Sender, getMessagesOfTwoUsersRequest.Receiver);
        return result;
    }

    [HttpGet("get-messages-2-users-contain-word")]
    public List<Message> GetMessages2UsersContainWord(GetMessagesContainWordRequest getMessagesContainWordRequest)
    {
        var result = _messageService.GetMessagesContainWord(getMessagesContainWordRequest.Sender,
            getMessagesContainWordRequest.Receiver,
            getMessagesContainWordRequest.Word);

        return result;
    }
}