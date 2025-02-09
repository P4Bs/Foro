using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Helpers.Passwords;
using ForoWebApp.Models.Requests;
using ForoWebApp.Models.Results;
using ForoWebApp.Models.Static;

namespace ForoWebApp.Services;

public class UserService(UnitOfWork unitOfWork, IPasswordHelper passwordHelper)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHelper _passwordHelper = passwordHelper;

    public async Task<RegistrationResult> RegisterUser(RegisterUserRequest model)
    {
        IList<string> validationErrors = [];
        var existingUser = await _unitOfWork.UsersRepository.FindUser(model.Email);

        if (existingUser != null)
        {
            validationErrors.Add("Ya existe un usuario registrado con este correo electrónico");
            return new RegistrationResult(success: false, errors: validationErrors);
        }

        User newUser = new()
        {
            Name = model.Username,
            Email = model.Email,
            RegisteredAt = DateTime.UtcNow,
            Role = UserRole.USER
        };

        var hashedPassword = _passwordHelper.HashPassword(newUser, model.Password);
        newUser.Password = hashedPassword;

        bool success = await _unitOfWork.UsersRepository.TryRegister(newUser);

        if (!success)
        {
            validationErrors.Add("No se pudo registrar al usuario. Intentelo de nuevo pasado un momento");
            return new RegistrationResult(success: false, errors: validationErrors);
        }

        return new RegistrationResult(success: true, newUser);
    }

    public async Task<LoginResult> LogUser(UserLoginRequest model)
    {
        User user = await _unitOfWork.UsersRepository.FindUser(model.Email);

        if (user == null || !_passwordHelper.VerifyPassword(user, user.Password, model.Password))
        {
            return new LoginResult(success: false, errors: ["El correo electrónico o la contraseña introducidos no son correctos"]);
        }

        return new LoginResult(success: true, user);
    }
}
