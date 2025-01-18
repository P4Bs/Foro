using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ForoWebApp.Database.Documents;

public class Post : IDocument
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }

	[BsonElement("threadId")]
	[BsonRepresentation(BsonType.ObjectId)]
	public string ThreadId { get; set; }

	[BsonElement("userId")]
	[BsonRepresentation(BsonType.ObjectId)]
	public string UserId { get; set; }

	[BsonElement("content")]
	public string Content { get; set; }

	[BsonElement("postDate")]
	public DateTime PostDate { get; set; }
}
