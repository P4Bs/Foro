using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Models.Requests;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThreadController(ILogger<ThreadController> logger, ThreadService threadService, PostService postService) : Controller
{
    private readonly ILogger<ThreadController> _logger = logger;
    private readonly ThreadService _threadService = threadService;
    private readonly PostService _postService = postService;

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Index(string id)
    {
        ThreadViewModel threadViewModel = await _threadService.GetThreadPosts(id);
        return View(threadViewModel);
    }

    [Authorize]
    [HttpGet]
    public IActionResult NewThread(string themeId)
    {
        ViewData["ThemeId"] = themeId;
        return View();
    }

    [Authorize]
    [HttpPost("create/{themeId}")]
    public async Task<IActionResult> CreateNewThread(string themeId, [FromForm] NewThreadViewModel threadViewModel)
    {
        if(!ModelState.IsValid || themeId is null)
        {
            return View("NewThread", threadViewModel);
        }

        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            _logger.LogError("No se pudo obtener el id del usuario");
            return Unauthorized();
        }

        ForumThread newThread = new()
        {
            ThemeId = themeId,
            Title = threadViewModel.Title,
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

        Post newPost = new()
        {
            ThreadId = threadId,
            UserId = userId,
            Content = threadViewModel.MessageContent,
            PostDate = DateTime.UtcNow
        };

        string postId;
        try
        {
            postId = await _postService.PublishPost(newPost);
        }
        catch (Exception ex)
        {
            _logger.LogError("{exceptionMessage}", ex.Message);
            throw;
        }

        return Redirect($"/thread/{threadId}");
    }
}
