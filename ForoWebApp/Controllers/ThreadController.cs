using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class ThreadController(ILogger<ThreadController> logger, ThreadService threadService) : Controller
{
	private readonly ILogger<ThreadController> _logger = logger;
	private readonly ThreadService _threadService = threadService;

	[HttpGet]
	public async Task<IActionResult> Thread(string themeId)
	{
		ThreadViewModel threadViewModel = await _threadService.GetThreadMessages(themeId);
		return View(threadViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> CreateNewThread([FromBody] CreateThreadData threadData)
	{
		ForumThread newThread = new()
		{
			ThemeId = threadData.ThemeId,
			Title = threadData.Title,
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

		return View(threadId);
	}
}
