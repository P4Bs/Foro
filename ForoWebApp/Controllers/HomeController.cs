using ForoWebApp.Controllers.Base;
using ForoWebApp.Features.Home.GetHome;
using ForoWebApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class HomeController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetHomeRequest());

        if (response.Success)
        {
            return View(new HomeViewModel(response.Themes));
        }

        return View(new ErrorViewModel(GetRequestId(), response.Errors));
    }
}
