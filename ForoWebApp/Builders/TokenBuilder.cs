using ForoWebApp.Database.Documents;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ForoWebApp.Builders;

public static class TokenBuilder
{
    public static List<Claim> GenerateUserClaims(User user, SigningCredentials signingCredentials)
    {
        List<Claim> userClaims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        ];

        JwtSecurityToken userTokenDescriptor = new(
            claims: userClaims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        string userToken = new JwtSecurityTokenHandler().WriteToken(userTokenDescriptor);

        userClaims.Add(new Claim("JWT", userToken));

        return userClaims;
    }
}
