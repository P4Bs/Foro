using ForoWebApp.Builders;
using ForoWebApp.Models.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ForoWebApp.Helpers.UserClaims;

public static class IdentityClaimsHelper
{
    public static ClaimsPrincipal GetClaimsIdentity(User model, SigningCredentials signingCredentials)
    {
        var userClaimsList = TokenBuilder.GenerateUserClaims(model, signingCredentials);
        var identityClaims = new ClaimsIdentity(userClaimsList, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identityClaims);
    }
}
