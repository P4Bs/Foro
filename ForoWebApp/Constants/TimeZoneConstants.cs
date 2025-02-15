namespace ForoWebApp.Constants;

public static class TimeZoneConstants
{
    public const string CentralEuropeanTimezoneID = "Central European Standard Time";
    public static readonly TimeZoneInfo CentralEuropeanTimeZone = TimeZoneInfo.FindSystemTimeZoneById(CentralEuropeanTimezoneID);
}
