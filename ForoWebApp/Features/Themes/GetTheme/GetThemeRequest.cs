using MediatR;

namespace ForoWebApp.Features.Themes.GetTheme;

public class GetThemeRequest : IRequest<GetThemeResponse>
{
    public string ThemeId { get; set; }
}
