using ForoWebApp.Database.Documents;
using ForoWebApp.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ForumThread = ForoWebApp.Database.Documents.ForumThread;

namespace ForoWebApp.Database;

public class DbContext
{
	private readonly IMongoDatabase _database;
    private IMongoCollection<Message> _messages;
	private IMongoCollection<Theme> _themes;
    private IMongoCollection<ForumThread> _threads;
    private IMongoCollection<User> _users;

    public IMongoCollection<Message> Messages => _messages ??= GetCollection<Message>("messages");
    public IMongoCollection<Theme> Themes => _themes ??= GetCollection<Theme>("themes");
    public IMongoCollection<ForumThread> Threads => _threads ??= GetCollection<ForumThread>("threads");
    public IMongoCollection<User> Users => _users ??= GetCollection<User>("users");

    public DbContext(IOptionsMonitor<DbSettings> options)
	{
        var client = new MongoClient(options.CurrentValue.ConnectionString);
        _database = client.GetDatabase(options.CurrentValue.DatabaseName);
    }

	protected IMongoCollection<T> GetCollection<T>(string collectionName)
	{
		return _database.GetCollection<T>(collectionName);
	}
}
