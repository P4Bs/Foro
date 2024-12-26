using ForoWebApp.Database.Repositories.Messages;
using ForoWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers.Message
{
	[Route("[controller]")]
	public class MessageController(ILogger<MessageController> logger, IMessagesRepository messagesRepository) : Controller
	{
		private readonly ILogger<MessageController> _logger = logger;
		private readonly IMessagesRepository _messagesRepository = messagesRepository;

		[HttpGet]
		public async Task<IActionResult> GetMessagesByThreadId([FromQuery] int threadId)
		{
			var messagesCursor = await _messagesRepository.FindAllByThreadIdAsync(threadId);
			return View(messagesCursor);
		}

		[HttpPost]
		public async Task<IActionResult> PublishMessageInThread([FromBody] CreateMessageData messageData)
		{
			Database.Documents.Message newMessage = new()
			{
				ThreadId = messageData.ThreadId,
				UserId = messageData.UserId,
				MessageContent = messageData.MessageContent,
				PublishingDate = DateTime.UtcNow
			};

			int messageId;

			try
			{
				messageId = await _messagesRepository.InsertOneAsync(newMessage);
			}
			catch (Exception)
			{
				throw;
			}

			return View(messageId);
		}
	}
}
