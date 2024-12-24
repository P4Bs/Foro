using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Models
{
	public class Thread
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public int Id { get; set; }

		[BsonElement("themeId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public int ThemeId { get; set; }

		[BsonElement("title")]
		public string? Title { get; set; }

		[BsonElement("messageCount")]
		public int MessageCount { get; set; }

		[BsonElement("lastUpdateAt")]
		public DateTime LastUpdateAt { get; set; }

		[BsonElement("createdAt")]
		public DateTime CreatedAt { get; set; }

		[BsonElement("isClosed")]
		public bool IsClosed { get; set; }

		[BsonElement("closureDate")]
		public DateTime? ClosureDate { get; set; }
	}
}
