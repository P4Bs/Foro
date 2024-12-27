using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThreadsController(ILogger<ThreadsController> logger, ThreadService threadService) : Controller
{
	private readonly ILogger<ThreadsController> _logger = logger;
	private readonly ThreadService _threadService = threadService;

	[HttpGet]
	public async Task<IActionResult> GetThreadsByThemeId([FromQuery] string themeId)
	{
		var threadsCursor = await _threadService.GetThreadsByThemeId(themeId);
		return View(threadsCursor);
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
