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
            return $"hace {daysSince} días a las {timeString}";
        }
        else if (daysSince < 30)
        {
            return $"hace {daysSince / 7} semanas a las {timeString}";
        }
        else if (daysSince < 365)
        {
            return $"hace {daysSince / 30} meses a las {timeString}";
        }
        else
        {
            return $"hace {daysSince / 365} años a las {timeString}";
        }
    }
}
