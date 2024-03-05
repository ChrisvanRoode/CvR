using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Item
{
    [BsonId]
    public ObjectId _id { get; set; }
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }    
}
