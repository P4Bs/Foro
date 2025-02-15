using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Themes.GetTheme;

public class GetThemeHandler(ILogger<GetThemeHandler> logger, ThemeService themeService) : IRequestHandler<GetThemeRequest, GetThemeResponse>
{
    private readonly ILogger<GetThemeHandler> _logger = logger;
    private readonly ThemeService _themeService = themeService;

    public async Task<GetThemeResponse> Handle(GetThemeRequest request, CancellationToken cancellationToken)
    {
        ThemeViewModel theme;

        try
        {
            theme = await _themeService.GetTheme(request.ThemeId);
        }
        catch (Exception ex)
        {
            string errorMessage =
                $"Error al obtener los hilos en el tema con id = {request.ThemeId}";
            _logger.LogError(ex, "{message}", errorMessage);
            return new GetThemeResponse(success: false,
                errors: [ex.Message, $"Hubo un error al obtener los hilos del tema con id = {request.ThemeId}. Por favor, intentalo de nuevo más tarde"]);
        }

        return new GetThemeResponse(success: true)
        {
            Theme = theme
        };
    }
}
