using System.Diagnostics;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models.ViewModels;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ThemeService themeService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ThemeService _themeService = themeService;

        public async Task<IActionResult> Index()
        {
            List<Theme> themesList = await _themeService.GetThemes();
            return View(themesList);
        }

        public async Task<IActionResult> Privacy()
        {
            var temas = await _themeService.GetThemes();
            return View(temas);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
