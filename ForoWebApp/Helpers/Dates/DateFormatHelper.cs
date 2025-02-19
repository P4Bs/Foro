namespace ForoWebApp.Helpers.Dates;

public static class DateFormatHelper
{
    public static string FormatDate(DateTime date)
    {
        var daysSince = DateTime.UtcNow.Subtract(date).Days;
        var timeString = date.ToString("HH:mm");

        if (daysSince == 0)
        {
            return $"hoy a las {timeString}";
        }
        else if (daysSince < 7)
        {
            return $"hace {daysSince} día{(daysSince > 1 ? "s" : string.Empty)} a las {timeString}";
        }
        else if (daysSince < 30)
        {
            return $"hace {daysSince / 7} semana{(daysSince > 13 ? "s" : string.Empty)} a las {timeString}";
        }
        else if (daysSince < 365)
        {
            return $"hace {daysSince / 30} mes{(daysSince > 59 ? "es" : string.Empty)} a las {timeString}";
        }
        else
        {
            return $"hace {daysSince / 365} año{(daysSince > 729 ? "s" : string.Empty)} a las {timeString}";
        }
    }
}
