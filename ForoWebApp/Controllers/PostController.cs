using ForoWebApp.Database.Documents;
using ForoWebApp.Models.Requests;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class PostController(ILogger<PostController> logger, PostService postService) : Controller
{
    private readonly ILogger<PostController> _logger = logger;
    private readonly PostService _postService = postService;

    [Authorize]
    public IActionResult NewPost(string threadId)
    {
        ViewData["ThreadId"] = threadId;
        return View();
    }

    [Authorize]
    [HttpPost("publish/{threadId}")]
    public async Task<IActionResult> PublishPostInThread(string threadId, [FromBody] CreatePostViewModel request)
    {
        string userId = User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).First().Value;
        Post newPost = new()
        {
            ThreadId = threadId,
            UserId = userId,
            Content = request.MessageContent,
            PostDate = DateTime.UtcNow
        };

        string postId;

        try
        {
            postId = await _postService.PublishPost(newPost);
            newPost.Id = postId;
        }
        catch (Exception)
        {
            throw;
        }

        return Redirect($"/thread/{threadId}");
    }
}
