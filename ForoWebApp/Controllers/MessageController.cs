using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class MessageController(ILogger<MessageController> logger, MessageService messageService) : Controller
{
	private readonly ILogger<MessageController> _logger = logger;
	private readonly MessageService _messageService = messageService;

	[HttpGet]
	public async Task<IActionResult> GetMessages([FromQuery] string threadId)
	{
		var messagesList = await _messageService.GetThreadMessages(threadId);
		return View(messagesList);
	}

	[HttpPost]
	[Authorize(Policy = "RegisteredUser")]
	public async Task<IActionResult> PublishMessageInThread([FromBody] CreateMessageData messageData)
	{
		Message newMessage = new()
		{
			ThreadId = messageData.ThreadId,
			UserId = messageData.UserId,
			Content = messageData.MessageContent,
			PublishingDate = DateTime.UtcNow
		};

		string messageId;

		try
		{
			messageId = await _messageService.PublishMessage(newMessage);
		}
		catch (Exception)
		{
			throw;
		}

		return View(messageId);
	}
}
