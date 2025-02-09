using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class HomeController(ThemeService themeService) : Controller
{
    private readonly ThemeService _themeService = themeService;

    public async Task<IActionResult> Index()
    {
        return View(new HomeViewModel(await _themeService.GetThemes()));
    }
}
