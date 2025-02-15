using ForoWebApp.Database;
using ForoWebApp.Database.Repositories.Users.Results;
using ForoWebApp.Features.Users.LogUser;
using ForoWebApp.Features.Users.RegisterUser;
using ForoWebApp.Helpers.Passwords;
using ForoWebApp.Mappers;
using ForoWebApp.Models.Domain;
using ForoWebApp.Models.Results;
using ForoWebApp.Validators;

namespace ForoWebApp.Services;

public class UserService(UnitOfWork unitOfWork, IPasswordHelper passwordHelper)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHelper _passwordHelper = passwordHelper;

    public async Task<RegistrationResult> RegisterUser(RegisterRequest model)
    {
        List<FieldValidation> validationErrors = [];

        #region Password Validation
        var passwordErrors = PasswordValidator.ValidatePassword(model.Password, model.ConfirmPassword);

        if(passwordErrors.Any())
        {
            foreach(var validationError in passwordErrors)
            {
                validationErrors.Add(
                    new FieldValidation("Password", validationError));
            }

            return new RegistrationResult(success: false, fieldValidations: validationErrors);
        }
        #endregion

        #region Check user already exists
        var existingUser = await _unitOfWork.UsersRepository.FindUser(model.Email);

        if (existingUser != null)
        {
            validationErrors.Add(
                new FieldValidation("Email", "Ya existe un usuario registrado con este correo electrónico"));
            return new RegistrationResult(success: false, fieldValidations: validationErrors);
        }

        User newUser = new()
        {
            Name = model.Username,
            Email = model.Email,
        };
        #endregion

        #region Hash password
        var hashedPassword = _passwordHelper.HashPassword(newUser, model.Password);
        newUser.Password = hashedPassword;
        #endregion

        InsertUserResult insertUserResult = await _unitOfWork.UsersRepository.TryRegister(newUser);
        if (!insertUserResult.Success || insertUserResult.Id == null)
        {
            return new RegistrationResult(success: false)
            {
                Errors = ["No se pudo registrar al usuario. Intentelo de nuevo pasado un momento"]
            };
        }
        newUser.Id = insertUserResult.Id;

        return new RegistrationResult(success: true)
        {
            User = newUser
        };
    }

    public async Task<LoginResult> LogUser(LogInRequest model)
    {
        var document = await _unitOfWork.UsersRepository.FindUser(model.Email);
        if (document == null)
        {
            return new LoginResult(success: false)
            {
                FieldValidations = [new FieldValidation("Email", "El correo electrónico o la contraseña introducidos no coinciden con los de ningun usuario")]
            };
        }

        User user = UserMapper.ToDomainModel(document);
        if(!_passwordHelper.VerifyPassword(user, user.Password, model.Password))
        {
            return new LoginResult(success: false)
            {
                FieldValidations = [new FieldValidation("Email", "El correo electrónico o la contraseña introducidos no coinciden con los de ningun usuario")],
            };
        }

        return new LoginResult(success: true)
        {
            User = user
        };
    }
}
