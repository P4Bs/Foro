using ForoWebApp.Database.Documents;

namespace ForoWebApp.Models
{
    public class LoginResult(bool success, User? user = null)
    {
        public bool Success { get; set; } = success;
        public User User { get; set; } = user;
    }
}
