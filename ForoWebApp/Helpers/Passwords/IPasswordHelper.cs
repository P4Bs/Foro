using ForoWebApp.Database.Documents;

namespace ForoWebApp.Helpers.Passwords;

public interface IPasswordHelper
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string hashedPassword, string password);
}
