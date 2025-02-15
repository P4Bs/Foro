namespace ForoWebApp.Database.Repositories.Users.Results;

public class InsertUserResult(bool success, string? id = null)
{
    public bool Success { get; set; } = success;
    public string Id { get; set; } = id;
}
