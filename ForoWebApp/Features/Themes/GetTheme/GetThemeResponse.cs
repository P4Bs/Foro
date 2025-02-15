using ForoWebApp.Features.Common;
using ForoWebApp.Models.ViewModels;

namespace ForoWebApp.Features.Themes.GetTheme;

public class GetThemeResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public ThemeViewModel? Theme { get; set; }
}
