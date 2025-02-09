using ForoWebApp.Database.Documents;

namespace ForoWebApp.Models.ViewModels;

public class HomeViewModel(IList<Theme> themes)
{
    public IList<Theme> Themes { get; set; } = themes;
}
