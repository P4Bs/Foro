using ForoWebApp.Controllers.Base;
using ForoWebApp.Features.Posts.PublishPost;
using ForoWebApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class PostController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [Authorize]
    public IActionResult NewPost(string threadId)
    {
        ViewData["ThreadId"] = threadId;
        return View();
    }

    [Authorize]
    [HttpPost("publish/{threadId}")]
    public async Task<IActionResult> PublishPostInThread(string threadId, [FromForm] CreatePostViewModel request)
    {
        var publishPostRequest = new PublishPostRequest
        {
            ThreadId = threadId,
            UserId = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value,
            PostContent = request.MessageContent
        };

        var response = await _mediator.Send(publishPostRequest);

        if (response.Success)
        {
            return Redirect($"/thread/{threadId}");
        }
        return View(new ErrorViewModel(GetRequestId(), response.Errors));
    }
}

