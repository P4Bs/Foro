using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ForoWebApp.Models.ViewModels
{
	public class MessageViewModel
	{
		public int Id { get; set; }

		public int ThreadId { get; set; }

		public int UserId { get; set; }

		public string? UserName { get; set; }

		public byte[]? UserProfilePicture { get; set; }

		public string? MessageContent { get; set; }

		public DateTime? PublishingDate { get; set; }

		public DateTime? EditionDate { get; set; }
	}
}
