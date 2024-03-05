using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    public ObjectId _id { get; set; }
    
    // Initialize required properties with default values
    // or ensure they are set in the constructor/object initializer
    public int Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string TelNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role? Role { get; set; }

    // Constructor that takes 7 arguments
    public User(ObjectId _id, int id, string firstName, string lastName, string telNumber, string email, Role? role)
    {
        this._id = _id;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        TelNumber = telNumber;
        Email = email;
        Role = role;
    }
}
