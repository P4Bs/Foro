using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models
{
    public record class UserLoginModel
    {
        [Required(ErrorMessage = "El campo de correo electrónico es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de contraseña es obligatorio")]
        public string Password { get; set; }
    }
}
