using ForoWebApp.Features.Common;
using ForoWebApp.Models.Domain;
using ForoWebApp.Validators;

namespace ForoWebApp.Features.Users.LogUser;

public class LogInResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public List<FieldValidation> FieldValidations { get; set; } = [];
    public User? User { get; set; }
}
