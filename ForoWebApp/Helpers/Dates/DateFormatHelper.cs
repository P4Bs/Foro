namespace ForoWebApp.Helpers.Dates;

public static class DateFormatHelper
{
    public static string FormatDate(DateTime date)
    {
        var daysSince = DateTime.UtcNow.Subtract(date).Days;

        if (daysSince < 7)
        {
            return $"hace {daysSince} días a las {date.Hour}:{date.Minute}";
        }
        else if (daysSince < 30)
        {
            return $"hace {daysSince / 7} semanas a las {date.Hour}:{date.Minute}";
        }
        else if (daysSince < 365)
        {
            return $"hace {daysSince / 30} meses a las {date.Hour}:{date.Minute}";
        }
        else
        {
            return $"hace {daysSince / 365} años a las {date.Hour}:{date.Minute}";
        }
    }
}
