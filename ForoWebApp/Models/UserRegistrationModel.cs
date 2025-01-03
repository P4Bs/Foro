using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models
{
    public class UserRegistrationModel
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }
    }
}
