using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models;

namespace ForoWebApp.Services;

public class UserService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public Task<(string, bool)> RegisterUser(UserRegistrationModel model)
	{
		User newUser = new()
		{
			Name = model.Name,
			Email = model.Email,
			Password = model.Password,
			RegisteredAt = model.RegisteredAt,
		};

		return _unitOfWork.UsersRepository.TryRegister(newUser);
    }
}
