using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

[Route("[controller]")]
public class UserController(ILogger<UserController> logger, UserService userService) : Controller
{
	private readonly ILogger<UserController> _logger = logger;
	private readonly UserService _userService = userService;

	public async Task<IActionResult> RegisterUser(UserRegistrationModel userData)
	{
		(string userId, bool successfulRegister) = await _userService.RegisterUser(userData);

		if (successfulRegister)
		{
			return new RedirectResult($"/user/userId={userId}");
		}
		else
		{
			//TODO: put log error
			_logger.LogError("");
			return View(userId);
		}
	}

	public async Task<IActionResult> LogIn(UserLoginModel userLogin)
	{
		(string userId, bool successfulLogin) = await _userService.LogUser(userLogin);

		if (successfulLogin)
		{
			//TODO: TOKEN? ??
		}

		//todo: xd
		return null;
	}
}
