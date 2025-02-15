namespace ForoWebApp.Mappers;

public static class ForumThreadMapper
{
    public static Database.Documents.ForumThread ToDatabaseDocument(Models.Domain.ForumThread model)
    {
        return new Database.Documents.ForumThread
        {
            Id = model.Id,
            ThemeId = model.ThemeId,
            Title = model.Title,
            CreatedAt = model.CreatedAt,
            IsClosed = model.IsClosed,
            ClosureDate = model.ClosureDate
        };
    }

    public static Models.Domain.ForumThread ToDomainModel(Database.Documents.ForumThread document)
    {
        return new Models.Domain.ForumThread
        {
            Id = document.Id,
            ThemeId = document.ThemeId,
            Title = document.Title,
            CreatedAt = document.CreatedAt,
            IsClosed = document.IsClosed,
            ClosureDate = document.ClosureDate
        };
    }
}
