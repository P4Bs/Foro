using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class ThemeController(ThemeService themeService) : Controller
{
	private readonly ThemeService _themeService = themeService;

	public async Task<IActionResult> Index(string themeId)
	{
		ThemeViewModel theme = await _themeService.GetThemeThreads(themeId);
		return View(theme);
	}
}
