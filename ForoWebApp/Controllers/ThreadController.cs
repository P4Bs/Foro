using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class ThreadController(ILogger<ThreadController> logger, ThreadService threadService, MessageService messageService) : Controller
{
	private readonly ILogger<ThreadController> _logger = logger;
	private readonly ThreadService _threadService = threadService;
	private readonly MessageService _messageService = messageService;

	[HttpGet]
	public async Task<IActionResult> Thread(string themeId)
	{
		ThreadViewModel threadViewModel = await _threadService.GetThreadMessages(themeId);
		return View(threadViewModel);
	}

    public IActionResult NewThread(string themeId)
    {
        return View(model: new NewThreadData(themeId));
    }

    public IActionResult CreateThread()
    {
        return View();
    }

    [HttpPost]
	public async Task<IActionResult> CreateNewThread([FromBody] CreateThreadData newThreadData)
	{
		ForumThread newThread = new()
		{
			ThemeId = newThreadData.ThemeId,
			Title = newThreadData.Title,
			CreatedAt = DateTime.UtcNow,
			IsClosed = false,
			ClosureDate = null
		};

		string threadId;

		try
		{
			threadId = await _threadService.PublishThread(newThread);
		}
		catch (Exception ex)
		{
			_logger.LogError("{exceptionMessage}", ex.Message);
			throw;
		}

		Message newMessage = new()
		{
			ThreadId = threadId,
			UserId = HttpContext.Session.GetString("UserId"),
			Content = newThreadData.MessageContent,
			PublishingDate = DateTime.UtcNow
		};

		string messageId;
		try
		{
			messageId = await _messageService.PublishMessage(newMessage);
        }
		catch(Exception ex)
		{
			_logger.LogError("{exceptionMessage}", ex.Message);
		}

		return View(threadId);
	}
}
