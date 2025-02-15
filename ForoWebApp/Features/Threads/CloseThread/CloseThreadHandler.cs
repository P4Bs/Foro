using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Threads.CloseThread;

public class CloseThreadHandler(ILogger<CloseThreadHandler> logger, ThreadService threadService) : IRequestHandler<CloseThreadRequest, CloseThreadResponse>
{
    private readonly ILogger<CloseThreadHandler> _logger = logger;
    private readonly ThreadService _threadService = threadService;

    public async Task<CloseThreadResponse> Handle(CloseThreadRequest request, CancellationToken cancellationToken)
    {
        bool isSuccessful;
        try
        {
            isSuccessful = await _threadService.CloseThread(request.ThreadId);
        }
        catch (Exception ex)
        {
            string errorMessage = $"Error al cerrar el hilo con id = {request.ThreadId}";
            _logger.LogError(ex, "{message}", errorMessage);
            return new CloseThreadResponse(success: false,
                errors: [ex.Message, $"Hubo un error al cerrar el hilo con id = {request.ThreadId}. Por favor, intentalo de nuevo más tarde"]);
        }

        if (isSuccessful)
        {
            return new CloseThreadResponse(success: true);
        }

        return new CloseThreadResponse(success: false,
            errors: [$"Hubo un error al cerrar el hilo con id = {request.ThreadId}. Por favor, intentalo de nuevo más tarde"]);
    }
}
