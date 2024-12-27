using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Database.Documents;

public class Theme : IDocument
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("name")]
	public string Name { get; set; }

	[BsonElement("description")]
	public string? Description { get; set; }

	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; }
}
