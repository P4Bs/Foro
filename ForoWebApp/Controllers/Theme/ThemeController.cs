using ForoWebApp.Database.Repositories.Themes;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers.Theme
{
	[Route("[controller]")]
	public class ThemeController(ILogger<ThemeController> logger, IThemesRepository themesRepository) : Controller
	{
		private readonly ILogger<ThemeController> _logger = logger;
		private readonly IThemesRepository _themesRepository = themesRepository;


	}
}
