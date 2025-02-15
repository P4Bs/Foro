using ForoWebApp.Models.Domain;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Threads.CreateThread;

public class CreateThreadHandler(ILogger<CreateThreadHandler> logger, ThreadService threadService, PostService postService) : IRequestHandler<CreateThreadRequest, CreateThreadResponse>
{
    private readonly ILogger<CreateThreadHandler> _logger = logger;
    private readonly ThreadService _threadService = threadService;
    private readonly PostService _postService = postService;

    public async Task<CreateThreadResponse> Handle(CreateThreadRequest request, CancellationToken cancellationToken)
    {
        ForumThread newThread = new()
        {
            ThemeId = request.ThemeId,
            Title = request.Title
        };

        string threadId;

        try
        {
            threadId = await _threadService.CreateThread(newThread);
        }
        catch(Exception ex)
        {
            string errorMessage = $"Error al publicar el hilo creado por el usuario con id = \"{request.UserId}\" con titulo: \"{request.Title}\" y con contenido \n\"{request.PostContent}\"\nEn el tema con id = \"{request.ThemeId}\"";
            _logger.LogError(ex, "{message}", errorMessage);
            return new CreateThreadResponse(success: false,
                errors: [ex.Message, $"Hubo un error al publicar tu hilo en el tema con id = \"{request.ThemeId}\". Por favor, intentalo de nuevo más tarde."]);
        }

        _logger.LogInformation("Hilo publicado en el tema con id = \"{themeId}\" por el usuario con id = \"{userId}\"", threadId, request.UserId);

        Post newPost = new()
        {
            ThreadId = threadId,
            UserId = request.UserId,
            Content = request.PostContent
        };

        string postId;

        try
        {
            postId = await _postService.CreatePost(newPost);
        }
        catch (Exception ex)
        {
            string errorMessage = $"Error al publicar el post creado por el usuario con id = \"{request.UserId}\" con contenido\n\"{request.PostContent}\"\nEn el hilo con id = \"{threadId}\"";
            _logger.LogError(ex, "{message}", errorMessage);
            return new CreateThreadResponse(success: false,
                errors: [ex.Message, $"Hubo un error al publicar tu post en el hilo con id = \"{threadId}\". Por favor, intentalo de nuevo más tarde."]);
        }

        _logger.LogInformation("Post publicado en el hilo con id = \"{threadId}\" por el usuario con id = \"{userId}\"", threadId, request.UserId);
        return new CreateThreadResponse(success: true)
        {
            ThreadId = threadId
        };
    }
}
