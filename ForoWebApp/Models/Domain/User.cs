using ForoWebApp.Constants;

namespace ForoWebApp.Models.Domain;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime RegisteredAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneConstants.CentralEuropeanTimeZone);
    public string Role { get; set; } = UserConstants.UserRole;
}

