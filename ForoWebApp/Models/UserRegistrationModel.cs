using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models
{
    public class UserRegistrationModel
    {
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
