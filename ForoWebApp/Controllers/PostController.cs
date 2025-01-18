using ForoWebApp.Database.Documents;
using ForoWebApp.Models.Requests;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class PostController(ILogger<PostController> logger, PostService postService) : Controller
{
    private readonly ILogger<PostController> _logger = logger;
    private readonly PostService _postService = postService;

    [Authorize]
    public IActionResult NewPost(string threadId)
    {
        return View(model: new NewPostViewModel(threadId));
    }

    [Authorize]
    [HttpPost("publish")]
    public async Task<IActionResult> PublishPostInThread([FromBody] CreatePostRequest request)
    {
        Post newPost = new()
        {
            ThreadId = request.ThreadId,
            UserId = request.UserId,
            Content = request.MessageContent,
            PostDate = DateTime.UtcNow
        };

        string postId;

        try
        {
            postId = await _postService.PublishPost(newPost);
        }
        catch (Exception)
        {
            throw;
        }

        return View(postId);
    }
}
