using ForoWebApp.Database.Repositories.Threads;
using ForoWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers.Thread
{
	[Route("[controller]")]
	public class ThreadsController(ILogger<ThreadsController> logger, IThreadsRepository threadsRepository) : Controller
	{
		private readonly ILogger<ThreadsController> _logger = logger;
		private readonly IThreadsRepository _threadsRepository = threadsRepository;

		[HttpGet]
		public async Task<IActionResult> GetThreadsByThemeId([FromQuery] int themeId)
		{
			var threadsCursor = await _threadsRepository.FindAllByThemeIdAsync(themeId);
			return View(threadsCursor);
		}

		[HttpPost]
		public async Task<IActionResult> CreateNewThread([FromBody] CreateThreadData threadData)
		{
			Models.Thread newThread = new()
			{
				ThemeId = threadData.ThemeId,
				Title = threadData.Title,
				MessageCount = 0,
				CreatedAt = DateTime.UtcNow,
				LastUpdateAt = DateTime.UtcNow,
				IsClosed = false,
				ClosureDate = null
			};

			int threadId;

			try
			{
				threadId = await _threadsRepository.InsertOneAsync(newThread);
			}
			catch (Exception)
			{
				throw;
			}

			return View(threadId);
		}
	}
}
