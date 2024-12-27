using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ForoWebApp.Database.Enums;

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
	public DateOnly RegisteredAt { get; set; }

	[BsonElement("profilePicture")]
	public byte[]? ProfilePicture { get; set; }

	[BsonElement("userRole")]
	public UserRole Role { get; set; }
}
