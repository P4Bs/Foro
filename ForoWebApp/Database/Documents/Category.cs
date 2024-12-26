using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Database.Documents;

public class Category : IDocument
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public int Id { get; set; }

	[BsonElement("name")]
	public string? Name { get; set; }

	[BsonElement("creationDate")]
	public DateTime CreationDate { get; set; }

	[BsonElement("isDeleted")]
	public bool IsDeleted { get; set; }
}
