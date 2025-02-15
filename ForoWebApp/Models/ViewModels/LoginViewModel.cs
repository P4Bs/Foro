using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models.ViewModels;

public record class LoginViewModel
{
    [Required(ErrorMessage = "El campo de correo electrónico es obligatorio")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
    public string Password { get; set; }
}
