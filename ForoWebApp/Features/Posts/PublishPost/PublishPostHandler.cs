using ForoWebApp.Database.Constants;
using ForoWebApp.Database.Documents;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Posts.PublishPost;

public class PublishPostHandler(ILogger<PublishPostHandler> logger, PostService postService) : IRequestHandler<PublishPostRequest, PublishPostResponse>
{
    private readonly ILogger<PublishPostHandler> _logger = logger;
    private readonly PostService _postService = postService;

    public async Task<PublishPostResponse> Handle(PublishPostRequest request, CancellationToken cancellationToken)
    {
        Post newPost = new()
        {
            ThreadId = request.ThreadId,
            UserId = request.UserId,
            Content = request.PostContent,
            PostDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Constants.CentralEuropeanTimezoneID))
        };

        string postId;
        try
        {
            postId = await _postService.PublishPost(newPost);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al publicar el post en el hilo con id = {threadId}: {exceptionMessage}", request.ThreadId, ex.Message);
            return new PublishPostResponse
            {
                Success = false,
                Message = $"Hubo un error al publicar tu post en el hilo {request.ThreadId}. Por favor, intentelo de nuevo más tarde"
            };
        }

        _logger.LogInformation("Post publicado en hilo {threadId} con identificador {postId}", request.ThreadId, postId);
        return new PublishPostResponse
        {
            Success = true,
            Message = "El post se ha publicado correctamente",
            ThreadId = request.ThreadId
        };
    }
}
