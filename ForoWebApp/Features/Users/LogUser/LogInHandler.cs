using ForoWebApp.Services;
using MediatR;

namespace ForoWebApp.Features.Users.LogUser;

public class LogInHandler(ILogger<LogInHandler> logger, UserService userService) : IRequestHandler<LogInRequest, LogInResponse>
{
    private readonly ILogger<LogInHandler> _logger = logger;
    private readonly UserService _userService = userService;

    public async Task<LogInResponse> Handle(LogInRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.LogUser(request);

        if (!result.Success)
        {
            if (result.Errors.Length != 0)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError("Error al logar al usuario {userEmail} : {errorMessage} ", request.Email, error);
                }
            }
            if (result.FieldValidations.Count != 0)
            {
                foreach (var fieldValidation in result.FieldValidations)
                {
                    _logger.LogError("Error de validación en el campo {nombreCampo}: {mensaje}", fieldValidation.FieldName, fieldValidation.ValidationMessage);
                }
            }

            return new LogInResponse(success: false)
            {
                Errors = result.Errors,
                FieldValidations = result.FieldValidations
            };
        }

        return new LogInResponse(success: true)
        {
            User = result.User,
        };
    }
}
