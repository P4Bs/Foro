using ForoWebApp.Database.Documents;

namespace ForoWebApp.Models
{
    public class RegistrationResult(bool success, User? user = null, IEnumerable<string> errors = null)
    {
        public bool Success { get; set; } = success;
        public User User { get; set; } = user;
        public IEnumerable<string> Errors { get; set; } = errors;
    }
}
