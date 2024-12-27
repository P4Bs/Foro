using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories;

public interface IGenericRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> GetCollectionAsQueryable();

    Task<List<TDocument>> GetAllAsync();

    Task<TDocument> GetByIdAsync(string id);

    Task<string> InsertAsync(TDocument document);

    Task UpdateAsync(string id, UpdateDefinition<TDocument>[] document);

    Task DeleteAsync(string id);
}
