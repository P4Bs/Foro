using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ForoWebApp.Managers;

public class CredentialsManager(IConfiguration configuration)
{
    private SigningCredentials _signingCredentials;
    public SigningCredentials SigningCredentials => _signingCredentials ??= new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Value)), SecurityAlgorithms.HmacSha512);
}
