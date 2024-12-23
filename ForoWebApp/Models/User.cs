using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Models
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public int Id { get; set; }

		[BsonElement("name")]
		public string? Name { get; set; }

		[BsonElement("email")]
		public string? Email { get; set; }

		[BsonElement("registeredAt")]
		public DateOnly RegisteredAt { get; set; }

		[BsonElement("profilePicture")]
		public byte[]? ProfilePicture { get; set; }
	}
}
