using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories;

public abstract class GenericRepository<TDocument>
{
    protected IMongoCollection<TDocument> Collection { get; private set; }

    public async Task<IAsyncCursor<TDocument>> GetAllAsync()
    {
        return await Collection.FindAsync(_ => true);
    }
    /*
    public async Task<TDocument> GetByIdAsync(string id)
    {
        return await Collection.FindAsync(element => );
    }*/
}
