namespace ForoWebApp.Models.ViewModels;

public class ThemeViewModel
{
    public string ThemeId { get; set; }
    public string ThemeTitle { get; set; }
    public int TotalThreads { get; set; }
    public int ThreadsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public IEnumerable<ThreadData> Threads { get; set; }
}

public class ThreadData
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string LastUpdateByUser { get; set; }
    public int TotalMessages { get; set; }
}
