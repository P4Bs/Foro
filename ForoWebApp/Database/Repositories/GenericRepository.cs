using ForoWebApp.Database.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Database.Repositories;

public abstract class GenericRepository<TDocument> : IGenericRepository<TDocument> where TDocument : IDocument
{
    protected IMongoCollection<TDocument> Collection { get; private set; }

    public GenericRepository(IMongoCollection<TDocument> collection)
    {
        Collection = collection;
    }

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

	public async Task<string> InsertAsync(TDocument document)
	{
	    await Collection.InsertOneAsync(document);
        return document.Id;
	}

	public async Task UpdateAsync(string id, UpdateDefinition<TDocument>[] documentUpdates)
	{
        await Collection.UpdateOneAsync(
           filter: d => d.Id == id, 
           update: Builders<TDocument>.Update.Combine(documentUpdates)
        );
	}

	public async Task DeleteAsync(string id)
	{
        await Collection.DeleteOneAsync(document => document.Id == id);
	}
}
