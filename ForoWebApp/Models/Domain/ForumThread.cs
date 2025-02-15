using ForoWebApp.Constants;

namespace ForoWebApp.Models.Domain;

public class ForumThread
{
    public string Id { get; set; }
    public string ThemeId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneConstants.CentralEuropeanTimeZone);
    public bool IsClosed { get; set; } = false;
    public DateTime? ClosureDate { get; set; } = null;
}
