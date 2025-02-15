using ForoWebApp.Database.Documents;
using ForoWebApp.Database.Repositories.Users.Results;
using ForoWebApp.Mappers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Database.Repositories.Users;

public class UsersRepository(DbContext context) : GenericRepository<User>(context.Users)
{
    public async Task<InsertUserResult> TryRegister(Models.Domain.User user)
    {
        var document = UserMapper.ToDatabaseDocument(user);
        try
        {
            await Collection.InsertOneAsync(document);
        }
        catch (Exception)
        {
            return new InsertUserResult(success: false);
        }
        return new InsertUserResult(success: true, document.Id);
    }

    public Task<User> FindUser(string userEmail)
    {
        return Collection.AsQueryable().FirstOrDefaultAsync(user => user.Email == userEmail);
    }
}
