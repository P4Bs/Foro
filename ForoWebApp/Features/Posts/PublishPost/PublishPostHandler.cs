using ForoWebApp.Models.Domain;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Posts.PublishPost;

public class PublishPostHandler(ILogger<PublishPostHandler> logger, PostService postService) : IRequestHandler<PublishPostRequest, PublishPostResponse>
{
    private readonly ILogger<PublishPostHandler> _logger = logger;
    private readonly PostService _postService = postService;

    public async Task<PublishPostResponse> Handle(PublishPostRequest request, CancellationToken cancellationToken)
    {
        Post newPostModel = new()
        {
            ThreadId = request.ThreadId,
            UserId = request.UserId,
            Content = request.PostContent
        };

        string postId;
        try
        {
            postId = await _postService.CreatePost(newPostModel);
        }
        catch (Exception ex)
        {
            string errorMessage =
                $"Error al publicar el post creado por el usuario {request.UserId} con contenido\n\"{request.PostContent}\"\nEn el hilo con id = {request.ThreadId}";
            _logger.LogError(ex, "{message}", errorMessage);
            return new PublishPostResponse(success: false,
                errors: [ex.Message, $"Hubo un error al publicar tu post en el hilo con id = \"{request.ThreadId}\". Por favor, intentalo de nuevo más tarde."]);
        }

        _logger.LogInformation("Post publicado por el usuario con id = {userId} en hilo con id = {threadId}.\n\tID del Post = {postId}.\n\tFecha de publicación: {postDate}", request.UserId, request.ThreadId, postId, newPostModel.PostDate);
        return new PublishPostResponse(success: true)
        {
            ThreadId = request.ThreadId
        };
    }
}
