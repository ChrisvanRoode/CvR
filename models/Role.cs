using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Role
{
    [BsonId]
    public ObjectId _id { get; set; }
    public required int Id { get; set; }
    public required string Title { get; set; }
}
