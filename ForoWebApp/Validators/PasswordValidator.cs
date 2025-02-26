using System.Text.RegularExpressions;

namespace ForoWebApp.Validators;

public partial class PasswordValidator
{
    public static IEnumerable<string> ValidatePassword(string password, string repeatedPassword)
    {
        if (password != repeatedPassword)
        {
            yield return "Las contraseñas no coinciden";
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            yield return "La contraseña no puede estar vacía";
        }
        if (password.Length < 6)
        {
            yield return "La contraseña debe tener al menos 6 caracteres";
        }
        if (!password.Any(char.IsDigit))
        {
            yield return "La contraseña debe contener al menos un número";
        }
        if (!password.Any(char.IsUpper))
        {
            yield return "La contraseña debe contener al menos una letra mayúscula";
        }
        if (!password.Any(char.IsLower))
        {
            yield return "La contraseña debe contener al menos una letra minúscula";
        }
        if (!SpecialSymbolRegex().IsMatch(password))
        {
            yield return "La contraseña debe contener al menos un símbolo especial";
        }
    }

    [GeneratedRegex(@"[@$!%*?&/\\#^+{}()<>|~`'"";:.,\[\]-]")]
    private static partial Regex SpecialSymbolRegex();
}
