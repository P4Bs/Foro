using ForoWebApp.Models.Domain;
using ForoWebApp.Validators;

namespace ForoWebApp.Models.Results;

public class RegistrationResult(bool success, string[]? errors = null, List<FieldValidation>? fieldValidations = null, User? user = null)
{
    public bool Success { get; set; } = success;
    public string[]? Errors { get; set; } = errors ?? []; 
    public List<FieldValidation> FieldValidations { get; set; } = fieldValidations ?? [];
    public User? User { get; set; } = user;
}
