using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Database.Documents;

public class ForumThread : IDocument
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("themeId")]
	[BsonRepresentation(BsonType.ObjectId)]
	public string ThemeId { get; set; }

	[BsonElement("title")]
	public string Title { get; set; }

	[BsonElement("createdAt")]
	public DateTime CreatedAt { get; set; }

	[BsonElement("isClosed")]
	public bool IsClosed { get; set; }

	[BsonElement("closureDate")]
	public DateTime? ClosureDate { get; set; }
}
