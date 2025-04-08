using Infrastructure.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace Infrastructure.Entities;

[CollectionName("Categories")]
public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public Guid UserId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public TypeOfTask Type { get; set; } = TypeOfTask.Individual;
    
    public string TeamId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public ICollection<string> TaskIds { get; set; } = [];
}
