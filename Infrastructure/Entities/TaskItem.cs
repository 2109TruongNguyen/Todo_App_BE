namespace Infrastructure.Entities;

using MongoDbGenericRepository.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Infrastructure.Enums;

[CollectionName("TaskItems")]
public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    public Guid CreatorUserId { get; set; }
    
    public string TeamId { get; set; } = string.Empty;
    
    public ICollection<Guid> Assignees { get; set; } = new List<Guid>();
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [BsonRepresentation(BsonType.String)]
    public PriorityLevel Priority { get; set; } 
    
    [BsonRepresentation(BsonType.String)]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    
    public DateTime DueDate { get; set; }
    
    public string? CategoryId { get; set; }
    
    public ICollection<SubTask> SubTask { get; set; } = [];
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

