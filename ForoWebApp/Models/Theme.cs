using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Models
{
	public class Theme
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public int Id { get; set; }

		[BsonElement("categoryId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public int CategoryId { get; set; }

		[BsonElement("name")]
		public string? Name { get; set; }

		[BsonElement("description")]
		public string? Description { get; set; }

		[BsonElement("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
