using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ForoWebApp.Database.Enums;

namespace ForoWebApp.Database.Documents
{
	public class Authentication
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public int Id { get; set; }

		[BsonElement("userId")]
		[BsonRepresentation(BsonType.ObjectId)]
		public int UserId { get; set; }

		[BsonElement("role")]
		public UserRole Role { get; set; }

		[BsonElement("passwordHash")]
		public string? PasswordHash { get; set; }

		[BsonElement("passwordSalt")]
		public string? PasswordSalt { get; set; }

		[BsonElement("lastPasswordUpdate")]
		public DateTime? LastPasswordUpdate { get; set; }

		[BsonElement("lastLogin")]
		public DateTime? LastLogin { get; set; }

		[BsonElement("registrationDate")]
		public DateTime? RegistrationDate { get; set; }

		[BsonElement("failedAttempts")]
		public int FailedAttempts { get; set; }

		[BsonElement("isLocked")]
		public bool IsLocked { get; set; }
	}
}
