using MongoDB.Bson.Serialization.Attributes;
namespace ForoWebApp.Models.ViewModels;

public class MessageViewModel
{
	public string Id { get; set; }

	public string UserId { get; set; }

	public string UserName { get; set; }

	public byte[]? UserProfilePicture { get; set; }

	public string Content { get; set; }

	public DateTime PublishingDate { get; set; }
}
