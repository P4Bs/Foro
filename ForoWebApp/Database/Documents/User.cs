using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Database.Documents;

public class User : IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("registeredAt")]
    public DateTime RegisteredAt { get; set; }

    [BsonElement("role")]
    public string Role { get; set; }
}
