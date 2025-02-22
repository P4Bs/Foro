using ForoWebApp.Controllers.Base;
using ForoWebApp.Features.Users.LogUser;
using ForoWebApp.Features.Users.RegisterUser;
using ForoWebApp.Helpers.UserClaims;
using ForoWebApp.Managers;
using ForoWebApp.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class UserController(IMediator mediator, CredentialsManager credentialsManager) : BaseController
{
    private readonly IMediator _mediator = mediator;
    private readonly CredentialsManager _credentialsManager = credentialsManager;

    [HttpGet("[action]")]
    public IActionResult Login()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return View();
        }
        return NoContent();
    }

    [HttpGet("[action]")]
    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("registerUser")]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Register", viewModel);
        }

        var request = new RegisterRequest
        {
            Username = viewModel.Username,
            Email = viewModel.Email,
            Password = viewModel.Password,
            ConfirmPassword = viewModel.ConfirmPassword
        };

        var response = await _mediator.Send(request);

        if (!response.Success)
        {
            foreach (var validation in response.FieldValidations)
            {
                ModelState.AddModelError(validation.FieldName, validation.ValidationMessage);
            }
            return View("Register", viewModel);
        }

        var principalClaims = IdentityClaimsHelper.GetClaimsIdentity(
            model: response.User,
            signingCredentials: _credentialsManager.SigningCredentials
        );
        var persistSession = new AuthenticationProperties
        {
            IsPersistent = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalClaims, persistSession);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logUser")]
    public async Task<IActionResult> LogUser([FromForm] LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View("Login", viewModel);
        }

        var request = new LogInRequest
        {
            Email = viewModel.Email,
            Password = viewModel.Password
        };

        var response = await _mediator.Send(request);

        if (!response.Success)
        {
            foreach (var validation in response.FieldValidations)
            {
                ModelState.AddModelError(validation.FieldName, validation.ValidationMessage);
            }
            return View("Login", viewModel);
        }

        var principalClaims = IdentityClaimsHelper.GetClaimsIdentity(
            model: response.User,
            signingCredentials: _credentialsManager.SigningCredentials
        );
        var persistSession = new AuthenticationProperties
        {
            IsPersistent = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principalClaims, persistSession);
        return RedirectToAction("Index", "Home");
    }
}
