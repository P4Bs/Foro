namespace ForoWebApp.Mappers;

public static class ThemeMapper
{
    public static Models.Domain.Theme ToDomainModel(Database.Documents.Theme document)
    {
        return new Models.Domain.Theme
        {
            Id = document.Id,
            Name = document.Name,
            Description = document.Description,
            CreatedAt = document.CreatedAt
        };
    }
}
