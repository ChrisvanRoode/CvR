using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Net.Mail;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

public class User
{
    [BsonId]
    public ObjectId _id { get; set; }
    public int Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string TelNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role? Role { get; set; }

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
