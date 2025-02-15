using ForoWebApp.Models.Domain;

namespace ForoWebApp.Models.ViewModels;

public class HomeViewModel(IEnumerable<Theme> themes)
{
    public IEnumerable<Theme> Themes { get; set; } = themes;
}
