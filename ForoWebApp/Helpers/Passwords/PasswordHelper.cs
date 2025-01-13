using ForoWebApp.Database.Documents;
using Microsoft.AspNetCore.Identity;

namespace ForoWebApp.Helpers.Passwords;

public class PasswordHelper(IPasswordHasher<User> passwordHasher) : IPasswordHelper
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string hashedPassword, string password)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
        return verificationResult.Equals(PasswordVerificationResult.Success);
    }
}
