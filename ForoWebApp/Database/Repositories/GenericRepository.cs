using ForoWebApp.Database.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Database.Repositories;

public abstract class GenericRepository<TDocument>(IMongoCollection<TDocument> collection) : IGenericRepository<TDocument> where TDocument : IDocument
{
    protected IMongoCollection<TDocument> Collection { get; private set; } = collection;

    public IQueryable<TDocument> GetCollectionAsQueryable()
    {
        return Collection.AsQueryable();
    }

    public Task<List<TDocument>> GetAllAsync()
    {
        return GetCollectionAsQueryable().ToListAsync();
    }

    public Task<TDocument> GetByIdAsync(string id)
    {
        return GetCollectionAsQueryable().FirstOrDefaultAsync(document => document.Id == id);
    }

    /// <summary>
    /// Inserts a new document inside the collection
    /// </summary>
    /// <param name="document">An object that inherits the interface TDocument</param>
    /// <returns>The id of the documment inserted</returns>
    public async Task<string> InsertAsync(TDocument document)
    {
        await Collection.InsertOneAsync(document);
        return document.Id;
    }

    public async Task<UpdateResult> UpdateAsync(string id, UpdateDefinition<TDocument>[] documentUpdates)
    {
        return await Collection.UpdateOneAsync(
           filter: d => d.Id == id,
           update: Builders<TDocument>.Update.Combine(documentUpdates)
        );
    }

    public async Task DeleteAsync(string id)
    {
        await Collection.DeleteOneAsync(document => document.Id == id);
    }
}
