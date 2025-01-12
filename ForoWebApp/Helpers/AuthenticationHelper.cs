namespace ForoWebApp.Utils;

public class AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
{
    public bool IsAuthenticated() => httpContextAccessor.HttpContext.Session.GetString("AuthToken") != null;
}
