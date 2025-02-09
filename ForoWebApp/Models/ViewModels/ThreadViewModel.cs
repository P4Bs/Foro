namespace ForoWebApp.Models.ViewModels;

public class ThreadViewModel
{
    public string ThreadId { get; set; }
    public string ThreadName { get; set; }
    public bool IsClosed { get; set; }
    public IEnumerable<PostData> Posts { get; set; }
    public int TotalPosts { get; set; }
    public int TotalPostsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}

public class PostData
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public DateTime PostDate { get; set; }
}
