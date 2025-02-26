using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ForoWebApp.Managers;

public class CredentialsManager
{
    private SigningCredentials _signingCredentials;
    public SigningCredentials SigningCredentials => _signingCredentials ??= new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET__KEY"))), SecurityAlgorithms.HmacSha256);
}
