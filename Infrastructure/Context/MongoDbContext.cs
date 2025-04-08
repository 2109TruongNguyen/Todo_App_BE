using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
        
        // var client = new MongoClient("mongodb+srv://nntnnt2000:DI7RlnxrGonwtoiW@cluster0.zoxny.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        // _database = client.GetDatabase("TodoApp");
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}