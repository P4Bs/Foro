using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThemeController(ThemeService themeService) : Controller
{
    private readonly ThemeService _themeService = themeService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Index(string id)
    {
        ThemeViewModel theme = await _themeService.GetThemeThreads(id);
        return View(theme);
    }
}
