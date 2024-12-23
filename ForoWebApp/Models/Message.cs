using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ForoWebApp.Models
{
	public class Message
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public int Id { get; set; }

		[BsonElement("threadId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public int ThreadId { get; set; }

		[BsonElement("userId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public int UserId { get; set; }

		[BsonElement("messageContent")]
		public string? MessageContent { get; set; }

		[BsonElement("publishingDate")]
		public DateTime? PublishingDate { get; set; }

		[BsonElement("editionDate")]
		public DateTime? EditionDate { get; set; }
	}
}
