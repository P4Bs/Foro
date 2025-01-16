using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Helpers.Passwords;
using ForoWebApp.Models;

namespace ForoWebApp.Services;

public class UserService(UnitOfWork unitOfWork, IPasswordHelper passwordHelper)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
	private readonly IPasswordHelper _passwordHelper = passwordHelper;

	public async Task<RegistrationResult> RegisterUser(UserRegistrationModel model)
	{
		var existingUser = await _unitOfWork.UsersRepository.FindUser(model.Email);
		if (existingUser != null)
		{
			return new RegistrationResult(success: false);
		}

		User newUser = new()
		{
			Name = model.Username,
			Email = model.Email,
			RegisteredAt = DateTime.UtcNow,
		};

		var hashedPassword = _passwordHelper.HashPassword(newUser, model.Password);
		newUser.Password = hashedPassword;

		bool success = await _unitOfWork.UsersRepository.TryRegister(newUser);

		if (!success)
		{
			return new RegistrationResult(success: false);
		}

		return new RegistrationResult(success: true, newUser);
    }

	public async Task<LoginResult> LogUser(UserLoginModel model)
	{
		User user = await _unitOfWork.UsersRepository.FindUser(model.Email);

		if(user == null || _passwordHelper.VerifyPassword(user, user.Password, model.Password))
		{
			return new LoginResult(success: false);
		}

		return new LoginResult(success: true, user);
	}
}
