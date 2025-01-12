using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using ForoWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ForoWebApp.Controllers;

public class UserController(ILogger<UserController> logger, UserService userService) : Controller
{
	private readonly ILogger<UserController> _logger = logger;
	private readonly UserService _userService = userService;

	[HttpGet]
	public IActionResult LogIn()
	{
		if(HttpContext.Session.GetString("AuthToken") == null)
		{
            return View();
        }
		return new RedirectResult("User/Profile");
    }


	[HttpGet]
	public IActionResult Registration()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Profile()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> RegisterUser([FromForm] UserRegistrationModel userData)
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

	[HttpPost]
	public async Task<IActionResult> LogUser([FromForm] UserLoginModel userLogin)
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
