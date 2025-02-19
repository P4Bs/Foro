using ForoWebApp.Controllers.Base;
using ForoWebApp.Features.Themes.GetTheme;
using ForoWebApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class ThemeController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Index(string id)
    {
        var request = new GetThemeRequest
        {
            ThemeId = id
        };

        var response = await _mediator.Send(request);

        if (response.Success)
        {
            return View(response.Theme);
        }
        
        return View("Error", new ErrorViewModel(GetRequestId(), response.Errors));
    }
}
