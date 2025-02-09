using ForoWebApp.Database.Constants;
using ForoWebApp.Database.Documents;
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
    public async Task<IActionResult> Index(string id, [FromQuery] int? pageNumber)
    {
        ThreadViewModel threadViewModel = await _threadService.GetThreadPosts(id, pageNumber);
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
            _logger.LogError("No se pudo obtener el id del usuario logado");
            return Unauthorized();
        }

        ForumThread newThread = new()
        {
            ThemeId = themeId,
            Title = threadViewModel.Title,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Constants.CentralEuropeanTimezoneID)),
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
            _logger.LogError("Se capturo una excepción al crear el hilo: {exceptionMessage}", ex.Message);
            throw;
        }

        Post newPost = new()
        {
            ThreadId = threadId,
            UserId = userId,
            Content = threadViewModel.MessageContent,
            PostDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Constants.CentralEuropeanTimezoneID))
        };

        string postId;
        try
        {
            postId = await _postService.PublishPost(newPost);
        }
        catch (Exception ex)
        {
            _logger.LogError("Se capturo una excepción al publicar el primer mensaje: {exceptionMessage}", ex.Message);
            throw;
        }

        return Redirect($"/thread/{threadId}");
    }

    [Authorize]
    [HttpPost("close/{threadId}")]
    public async Task<IActionResult> CloseThread(string threadId)
    {
        bool success;
        try
        {
            success = await _threadService.CloseThread(threadId);
        }
        catch (Exception ex)
        {
            _logger.LogError("Se capturo una excepción al cerrar el hilo: {exceptionMessage}", ex.Message);
            throw;
        }

        if (success)
        {
            return StatusCode(205, "El hilo se ha cerrado correctamente");
        }
        return StatusCode(205, "Ocurrio un error al intentar cerrar el hilo");
    }
}
