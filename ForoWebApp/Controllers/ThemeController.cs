using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class ThemeController(ILogger<ThemeController> logger, ThemeService themeService, ThreadService threadService) : Controller
{
	private readonly ILogger<ThemeController> _logger = logger;
	private readonly ThemeService _themeService = themeService;

	public async Task<IActionResult> Index(string themeId)
	{
		ThemeViewModel theme = await _themeService.GetThemeThreads(themeId);
		return View(theme);
	}

}
