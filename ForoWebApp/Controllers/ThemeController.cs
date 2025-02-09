using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThemeController(ILogger<ThemeController> logger, ThemeService themeService) : Controller
{
    private readonly ILogger<ThemeController> _logger = logger;
    private readonly ThemeService _themeService = themeService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Index(string id)
    {
        ThemeViewModel theme;
        try
        {
            theme = await _themeService.GetThemeThreads(id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al obtener el hilo con id = {id}: {exceptionMessage}", id, ex.Message);
            throw;
        }

        return View(theme);
    }
}
