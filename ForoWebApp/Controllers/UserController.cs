using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class UserController(ILogger<UserController> logger, UserService userService) : Controller
{
	private readonly ILogger<UserController> _logger = logger;
	private readonly UserService _userService = userService;

	public IActionResult LogIn()
	{
		return View();
	}

	public IActionResult Registration()
	{
		return View();
	}

	public async Task<IActionResult> RegisterUser(UserRegistrationModel userData)
	{
		(string userToken, User newUser) = await _userService.RegisterUser(userData);

		if (userToken == null)
		{
            // TODO: PUT ERROR
            _logger.LogError("No se pudo registrar al usuario");
			return new RedirectResult("/");
            
		}

		HttpContext.Session.SetString("AuthToken", userToken);

        return new RedirectResult($"/user/userId={newUser.Id}");
    }

	public async Task<IActionResult> LogUser(UserLoginModel userLogin)
	{
		string userToken = await _userService.LogUser(userLogin);

		if (userToken == null)
		{
			return Unauthorized();
		}

        HttpContext.Session.SetString("AuthToken", userToken);

        return Ok(userToken);
	}
}
