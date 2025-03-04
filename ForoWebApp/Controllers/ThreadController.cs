using ForoWebApp.Controllers.Base;
using ForoWebApp.Features.Threads.CloseThread;
using ForoWebApp.Features.Threads.CreateThread;
using ForoWebApp.Features.Threads.GetThread;
using ForoWebApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThreadController(IMediator mediator, ILogger<ThreadController> logger) : BaseController
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<ThreadController> _logger = logger;

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Index([FromRoute] string id, int? page)
    {
        var request = new GetThreadRequest
        {
            ThreadId = id,
            Page = page
        };

        var response = await _mediator.Send(request);

        if (response.Success)
        {
            return View(response.ThreadViewModel);
        }

        return View("Error", new ErrorViewModel(GetRequestId(), response.Errors));
    }

    [Authorize]
    [HttpGet]
    public IActionResult NewThread(string themeId)
    {
        ViewData["ThemeId"] = themeId;
        return View();
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CloseThread([FromQuery] string threadId)
    {
        if (threadId == null)
        {
            _logger.LogError("No se pudo obtener el id del hilo");
            return View("Error", new ErrorViewModel(GetRequestId(), ["No se pudo obtener el id del hilo"]));
        }
            var request = new CloseThreadRequest
        {
            ThreadId = threadId
        };

        var response = await _mediator.Send(request);

        if (response.Success)
        {
            return RedirectToAction("Index", "Thread", new { id = threadId });
        }

        return View("Error", new ErrorViewModel(GetRequestId(), response.Errors));
    }

    [Authorize]
    [HttpPost("create/{themeId}")]
    public async Task<IActionResult> CreateNewThread(string themeId, [FromForm] NewThreadViewModel threadViewModel)
    {
        if(!ModelState.IsValid || themeId is null)
        {
            _logger.LogError("No se pudo obtener el id del usuario logado");
            return View("NewThread", threadViewModel);
        }

        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            _logger.LogError("No se pudo obtener el id del usuario logado");
            return Unauthorized();
        }

        var request = new CreateThreadRequest
        {
            ThemeId = themeId,
            UserId = userId,
            Title = threadViewModel.Title,
            PostContent = threadViewModel.MessageContent
        };

        var response = await _mediator.Send(request);

        if(response.Success)
        {
            return Redirect($"/thread/{response.ThreadId}");
        }

        return View("Error", new ErrorViewModel(GetRequestId(), response.Errors));
    }
}

