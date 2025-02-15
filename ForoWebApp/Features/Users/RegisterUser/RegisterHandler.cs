using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Users.RegisterUser;

public class RegisterHandler(ILogger<RegisterHandler> logger, UserService userService) : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly ILogger<RegisterHandler> _logger = logger;
    private readonly UserService _userService = userService;

    public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.RegisterUser(request);

        if (!result.Success)
        {
            if (result.Errors.Length != 0)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError("Error al registrar al usuario {userEmail} : {errorMessage} ", request.Email, error);
                }
            }
            if (result.FieldValidations.Count != 0)
            {
                foreach (var fieldValidation in result.FieldValidations)
                {
                    _logger.LogError("Error de validación en el campo {nombreCampo}: {mensaje}", fieldValidation.FieldName, fieldValidation.ValidationMessage);
                }

                return new RegisterResponse(success: false)
                {
                    Errors = result.Errors,
                    FieldValidations = result.FieldValidations
                };
            }
        }

        return new RegisterResponse(success: true)
        {
            User = result.User,
        };
    }
}
