using ForoWebApp.Constants;

namespace ForoWebApp.Models.Domain;

public class Post
{
    public string Id { get; set; }
    public string ThreadId { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime PostDate { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneConstants.CentralEuropeanTimeZone);
}
