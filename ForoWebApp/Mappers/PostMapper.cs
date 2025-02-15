namespace ForoWebApp.Mappers;

public static class PostMapper
{
    public static Database.Documents.Post ToDatabaseDocument(Models.Domain.Post model)
    {
        return new Database.Documents.Post
        {
            Id = model.Id,
            ThreadId = model.ThreadId,
            UserId = model.UserId,
            Content = model.Content,
            PostDate = model.PostDate
        };
    }
    public static Models.Domain.Post ToDomainModel(Database.Documents.Post document)
    {
        return new Models.Domain.Post
        {
            Id = document.Id,
            ThreadId = document.ThreadId,
            UserId = document.UserId,
            Content = document.Content,
            PostDate = document.PostDate
        };
    }
}
