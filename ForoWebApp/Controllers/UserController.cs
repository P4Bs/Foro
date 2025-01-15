using ForoWebApp.Builders;
using ForoWebApp.Database.Documents;
using ForoWebApp.Managers;
using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

public class UserController(ILogger<UserController> logger, UserService userService, CredentialsManager credentialsManager) : Controller
{
	private readonly ILogger<UserController> _logger = logger;
	private readonly UserService _userService = userService;
	private readonly CredentialsManager _credentialsManager = credentialsManager;

	[HttpGet]
	public IActionResult Login()
	{
		if (User.Identity.IsAuthenticated)
		{
			return Redirect("/");
		}
		return View();
    }


	[HttpGet]
	public IActionResult Register()
	{
		if (User.Identity.IsAuthenticated)
		{
			return RedirectToAction("Profile");
		}
		return View();
	}

	[HttpGet]
	public IActionResult Profile()
	{
		if (!User.Identity.IsAuthenticated)
		{
			return RedirectToAction("Register");
		}
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> RegisterUser([FromForm] UserRegistrationModel userData)
	{
		RegistrationResult result = await _userService.RegisterUser(userData);

		if (!result.Success)
		{
            // TODO: PUT ERROR
            _logger.LogError("No se pudo registrar al usuario");
			return new RedirectResult("/");
		}

		(ClaimsPrincipal userClaimsPrincipal, AuthenticationProperties userAuthenticationProperties) = GenerateUserClaimsAndProperties(result.User);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrincipal, userAuthenticationProperties);

		return new RedirectResult("Index");
    }

	[HttpPost]
	public async Task<IActionResult> LogUser([FromForm] UserLoginModel userLogin)
	{
		LoginResult result = await _userService.LogUser(userLogin);

		if (!result.Success)
		{
			return Unauthorized("Invalid user credentials");
		}

        (ClaimsPrincipal userClaimsPrincipal, AuthenticationProperties userAuthenticationProperties) = GenerateUserClaimsAndProperties(result.User);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrincipal, userAuthenticationProperties);

		return new RedirectResult("Index");
	}

	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		return RedirectToAction("Index");
	}

	private (ClaimsPrincipal, AuthenticationProperties) GenerateUserClaimsAndProperties(User user)
	{
        List<Claim> userClaims = TokenBuilder.GenerateUserClaims(user, _credentialsManager.SigningCredentials);
		
		var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
		var authenticationProperties = new AuthenticationProperties
		{
			IsPersistent = true
		};

		return (new ClaimsPrincipal(claimsIdentity), authenticationProperties);
    }
}
