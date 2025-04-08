using Infrastructure.Context;
using MongoDB.Driver;

namespace Infrastructure.Repositories.Common;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private MongoDbContext _context;

    public GenericRepository(MongoDbContext context, string collectionName)
    {
        _context = context;
        _collection = context.GetCollection<T>(collectionName);
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }
}