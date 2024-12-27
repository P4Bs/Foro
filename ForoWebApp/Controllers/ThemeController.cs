using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThemeController(ILogger<ThemeController> logger, ThemeService themeService) : Controller
{
	private readonly ILogger<ThemeController> _logger = logger;
	private readonly ThemeService _themeService = themeService;


}
