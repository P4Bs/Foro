using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models.Requests;

public class UserRegistrationRequest
{
    [StringLength(30, MinimumLength = 4, ErrorMessage = "El nombre de usuario ha de tener un mínimo de 4 y máximo de 30 carácteres")]
    [Required(ErrorMessage = "El campo de nombre de usuario es obligatorio")]
    public string Username { get; set; }

    [Required(ErrorMessage = "El campo de correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "La dirección de correo ha de ser una válida")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
    public string Password { get; set; }

    [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
    public string RepeatedPassword { get; set; }
}
