namespace ForoWebApp.Models.ViewModels;

public class ThemeViewModel
{
    public string ThemeId { get; set; }
    public string ThemeTitle { get; set; }
    public IEnumerable<ThreadData> Threads { get; set; }
}

public class ThreadData
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string LastUpdateUsername { get; set; }
    public int TotalMessages { get; set; }
}
