namespace Infrastructure.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Infrastructure.Enums;

public class SubTask
{
    public string Title { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.String)]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
}
