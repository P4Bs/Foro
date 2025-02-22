using ForoWebApp.Database.Documents;
using ForoWebApp.Models.Settings;
using MongoDB.Driver;

namespace ForoWebApp.Database;

public class DbContext
{
    private readonly IMongoDatabase _database;
    private IMongoCollection<Post> _posts;
    private IMongoCollection<Theme> _themes;
    private IMongoCollection<ForumThread> _threads;
    private IMongoCollection<User> _users;

    public IMongoCollection<Post> Posts => _posts ??= GetCollection<Post>("post");
    public IMongoCollection<Theme> Themes => _themes ??= GetCollection<Theme>("theme");
    public IMongoCollection<ForumThread> Threads => _threads ??= GetCollection<ForumThread>("thread");
    public IMongoCollection<User> Users => _users ??= GetCollection<User>("user");

    public DbContext(DatabaseConfiguration dbConfig)
    {
        var client = new MongoClient(dbConfig.ConnectionString);
        _database = client.GetDatabase(dbConfig.DatabaseName);
    }

    protected IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}
