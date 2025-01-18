using ForoWebApp.Builders;
using ForoWebApp.Database.Documents;
using ForoWebApp.Managers;
using ForoWebApp.Models;
using ForoWebApp.Services;
using ForoWebApp.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class UserController(ILogger<UserController> logger, UserService userService, CredentialsManager credentialsManager) : Controller
{
	private readonly ILogger<UserController> _logger = logger;
	private readonly UserService _userService = userService;
	private readonly CredentialsManager _credentialsManager = credentialsManager;

	[HttpGet("[action]")]
	public IActionResult Login()
	{
		if (User.Identity.IsAuthenticated)
		{
			return Redirect("/");
		}
		return View();
    }

	[HttpGet("[action]")]
	public IActionResult Register()
	{
		if (User.Identity.IsAuthenticated)
		{
			return Redirect("/");
		}
		return View();
	}

	[HttpPost("RegisterUser")]
	public async Task<IActionResult> RegisterUser([FromForm] UserRegistrationModel model)
	{
		if (!ModelState.IsValid)
		{
			return View("Register", model);
		}

		// PASSWORD VALIDATION
		var passwordErrors = PasswordValidator.ValidatePassword(model.Password, model.RepeatedPassword);

		if (passwordErrors.Any())
		{
            foreach (var error in passwordErrors)
            {
                ModelState.AddModelError("Password", error);
            }
            return View("Register", model);
        }

        RegistrationResult result = await _userService.RegisterUser(model);

		if (!result.Success)
		{
            _logger.LogError("No se pudo registrar al usuario");
			foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Email", error);
            }
			return View("Register", model);
		}

		(ClaimsPrincipal userClaimsPrincipal, AuthenticationProperties userAuthenticationProperties) = GenerateUserClaimsAndProperties(result.User);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrincipal, userAuthenticationProperties);

		return Redirect("/");
    }

	[HttpPost("LogUser")]
	public async Task<IActionResult> LogUser([FromForm] UserLoginModel model)
	{
		if (!ModelState.IsValid)
		{
			return View("Login", model);
		}

		LoginResult result = await _userService.LogUser(model);

		if (!result.Success)
		{
			foreach(var error in result.Errors)
            {
                ModelState.AddModelError("Email", error);
            }
			return View("Login", model);
		}

        (ClaimsPrincipal userClaimsPrincipal, AuthenticationProperties userAuthenticationProperties) = GenerateUserClaimsAndProperties(result.User);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimsPrincipal, userAuthenticationProperties);

		return Redirect("/");
	}

	[HttpPost("Logout")]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		return Redirect("/");
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
