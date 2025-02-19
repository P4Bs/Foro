using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Threads.GetThread;

public class GetThreadHandler(ILogger<GetThreadHandler> logger, ThreadService threadService) : IRequestHandler<GetThreadRequest, GetThreadResponse>
{
    private readonly ILogger<GetThreadHandler> _logger = logger;
    private readonly ThreadService _threadService = threadService;

    public async Task<GetThreadResponse> Handle(GetThreadRequest request, CancellationToken cancellationToken)
    {
        ThreadViewModel threadViewModel;

        try
        {
            threadViewModel = await _threadService.GetThreadPosts(request.ThreadId, request.Page);
        }
        catch(Exception ex)
        {
            string errorMessage =
                $"Error al obtener los posts del hilo con id = {request.ThreadId}";
            _logger.LogError(ex, "{message}", errorMessage);
            return new GetThreadResponse(success: false,
                errors: [ex.Message, $"Hubo un error al obtener los posts del hilo con id = {request.ThreadId}. Por favor, intentalo de nuevo más tarde"]);
        }

        return new GetThreadResponse(success: true)
        {
            ThreadViewModel = threadViewModel
        };
    }
}
