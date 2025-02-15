using ForoWebApp.Database.Documents;
using ForoWebApp.Mappers;
using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Home.GetHome;

public class GetHomeHandler(ILogger<GetHomeHandler> logger, ThemeService themeService) : IRequestHandler<GetHomeRequest, GetHomeResponse>
{
    private readonly ILogger<GetHomeHandler> _logger = logger;
    private readonly ThemeService _themeService = themeService;

    public async Task<GetHomeResponse> Handle(GetHomeRequest request, CancellationToken cancellationToken)
    {
        List<Theme> themeDocumentList;
        try
        {
            themeDocumentList = await _themeService.GetThemes();
        }
        catch(Exception ex)
        {
            string errorMessage = "Error al obtener los temas de la aplicación";
            _logger.LogError(ex, "{message}", errorMessage);
            return new GetHomeResponse(success: false,
                [ex.Message, "Hubo un error al cargar los temas del foro. Por favor, intentalo más tarde"]);
        }

        var themeList = themeDocumentList.Select(ThemeMapper.ToDomainModel);

        return new GetHomeResponse(success: true)
        {
            Themes = themeList
        };
    }
}
