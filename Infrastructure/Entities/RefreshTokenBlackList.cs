using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Entities;

[CollectionName("RefreshTokenBlackList")]
public class RefreshTokenBlackList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public string Token { get; set; } = null!;
}
