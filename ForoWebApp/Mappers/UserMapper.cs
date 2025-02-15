namespace ForoWebApp.Mappers;

public class UserMapper
{
    public static Database.Documents.User ToDatabaseDocument(Models.Domain.User model)
    {
        return new Database.Documents.User
        {
            Id = model.Id,
            Name = model.Name,
            Email = model.Email,
            Password = model.Password,
            RegisteredAt = model.RegisteredAt,
            Role = model.Role
        };
    }

    public static Models.Domain.User ToDomainModel(Database.Documents.User document)
    {
        return new Models.Domain.User
        {
            Id = document.Id,
            Name = document.Name,
            Email = document.Email,
            Password = document.Password,
            RegisteredAt = document.RegisteredAt,
            Role = document.Role
        };
    }
}
